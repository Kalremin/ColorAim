using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    [SerializeField] Text _magazine_txt;
    //[SerializeField] Text _totalAmmo_txt;
    
    [SerializeField] Slider _healthSlider;

    [SerializeField] Text _minuteTime_txt;
    [SerializeField] Text _secondTime_txt;

    [SerializeField] Text _score_txt;

    [SerializeField] Image _nextColor_Img;
    [SerializeField] Material[] _nextColor_Mat;


    static GUIManager _unique;
    public static GUIManager _instance { get { return _unique; } }

    int _score = 0;
    int min = 0;
    int sec = 0;
    float flow_timer=0;
    public int Score { get { return _score; } }
    public int Min { get { return min; } }
    public DefineEnum.eColor _imgState;

    private void Awake()
    {
        _unique = this;
        ChangeNextColorMat();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        _score_txt.text = _score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (InGameManager._instance.GameState != DefineEnum.eGameState.Play)
            return;

        flow_timer += Time.deltaTime;
        SetTime(flow_timer);
        CheckHealth();
    }

    ///////////////////////////////////////////////////// 시간 형식 수정 필요
    void SetTime(float current)
    {
        min = (int)current / 60;
        sec = (int)current % 60;

        _minuteTime_txt.text = (min < 10) ? "0" + min.ToString() : min.ToString();
        _secondTime_txt.text = (sec < 10) ? "0" + sec.ToString() : sec.ToString();

    }

    void CheckHealth() 
    {
        
        _healthSlider.value = Mathf.Lerp(_healthSlider.value, PlayerStatus.HP / PlayerStatus.MaxHP, Time.deltaTime * 3);
        _healthSlider.GraphicUpdateComplete();
    }

    public void SetText_Magazine(int temp) { _magazine_txt.text = temp.ToString(); }
    //public void SetText_TotalAmmo(int temp) { _totalAmmo_txt.text = temp.ToString(); }
    public void PlusScore(int score) { _score += score; _score_txt.text = _score.ToString(); }

    public void ChangeNextColorMat()
    {
        int colorInt = Random.Range(0, 5);

        switch (colorInt)
        {
            case 0:
                _imgState = DefineEnum.eColor.Red;
                break;
            case 1:
                _imgState = DefineEnum.eColor.Yellow;
                break;
            case 2:
                _imgState = DefineEnum.eColor.Green;
                break;
            case 3:
                _imgState = DefineEnum.eColor.Blue;
                break;
            case 4:
                _imgState = DefineEnum.eColor.Purple;
                break;
        }

        _nextColor_Img.material = _nextColor_Mat[colorInt];
    }

    public DefineEnum.eColor ColorState { get { return _imgState; } }
}
