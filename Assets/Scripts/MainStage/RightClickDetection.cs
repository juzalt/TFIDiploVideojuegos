using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightClickDetection : MonoBehaviour
{
    [SerializeField] Interactable[] Interactables;

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
        Array.ForEach(Interactables, (interactable) => interactable.OnMouseHover());
        AudioManager.Instance.PlaySound(AudioManager.Sound.DownRightClick);
    }

    public void OffRightClick()
    {
        Debug.Log("off");
        Array.ForEach(Interactables, (interactable) => interactable.OffMouseHover());
        AudioManager.Instance.PlaySound(AudioManager.Sound.UpRightClick);
    }
}
