using System;
using System.Collections;
using UnityEngine;

public class SceneManager : MonoBehaviour
{    
    public static SceneManager Instance;

    public event Action BeginLoadScene;
    public event Action EndLoadScene;

    private AsyncOperation _loadSceneAsync;
    
    private void Awake()
    {        
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName, Fader fader,AudioClip nextMusic)
    {
        fader.CallOnEndFade += () =>
        {
            _loadSceneAsync.allowSceneActivation = true;
            SoundManager.PlayMusic(nextMusic);            
        };
        StartCoroutine(LoadSceneAsync(sceneName,fader,nextMusic));
    }
    
    private IEnumerator LoadSceneAsync(string sceneName,Fader fader,AudioClip nextMusic)
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
