using UnityEngine;

public class Bed : Interactable
{
    public GameObject bedPopup;
    public override void Interact()
    {
        bedPopup.SetActive(true);
    }
}
