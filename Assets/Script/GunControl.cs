using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunControl : MonoBehaviour
{
    
    [SerializeField] protected GameObject _bulletPref;    // 총알
    [SerializeField] protected GameObject _casePref;    // 탄피
    [SerializeField] protected GameObject _fireEffect;    // 발사 이펙트
    [SerializeField] protected Transform _posFire;    // 발사 위치
    [SerializeField] protected Transform _posFireEffect; // 발사 이펙트 위치
    [SerializeField] protected Transform _posCase; // 탄피 위치
    
    [SerializeField] protected DefineEnum.eGunKind _gunKind; // 총 종류

    protected Animator _animator;
    
    protected DefineEnum.eGunState _eGunState;

    int _max_Magazine = 0;
    public int _magazine;

    protected abstract void SetAwake();
    protected abstract void SetStart();

    protected void Awake()
    {
        _animator = GetComponent<Animator>();
        _max_Magazine = _magazine;
        _eGunState = DefineEnum.eGunState.Idle;
        SetAwake();
    }

    protected void Start()
    {
        SetStart();
    }

    protected void OnEnable()
    {
        _magazine = _max_Magazine;
        _eGunState = DefineEnum.eGunState.Idle;
        GUIManager._instance.SetText_Magazine(_magazine);
    }

    public DefineEnum.eGunKind GunKind { get { return _gunKind; } }

    public void ResetMagazine()
    {
        _magazine = _max_Magazine;
        GUIManager._instance.SetText_Magazine(_magazine);
    }

    
    //Aanimation Event

    public void CaseOutAnimation()
    {
        Instantiate(_casePref, _posCase.transform.position, _posCase.transform.rotation);
        GUIManager._instance.SetText_Magazine(--_magazine);
    }

    public void FireCompleteAnimation()
    {
        _animator.SetTrigger("Idle");
        _eGunState = DefineEnum.eGunState.Idle;//!reloading //fire = false;
        new WaitForSeconds(0.3f);
    }


}
