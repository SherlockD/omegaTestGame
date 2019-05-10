using System;
using System.Collections;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private ErrorPanel _errorPanel;
    
    public static SceneManager Instance;

    private AsyncOperation _loadSceneAsync;

    private event Action<AssetBundle> responce;

    private string _unpackedScene;
    
    private void Awake()
    {
        responce += UnpackScene;
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName, Fader fader)
    {
        fader.CallOnEndFade += () =>
        {
            _loadSceneAsync.allowSceneActivation = true;          
        };
        StartCoroutine(LoadSceneAsync(sceneName,fader));
    }

    public void LoadSceneFromBundle(string pathesSaveUrl, Fader fader)
    {
        StartCoroutine(LoadSceneFromBundleAsync(pathesSaveUrl,responce,fader));
    }

    public void StopSceneLoad()
    {
        StopAllCoroutines();
    }
    
    private void UnpackScene(AssetBundle bundle)
    {        
        if (bundle.isStreamedSceneAssetBundle)
        {
            _unpackedScene = System.IO.Path.GetFileNameWithoutExtension(bundle.GetAllScenePaths()[0]);
            Debug.Log(System.IO.Path.GetFileNameWithoutExtension(bundle.GetAllScenePaths()[0]));
        }
    }
    
    private IEnumerator LoadSceneFromBundleAsync(string pathesSaveUrl,Action<AssetBundle> responce,Fader fader)
    {      
        AssetBundleManager.Instance.LoadBundle(pathesSaveUrl,responce,fader);

        while(_unpackedScene == null)
        {
            yield return null;
        }
        
        LoadScene(_unpackedScene,fader);
    }
    
    private IEnumerator LoadSceneAsync(string sceneName,Fader fader)
    {        
        _loadSceneAsync = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        _loadSceneAsync.allowSceneActivation = false;

        while (!_loadSceneAsync.isDone)
        {
            if (_loadSceneAsync.progress >= 0.9f)
            {
                fader.FadeOut();
            }
            yield return null;
        }        
        yield return null;
        
    }
}
