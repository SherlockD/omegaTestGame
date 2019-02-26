using UnityEngine;
using UnityEngine.UI;

public class MainPanelController : MonoBehaviour
{
    [SerializeField] private LoadingScreen _loadingFade;
    
    [Header("Disabled buttons")]
    [SerializeField] private Button[] _buttons;

    [Header("Music")] 
    [SerializeField] private AudioClip _gameMusic;
    
    private Animator _animator;
    private RectTransform _rectTransform;
    private LoadingScreen _fader;

    private State _state ;

    private Vector3 startPosition;
    
    private enum State
    {
        Idle,
        SettingsOpened,
        SettingsClosed,
        Play
    }
    
    private void Awake()
    {
        startPosition = transform.position;
        _animator = GetComponent<Animator>();
        _fader = GetComponent<LoadingScreen>();
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        switch (_state)
        {
            case State.SettingsOpened:
                _rectTransform.anchoredPosition = Vector2.Lerp(_rectTransform.anchoredPosition,new Vector2(-Screen.width,0), 5*Time.deltaTime);
                break;
            case State.SettingsClosed:
                _rectTransform.anchoredPosition = Vector2.Lerp(_rectTransform.anchoredPosition,Vector2.zero, 5*Time.deltaTime);
                break;
                     
        }
    }

    private void ActiveButtons(bool enable)
    {
        foreach (var button in _buttons)
        {
            button.interactable = enable;
        }
    }
    
    private void SetState(State state)
    {
        _state = state;
        switch (state)
        {
                case State.SettingsOpened:
                    ActiveButtons(false);
                    break;
                case State.SettingsClosed:
                    ActiveButtons(true);
                    break;
                case State.Play:
                    _fader.FadeOut();
                    ActiveButtons(false);                    
                    SceneManager.Instance.LoadSceneFromBundle("https://s3.amazonaws.com/omegatestgame/PathesSave.json",_loadingFade,_gameMusic);
                    break;
        }
    }
    
    public void OpenSettings()
    {
        SoundManager.PlayButtonSound();
        SetState(State.SettingsOpened);
    }

    public void CloseSettings()
    {
        SoundManager.PlayButtonSound();
        SetState(State.SettingsClosed);
    }

    public void OnPlay()
    {
        SoundManager.PlayButtonSound();
        SetState(State.Play);
    }
}
