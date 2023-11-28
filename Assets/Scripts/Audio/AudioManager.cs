using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    AudioSource audioSource;

    public static AudioManager Instance { get => instance; }

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

    public void PlayUIClickSound()
    {
        audioSource.PlayOneShot(AudioAssets.Instance.UIClick);
    }
}
