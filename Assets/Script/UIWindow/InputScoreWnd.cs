using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputScoreWnd : MonoBehaviour
{

    [SerializeField] GameObject _scoreboardWnd;
    [SerializeField] Text _inputScore;
    [SerializeField] InputField _inputName;


    int tempScore;
    int tempIndex=-1;

    // Start is called before the first frame update
    void Start()
    {
        tempScore = GUIManager._instance.Score;
        _inputScore.text = GUIManager._instance.Score.ToString();

        for (int i = 0; i < 5; i++)
        {
            if (tempScore > int.Parse(PlayerPrefs.GetString(RankingData.GetScoreData[i], "0")))
            {
                tempIndex = i;
                break;
            }
        }

        if(tempIndex == -1)
        {
            Instantiate(_scoreboardWnd);
            Destroy(gameObject);
        }
        
    }


    void ChangeScoreName(int index, string name, int score)
    {
        if (index >= 5)
            return;
        int tempIndex = index + 1;
        string tempName = PlayerPrefs.GetString(RankingData.GetNameData[index], "[Empty]");
        int tempScore = int.Parse(PlayerPrefs.GetString(RankingData.GetScoreData[index], "0"));
        ChangeScoreName(tempIndex, tempName, tempScore);

        PlayerPrefs.SetString(RankingData.GetNameData[index], name);
        PlayerPrefs.SetString(RankingData.GetScoreData[index], score.ToString());
    }

    public void ClickBtnInputScore()
    {
        int tempscore = int.Parse(_inputScore.text);
        ChangeScoreName(tempIndex, _inputName.text, tempscore);

        Instantiate(_scoreboardWnd);
        Destroy(gameObject);
    }
}
