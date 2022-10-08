using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Entity : MonoBehaviour
{
    protected enum eEnemyKind
    {
        Xbot = 0,
        Mutant
    }

    [SerializeField] GameObject _itemColor;
    [SerializeField] GameObject _itemAmmo;
    [SerializeField] GameObject _barrior;

    protected float _MoveSpeed;
    protected eEnemyKind _kind;
    protected DefineEnum.eHitSound _hitSound;
    protected DefineEnum.eEnemyState _state;
    protected DefineEnum.eColor _colorSkin;
    
    protected Animator _animator;
    protected NavMeshAgent _navAgent;
    protected Transform _posCam;
    protected Vector3 _vecTargetPos;

    //public SkinnedMeshRenderer joint_meshRenderer;
    //public MeshCollider joint_collider; //Mesh Init
    public SkinnedMeshRenderer surface_meshRenderer;
    //public MeshCollider surface_collider; //Mesh Init

    protected int ani_death_cnt;
    protected int ani_attack_cnt;

    int score_bot;
    int spawnRateBall = 40;
    int spawnRateAmmo = 20;
    bool _isMoving;
    bool _isJumping;
    Vector3 _continueVel;

    float _deadTime = 5;
    float _flowTimer = 0;
    float _stopDistance = 4.5f;


    public int Score { get { return score_bot; } }
    public DefineEnum.eColor ColorSkin { get { return _colorSkin; } }
    void CreateItem()
    {
        float random = Random.Range(0, 100);
        
        if (_itemColor != null && random < spawnRateBall)
        {
            
            Instantiate(_itemColor, transform.position + Vector3.up * 1, _itemColor.transform.rotation);
        }
    }

    protected void SetAwakeMethod(int death_cnt, int attack_cnt, int score, eEnemyKind kind)
    {
        _animator = GetComponent<Animator>();
        _navAgent = GetComponent<NavMeshAgent>();
        ani_death_cnt = death_cnt;
        ani_attack_cnt = attack_cnt;
        score_bot = score;
        

        if (Random.Range(0, 2) == 1)
            _MoveSpeed = 6;
        else
            _MoveSpeed = 3;

        _kind = kind;
        switch (_kind)
        {
            case eEnemyKind.Xbot:
                _hitSound = DefineEnum.eHitSound.HittedXbot;
                break;
            case eEnemyKind.Mutant:
                _hitSound = DefineEnum.eHitSound.HittedMutant;
                break;
        }

        _colorSkin = DefineEnum.eColor.None;
        _isMoving = true;
        _isJumping = false;
    }

    protected void SetStartMethod()
    {
        _navAgent.destination = InGameManager._instance.PosCam.position;
        _navAgent.stoppingDistance = _stopDistance;
        _navAgent.speed = _MoveSpeed;
        _posCam = InGameManager._instance.PosCam;
        _vecTargetPos = new Vector3(_posCam.position.x, 0, _posCam.position.z);

        transform.rotation = Quaternion.LookRotation((_vecTargetPos - transform.position).normalized);

        if (Random.Range(0, 100) > spawnRateAmmo)
            _barrior.SetActive(false);
        ChangeState(DefineEnum.eEnemyState.Run);
    }


    protected void Update()
    {
        
        switch (_state)
        {
            case DefineEnum.eEnemyState.Run:
                if (FarTarget())
                    ChangeState(DefineEnum.eEnemyState.Attack);

                if(InGameManager._instance.GameState != DefineEnum.eGameState.Play)
                    ChangeState(DefineEnum.eEnemyState.Pause);

                break;

            case DefineEnum.eEnemyState.Attack:
                if (!FarTarget())
                    ChangeState(DefineEnum.eEnemyState.Reset);

                if (InGameManager._instance.GameState != DefineEnum.eGameState.Play)
                    ChangeState(DefineEnum.eEnemyState.Pause);

                break;

            case DefineEnum.eEnemyState.Death:
                if(_flowTimer >=_deadTime)
                {
                    if (Random.Range(0, 100) < spawnRateAmmo)
                        Instantiate(_itemAmmo, transform.position, _itemAmmo.transform.rotation);
                    Destroy(gameObject);
                }

                _flowTimer += Time.deltaTime;

                break;

            case DefineEnum.eEnemyState.Pause:
                if (InGameManager._instance.GameState == DefineEnum.eGameState.Play)
                {
                    if (FarTarget())
                        ChangeState(DefineEnum.eEnemyState.Attack);
                    else
                        ChangeState(DefineEnum.eEnemyState.Run);
                }
                break;        
        }

        //transform.rotation = Quaternion.LookRotation((_vecTargetPos - transform.position).normalized);

    }


    void StopActionForPause()
    {
        if (_isMoving)
        {
            _isMoving = false;
            _animator.speed = 0;
            _navAgent.isStopped = true;
            _navAgent.speed = 0;
            _continueVel = _navAgent.velocity;
            _navAgent.velocity = Vector3.zero;
        }
        else
        {
            _isMoving = true;
            _animator.speed = 1;
            _navAgent.isStopped = false;
            _navAgent.speed = _MoveSpeed;
            _navAgent.velocity = _continueVel;

        }
    }

    bool FarTarget()
    {
        if (Vector3.Distance(transform.position, _vecTargetPos) < _stopDistance)
            return true;
        return false;
    }


    void ChangeState(DefineEnum.eEnemyState state)
    {
        int temp;

        switch (state)
        {
            case DefineEnum.eEnemyState.Idle:
                _animator.SetBool("Idle", true);
                _state = state;
                break;

            case DefineEnum.eEnemyState.Walk:
                _animator.SetBool("Walk", true);
                _state = state;
                break;

            case DefineEnum.eEnemyState.Run:
                if (_MoveSpeed == 3)
                    _animator.SetBool("Run", false);
                else
                    _animator.SetBool("Run", true);

                if (!_isMoving)
                    StopActionForPause();

                _state = state;
                break;
            case DefineEnum.eEnemyState.Attack:
                if (!_isMoving)
                    StopActionForPause();

                temp = Random.Range(0, ani_attack_cnt);
                _animator.SetInteger("Attack", temp);
                _navAgent.isStopped = true;
                _navAgent.speed = 0;
                _continueVel = _navAgent.velocity;
                _navAgent.velocity = Vector3.zero;
                _state = state;
                break;

            case DefineEnum.eEnemyState.Death:
                temp = Random.Range(0, ani_death_cnt);

                _animator.SetInteger("Death",temp);
                _animator.SetTrigger("DeathTri");
                _navAgent.isStopped = true;
                _navAgent.speed = 0;
                _navAgent.velocity = Vector3.zero;
                _state = state;
                InGameManager._instance.cnt_bot--;
                Destroy(transform.Find("EnemyMarker").gameObject);
                //Destroy(gameObject, 5f);
                break;

            case DefineEnum.eEnemyState.Reset:
                _animator.SetTrigger("Reset");
                _animator.SetInteger("Attack", -1);
                _state = DefineEnum.eEnemyState.Run;
                break;

            case DefineEnum.eEnemyState.Pause:
                if(_isMoving)
                    StopActionForPause();
                _state = state;
                break;
        }
    }


    public bool Hitted(DefineEnum.eColor fireColor)
    {
        if(fireColor != DefineEnum.eColor.None) // pistol bullet
        {
            if (_kind == eEnemyKind.Xbot && fireColor != _colorSkin)    //xbot
                return false;
        }


        if (_barrior.activeSelf)
        {
            _barrior.SetActive(false);
            return true;
        }

        ChangeState(DefineEnum.eEnemyState.Death);
        float vol = (50f - Vector3.Distance(transform.position, _vecTargetPos)) / 50f;
        vol = vol < 0 ? 0 : vol;
        SoundControl._instance.PlayHitEffectSound(_hitSound, gameObject, vol * SoundControl._instance.GetEffectVol());
        GetComponent<Collider>().enabled = false;
        InGameManager._instance.ComboUp();
        InGameManager._instance.KillsUp();
        GUIManager._instance.PlusScore(score_bot + InGameManager._instance.GetCombo() * 10);
        CreateItem();

        return true;
    }

    public void SetBarrior(bool swiBarr)
    {
        _barrior.SetActive(swiBarr);
    }

    public void AttackAni()
    {
        ChangeState(DefineEnum.eEnemyState.Attack);
    }

    public void JumpAtkAni()
    {
        _isJumping = !_isJumping;
        _animator.SetBool("JumpAtk", _isJumping);
    }

    //protected void ColliderInit(SkinnedMeshRenderer renderer, MeshCollider collider)    //  메쉬 콜라이더 업데이트 메소드
    //{
    //    Mesh bakedMesh = new Mesh();
    //    collider.sharedMesh = bakedMesh; //Transform or Blend
    //    renderer.SetBlendShapeWeight(0, 1); //Mesh Update
    //    renderer.BakeMesh(bakedMesh);
    //    collider.sharedMesh = bakedMesh;
    //}


}
