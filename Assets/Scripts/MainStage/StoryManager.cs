using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    [SerializeField] PlayerMovement player;

    private void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("StoryProgress"));
        if (PlayerPrefs.GetInt("StoryProgress") != 0)
        {
            gameObject.SetActive(false);  
        }
        else
        {
            player.CanMove = false;
            PlayerPrefs.SetInt("StoryProgress", 1);
        }
    }

    public void ClosePopUp()
    {
        AudioManager.Instance.PlaySound(AudioManager.Sound.UIClick);
        player.CanMove = true;
        gameObject.SetActive(false);
    }
}
