using System;
using System.Collections;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private ErrorPanel _errorPanel;
    
    public static SceneManager Instance;

    public event Action BeginLoadScene;
    public event Action EndLoadScene;

    private AsyncOperation _loadSceneAsync;

    private event Action<AssetBundle> responce;

    private string _unpackedScene;
    
    private void Awake()
    {
        responce += unpackScene;
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName, LoadingScreen fader,AudioClip nextMusic)
    {
        fader.CallOnEndFade += () =>
        {
            _loadSceneAsync.allowSceneActivation = true;
            SoundManager.PlayMusic(nextMusic);            
        };
        StartCoroutine(LoadSceneAsync(sceneName,fader,nextMusic));
    }

    public void LoadSceneFromBundle(string pathesSaveUrl, LoadingScreen fader,AudioClip nextMusic)
    {
        StartCoroutine(LoadSceneFromBundleAsync(pathesSaveUrl,responce,fader,nextMusic));
    }

    public void StopSceneLoad()
    {
        StopAllCoroutines();
    }
    
    private void unpackScene(AssetBundle bundle)
    {        
        if (bundle.isStreamedSceneAssetBundle)
        {
            _unpackedScene = System.IO.Path.GetFileNameWithoutExtension(bundle.GetAllScenePaths()[0]);
            Debug.Log(System.IO.Path.GetFileNameWithoutExtension(bundle.GetAllScenePaths()[0]));
        }
    }
    
    private IEnumerator LoadSceneFromBundleAsync(string pathesSaveUrl,Action<AssetBundle> responce,LoadingScreen fader,AudioClip nextMusic)
    {      
        AssetBundleManager.Instance.LoadBundle(pathesSaveUrl,responce,fader);

        while(_unpackedScene == null)
        {
            yield return null;
        }
        
        LoadScene(_unpackedScene,fader,nextMusic);
    }
    
    private IEnumerator LoadSceneAsync(string sceneName,LoadingScreen fader,AudioClip nextMusic)
    {
        yield return new WaitForSeconds(2);
        
        _loadSceneAsync = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        _loadSceneAsync.allowSceneActivation = false;
        BeginLoadScene?.Invoke();
        while (!_loadSceneAsync.isDone)
        {
            if (_loadSceneAsync.progress >= 0.9f)
            {
                fader.FadeOut();
            }
            yield return null;
        }        
        EndLoadScene?.Invoke();
        yield return null;
        
    }
}
