using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameManager : MonoBehaviour  
{
    static InGameManager _unique;
    public static InGameManager _instance => _unique;

    [SerializeField] GameObject _camera;
    [SerializeField] GameObject[] _bots;
    [SerializeField] GameObject HpUnitsParent;
    [SerializeField] Material[] ColorMaterials;
    [SerializeField] GameObject _prefPauseWnd;
    [SerializeField] GameObject _endWnd;
    [SerializeField] GameObject _msgBg;
    [SerializeField] Text _msgText;

    DefineEnum.eGameState _gameState;
    //Transform _pauseTrans;
    public DefineEnum.eGameState GameState { get { return _gameState; } }
    public Transform PosCam { get { return _camera.transform; } }
    public Material GetColorMaterial(int index) { return ColorMaterials[index]; }

    bool isPauseWnd; 
    bool isEndWnd;

    int combo = 0;
    int cnt_kill = 0;

    int spawnTime = 5;
    int minSpawnTime = 2;
    int maxCountBot = 20;

    float flow_timer;
    float mx;
    float my;
    float spawnXposMin = -230;
    float spawnXposMax = 230;
    float spawnZposMin = -230;
    float spawnZposMax = 230;
    float exceptionSpawnRadius = 15;

    public int cnt_bot;

    public int maxHP = -1;
    public int unitHP = -1;

    public bool SpawnBot = false;
    public bool CheckLock = false;

    public void ChangeEnablePauseWnd() { isPauseWnd = !isPauseWnd; }
    public void ComboUp() => combo++;
    public void ComboClear() => combo = 0;
    public void KillsUp() => cnt_kill++;
    public bool EnableEndWnd { get { return isEndWnd; } set { isEndWnd = value; } }
    public int GetCombo() => combo;
    public int GetKills() => cnt_kill;

    public void SetHpUnit()
    {
        PlayerStatus.SetHPUnitLine(HpUnitsParent);
        
    }
    private void Awake()
    {
        isPauseWnd = false;
        isEndWnd = false;
        PlayerStatus.HP = PlayerStatus.MaxHP = maxHP;
        PlayerStatus.SetUnitHP(unitHP);

        _unique = this;
        _gameState = DefineEnum.eGameState.Ready;
    }

    // Start is called before the first frame update
    void Start()
    {
        flow_timer = 0f;

        SetHpUnit();
        ReadyGame();
    }

    // Update is called once per frame
    void Update()
    {

        switch (_gameState)
        {
            case DefineEnum.eGameState.Ready:

                if (flow_timer >= 2)
                    _msgText.text = "Start!";

                if (flow_timer >= 3)
                    PlayGame();

                flow_timer += Time.deltaTime;
                break;
            case DefineEnum.eGameState.Play:

                if (PlayerStatus.HP <= 0)
                {
                    EndGame();
                    return;
                }
                
                if (Input.GetKeyDown(KeyCode.Escape))   // 임시 정지 키 
                {
                    OpenPauseWnd();
                    Lockmouse(false);
                    return;
                }


                
                EnemySpawn();

                if (PlayerStatus.HP < PlayerStatus.MaxHP)
                    PlayerStatus.HP += Time.deltaTime * 10;

                if (cnt_kill >= 10)
                {
                    PlayerStatus.MaxHP += 50;
                    SetHpUnit();
                    cnt_kill = 0;
                }

                flow_timer += Time.deltaTime;

                break;

            case DefineEnum.eGameState.Pause:
                if(gameObject.transform.GetComponentsInChildren<PauseWnd>().Length <= 0)
                    ResumeGame();
                break;

            case DefineEnum.eGameState.End:
                if (flow_timer > 3)
                    ResultGame();

                flow_timer += Time.deltaTime;

                break;

            case DefineEnum.eGameState.Result:

                break;
        }

        

    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            OpenPauseWnd();
            
            return;
        }
    }
    void ReadyGame()    
    {
        _gameState = DefineEnum.eGameState.Ready;
        _msgBg.SetActive(true);
        _msgText.text = "Ready";
    }

    void PlayGame()
    {
        _gameState = DefineEnum.eGameState.Play;
        _msgBg.SetActive(false);

#if UNITY_ANDROID
        Lockmouse(false);
#else
        Lockmouse(true);
#endif

        flow_timer = 0;
    }

    void EnemySpawn()
    {
        if (flow_timer >= ((spawnTime - GUIManager._instance.Min > minSpawnTime) ? spawnTime - GUIManager._instance.Min : minSpawnTime))
        {
            flow_timer = 0f;
            if (cnt_bot < maxCountBot && SpawnBot == true)
            {
                cnt_bot++;
                EnemySpawnControl._instance.Spawn(_bots[RandomBotIndex()]);
            }
        }
    }

    Vector3 RandomSpawnPos()
    {
        float xPos = Random.Range(spawnXposMin, spawnXposMax);
        float zPos = Random.Range(spawnZposMin, spawnZposMax);
        Vector3 pos = new Vector3(xPos, 0, zPos);

        while (Vector3.Distance(pos, _camera.transform.position) <= exceptionSpawnRadius)
        {
            xPos = Random.Range(spawnXposMin, spawnXposMax);
            zPos = Random.Range(spawnZposMin, spawnZposMax);
            pos = new Vector3(xPos, 0, zPos);
        }


        return pos;
    }

    int RandomBotIndex()
    {
        int temp = Random.Range(0, 100);
        switch (temp/10)
        {
            case 0:
            case 1:
            case 2:
                temp = 1;//mutant
                break;
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
                temp = 0;//xbot
                break;
        }

        return temp;
    }

    void ResumeGame()
    {
        _gameState = DefineEnum.eGameState.Play;
#if UNITY_ANDROID
        Lockmouse(false);
#else
        Lockmouse(true);
#endif
    }


    void EndGame()
    {
        flow_timer = 0;
        _gameState = DefineEnum.eGameState.End;
        Lockmouse(false);
        _msgBg.SetActive(true);
        _msgText.text = "End";

    }

    void ResultGame()
    {
        _gameState = DefineEnum.eGameState.Result;
        Instantiate(_endWnd);
    }


    // 마우스를 게임 스크린으로 잠금
    void Lockmouse(bool check)
    {
        if (check)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    // 마우스 이동으로 화면 전환
    void ChangeMousePos()
    {
        
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        mx += h * Time.deltaTime * 50;
        my += v * Time.deltaTime * 10;

        if (my >= 90)
        {
            my = 90;
        }
        else if (my <= -90)
        {
            my = -90;
        }

        _camera.transform.eulerAngles = new Vector3(-my, mx, 0);
    }
    public void OpenPauseWnd()
    {
        _gameState = DefineEnum.eGameState.Pause;
        Lockmouse(false);
        isPauseWnd = true;
        Instantiate(_prefPauseWnd, transform);
    }

    // 무작위로 색상 지정
    public DefineEnum.eColor SetRandomColorState()
    {
        int colorInt = Random.Range(0, 5);
        DefineEnum.eColor state = new DefineEnum.eColor();
        switch (colorInt)
        {
            case 0:
                state = DefineEnum.eColor.Red;
                break;
            case 1:
                state = DefineEnum.eColor.Yellow;
                break;
            case 2:
                state = DefineEnum.eColor.Green;
                break;
            case 3:
                state = DefineEnum.eColor.Blue;
                break;
            case 4:
                state = DefineEnum.eColor.Purple;
                break;
        }

        return state;
    }

    // 플레이어 타격 소리
    public void PlayHitSound()
    {
        SoundControl._instance.PlayHitEffectSound(DefineEnum.eHitSound.HittedPlayer, _camera, SoundControl._instance.GetEffectVol());
    }
    
}
