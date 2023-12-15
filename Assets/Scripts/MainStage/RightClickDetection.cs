using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightClickDetection : MonoBehaviour
{
     void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            OnRightClick();
        } 
        
        if (Input.GetMouseButtonUp(1)) 
        {
            OffRightClick();
        }
    }

    public void OnRightClick()
    {
        AudioManager.Instance.PlaySound(AudioManager.Sound.DownRightClick);
    }

    public void OffRightClick()
    {
        Debug.Log("off");
        AudioManager.Instance.PlaySound(AudioManager.Sound.UpRightClick);
    }
}
