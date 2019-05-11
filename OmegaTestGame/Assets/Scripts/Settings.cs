﻿using Scripts.SoundManagerNameSpace;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class Settings : MonoBehaviour
    {
        [SerializeField] Toggle _sfxToggle;
        [SerializeField] Toggle _musicToggle;

        private void Start()
        {
            _sfxToggle.isOn = SoundManager.Instance.SfxEnable;
            _musicToggle.isOn = SoundManager.Instance.MusicEnable;
        }

        public bool SfxEnable
        {
            set { SoundManager.Instance.SfxEnable = value; }
        }

        public bool MusicEnable
        {
            set { SoundManager.Instance.MusicEnable = value; }
        }
    }
}