using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControlManager : MonoBehaviour
{
    

    [SerializeField] GameObject _loadingWnd;

    DefineEnum.eScenesState _sceneState;

    static SceneControlManager _unique;
    public static SceneControlManager _instance { get { return _unique; } }

    private void Awake()
    {
        _unique = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeScene(DefineEnum.eScenesState.TitleScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public DefineEnum.eScenesState GetSceneState() => _sceneState;
    public void ChangeScene(DefineEnum.eScenesState scenesState)
    {
        _sceneState = scenesState;
        StartCoroutine(LoadingScene(_sceneState.ToString()));
        SoundControl._instance.PlayBGMSound(_sceneState, SoundControl._instance.GetBGMVol());
    }

    //public void ChangeSceneTitle()
    //{
    //    _sceneState = ScenesCollection.TitleScene;
    //    StartCoroutine(LoadingScene(_sceneState.ToString()));
    //    SoundControl._instance.PlayBGMSound(DefineEnum.eSoundBgm.Title, SoundControl._instance.GetBGMVol());
    //}

    //public void ChangeSceneInGame()
    //{
    //    StartCoroutine(LoadingScene(ScenesCollection.InGameScene.ToString()));
    //    SoundControl._instance.PlayBGMSound(DefineEnum.eSoundBgm.InGame, SoundControl._instance.GetBGMVol());
    //}

    IEnumerator LoadingScene(string SceneName)
    {
        GameObject go = Instantiate(_loadingWnd, transform);
        LoadSceneWnd wnd = go.GetComponent<LoadSceneWnd>();
        wnd.OpenWnd();
        AsyncOperation ao = SceneManager.LoadSceneAsync(SceneName);
        while (!ao.isDone)
        {
            wnd.SetLoadingRate(ao.progress);
            yield return null;
        }

        wnd.SetLoadingRate(ao.progress);
        yield return null;

        Destroy(go);
    }
}
