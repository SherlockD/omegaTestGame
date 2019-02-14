using System;
using UnityEngine;


public class Fader : MonoBehaviour
{
    public event Action CallOnBeginFade;
    public event Action CallOnEndFade;
    
    [SerializeField] private float _fadeSpeed;
    
    private enum FadeStatus
    {
        FadeIdle,
        FadeStart,
        FadeInProgress ,
        FadeEnd
    }

    private enum FadeState
    {
        FadeIn,
        FadeOut
    }
    
    private  CanvasGroup _canvasGroup;
    private FadeStatus _fadeStatus;
    private FadeState _fadeState;
    

    private void Awake()
    {
        _fadeStatus = FadeStatus.FadeIdle;
        _canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    private void Update()
    {
        switch (_fadeStatus)
        {
                case FadeStatus.FadeIdle:
                    return;
                case FadeStatus.FadeStart:
                    CallOnBeginFade?.Invoke();
                    _fadeStatus = FadeStatus.FadeInProgress;
                    break;
                case FadeStatus.FadeInProgress:
                    switch (_fadeState)
                    {
                            case FadeState.FadeOut:
                                _canvasGroup.alpha = Mathf.Lerp(_canvasGroup.alpha,0, Time.deltaTime*_fadeSpeed);
                                if (_canvasGroup.alpha < 0.01f)
                                {
                                    _fadeStatus = FadeStatus.FadeEnd;
                                }
                                break;
                            case FadeState.FadeIn:
                                Mathf.Lerp(_canvasGroup.alpha, 1, _fadeSpeed * Time.deltaTime);
                                if (_canvasGroup.alpha > 0.9f)
                                {
                                    _fadeStatus = FadeStatus.FadeEnd;                                                           
                                }
                                break;
                    }
                    break;
                case FadeStatus.FadeEnd:
                    CallOnEndFade?.Invoke();
                    Destroy(_canvasGroup);
                    _fadeStatus = FadeStatus.FadeIdle;
                    gameObject.SetActive(false);
                    break;
        }
    }

    public void FadeIn()
    {
        if (_fadeStatus == FadeStatus.FadeIdle)
        {
            _fadeState = FadeState.FadeIn;
            _fadeStatus = FadeStatus.FadeStart;
        }
    }

    public void FadeOut()
    {
        if (_fadeStatus == FadeStatus.FadeIdle)
        {
            _fadeState = FadeState.FadeOut;
            _fadeStatus = FadeStatus.FadeStart;
        }
    }
}
