using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio sources")]

    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundSource;

    [Header("Standard audio clips")] 
    [SerializeField] private AudioClip _standardMusic;
    [SerializeField] private AudioClip _buttonClickSound;
    
    public bool SfxEnable
    {
        get { return !Instance._soundSource.mute; }
        set { Instance._soundSource.mute = !value; }
    }

    public bool MusicEnable
    {
        get { return !Instance._musicSource.mute; }
        set { Instance._musicSource.mute = !value; }
    }

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

    public static void PlaySound(AudioClip clip)
    {
        Instance.PlaySoundInternal(clip);
    }

    public static void PlayMusic(AudioClip clip)
    {
        Instance.PlayMusicInternal(clip);
    }

    public static void PlayButtonSound()
    {
        Instance.PlaySoundInternal(Instance._buttonClickSound);
    }
        
    private void PlaySoundInternal(AudioClip clip)
    {
        _soundSource.PlayOneShot(clip);
    }

    private void PlayMusicInternal(AudioClip clip)
    {
        if (clip != null)
        {
            _musicSource.clip = clip;
            _musicSource.Play();
        }
    }        
}
