using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public bool SfxEnable
    {
        set { SoundManager.Instance.SfxEnable = value; }
    }

    public bool MusicEnable
    {
        set { SoundManager.Instance.MusicEnable = value; }
    }
}
