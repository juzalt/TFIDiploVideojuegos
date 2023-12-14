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
    }

    public void OnRightClick()
    {
        Debug.Log("aqui");
        AudioManager.Instance.PlaySound(AudioManager.Sound.DownRightClick);
    }
}
