using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    AudioSource audioSource;

    public static AudioManager Instance { get => instance; }

    public enum Sound
    {
        GainWisdomSG,
        LoseConcentrationSG,
        UIClick,
        UIClickYes,
        UIClickNo,
        WinMiniGame,
        LoseMiniGame
    }

    private void Awake()
    {
        if (Instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(Sound sound)
    {
        audioSource.PlayOneShot(GetAudioClip(sound));
    }

    private AudioClip GetAudioClip(Sound sound)
    {
        foreach(AudioAssets.SoundAudioClip soundAudioClip in AudioAssets.Instance.sounds)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("The sound " + sound + "Doesn't exist.");

        return null;
    }
}
