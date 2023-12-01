using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance { get => instance; }

    [SerializeField] AudioSource ambientMusicAudioSource;
    [SerializeField] AudioSource miniGameMusicAudioSource;
    AudioSource audioSource;

    

    public enum Sound
    {
        GainWisdomSG,
        LoseConcentrationSG,
        UIClick,
        UIClickYes,
        UIClickNo,
        WinMiniGame,
        LoseMiniGame,
        MoveClothOG,
        AmbientMusic
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

    public void ChangeMusic()
    {
        Debug.Log("Ambientmusic: " + ambientMusicAudioSource.isPlaying);
        if (ambientMusicAudioSource.isPlaying)
        {
            ambientMusicAudioSource.Stop();
            miniGameMusicAudioSource.Play();
        }
        else
        {
            miniGameMusicAudioSource.Stop();
            ambientMusicAudioSource.Play();
        }
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
