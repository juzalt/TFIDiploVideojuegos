using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAssets : MonoBehaviour
{
    private static AudioAssets instance;

    public static AudioAssets Instance
    {
        get
        {
            if (instance == null) instance = Instantiate(Resources.Load<AudioAssets>("AudioAssets"));
            return instance;
        }
    }

    [SerializeField] public SoundAudioClip[] sounds;

    [System.Serializable]
    public class SoundAudioClip
    {
        public AudioManager.Sound sound;
        public AudioClip audioClip;
    }
}