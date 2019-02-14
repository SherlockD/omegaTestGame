using UnityEngine;
using UnityEngine.UI;

public class MainPanelController : MonoBehaviour
{
    [SerializeField] private Fader _loadingFade;
    
    [Header("Disabled buttons")]
    [SerializeField] private Button[] _buttons;

    [Header("Music")] 
    [SerializeField] private AudioClip _gameMusic;
    
    private Animator _animator;

    private Fader _fader;
    
    private enum State
    {
        SettingsOpened,
        SettingsClosed,
        Play
    }
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _fader = GetComponent<Fader>();
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
        switch (state)
        {
                case State.SettingsOpened:
                    ActiveButtons(false);
                    _animator.SetBool("SlideTheMenu",true);
                    break;
                case State.SettingsClosed:
                    _animator.SetBool("SlideTheMenu",false);
                    ActiveButtons(true);
                    break;
                case State.Play:
                    _fader.FadeOut();
                    ActiveButtons(false);
                    SceneManager.Instance.LoadScene("GameScene",_loadingFade,_gameMusic);
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
