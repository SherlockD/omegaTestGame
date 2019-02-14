﻿using UnityEngine;


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
        set { Instance._soundSource.mute = !value; }
    }

    public bool MusicEnable
    {
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
    
    #region PRIVATE MEMBERS
    
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
        else
        {
            _musicSource.clip = _standardMusic;
            _musicSource.Play();
        }
    }
    
    #endregion
    
}
