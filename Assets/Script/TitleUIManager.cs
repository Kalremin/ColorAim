using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUIManager : MonoBehaviour
{
    [SerializeField] GameObject _prefabScoreboardWnd;
    [SerializeField] GameObject _prefabOptionWnd;

    static TitleUIManager _unique;

    public static TitleUIManager _instance { get { return _unique; } }

    bool isScoreboard = false;
    bool isOption = false;

    private void Awake()
    {
        _unique = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DonwBtn(Image img)
    {
        if (gameObject.transform.GetComponentsInChildren<OptionManager>().Length <= 0
            && !isScoreboard)
            img.fillCenter = false;
    }

    public void UpBtn(Image img)
    {
        if (gameObject.transform.GetComponentsInChildren<OptionManager>().Length <= 0 
            && !isScoreboard)
            img.fillCenter = true;
    }

    public void ClickStartBtn()
    { 
        if (gameObject.transform.GetComponentsInChildren<OptionManager>().Length <= 0 
            && !isScoreboard) 
            SceneControlManager._instance.ChangeScene(DefineEnum.eScenesState.InGameScene); 
    }
    
    public void ClickScoreboardBtn() 
    { 
        if (gameObject.transform.GetComponentsInChildren<OptionManager>().Length <= 0 
            && !isScoreboard) 
        {
            isScoreboard = true; 
            Instantiate(_prefabScoreboardWnd); 
        } 
    }
    
    public void ClickOptionBtn() 
    { 
        if (gameObject.transform.GetComponentsInChildren<OptionManager>().Length <= 0
            && !isScoreboard) 
        { 
            isOption = true; Instantiate(_prefabOptionWnd,transform); 
        } 
    }

    public void ClickExitBtn() 
    {
        if (!isOption && !isScoreboard)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
        }
    }
    
    public void BtnClickScoreboardOK()
    {
        isScoreboard = false;
        Debug.Log(isScoreboard);
    }

    public void BtnClickOptionOK()
    {
        isOption = false;
    }

}
