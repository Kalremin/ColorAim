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

    public void OnClickResume()
    {
        if (gameObject.transform.GetComponentsInChildren<OptionManager>().Length > 0) 
            return;
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
