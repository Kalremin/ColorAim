using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseWnd : MonoBehaviour
{
    [SerializeField] Button _btnResume;
    [SerializeField] Button _btnOption;
    [SerializeField] Button _btnExit;
    [SerializeField] GameObject _prefOptionWnd;

    OptionManager _option;

    public void ClickDown(Image image) => image.color = Color.gray;

    public void ClickUP(Image image) => image.color = Color.white;

    private void Start()
    {
        // 게임 화면 일시 정지
    }

    public void OnClickResume()
    {
        if (gameObject.transform.GetComponentsInChildren<OptionManager>().Length > 0) 
            return;

        //InGameManager.Instance().ChangeEnablePauseWnd();// 필수적으로 삭제
        Destroy(gameObject);
    }

    public void OnClickOption()
    {
        if (gameObject.transform.GetComponentsInChildren<OptionManager>().Length > 0) 
            return;

        Instantiate(_prefOptionWnd, transform);        
    }

    public void OnClickExit()
    {
        if (gameObject.transform.GetComponentsInChildren<OptionManager>().Length > 0) 
            return;

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }



}
