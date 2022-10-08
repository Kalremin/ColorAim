using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardControl : MonoBehaviour
{
    [SerializeField] Text[] _nameList;
    [SerializeField] Text[] _scoreList;

    [SerializeField] GameObject _inputUI;
    [SerializeField] InputField _inputName;
    [SerializeField] Text _inputScore;

    bool isShiftPressed;

    private void Awake()
    {
        
    }

    private void Start()
    {
        
        for (int i = 0; i < 5; i++)
        {
            _nameList[i].text = PlayerPrefs.GetString(RankingData.GetNameData[i], "[Empty]");
            //PlayerPrefs.SetString(RankingData.GetScoreData[i], "500");
            _scoreList[i].text = PlayerPrefs.GetString(RankingData.GetScoreData[i], "0");
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            isShiftPressed = true;
        else
            isShiftPressed = false;

        if (isShiftPressed && Input.GetKeyDown(KeyCode.D))
        {
            PlayerPrefs.DeleteAll();
            
            for (int i = 0; i < 5; i++)
            {
                _nameList[i].text = PlayerPrefs.GetString(RankingData.GetNameData[i], "[Empty]");
                _scoreList[i].text = PlayerPrefs.GetString(RankingData.GetScoreData[i], "0");
            }
            
        }
    }



    public void ClickBoardListOk()
    {
        
        if (SceneControlManager._instance.GetSceneState() != DefineEnum.eScenesState.TitleScene)
            SceneControlManager._instance.ChangeScene(DefineEnum.eScenesState.TitleScene);
        else
            TitleUIManager._instance.BtnClickScoreboardOK();
        Destroy(gameObject);
    }


    
}
