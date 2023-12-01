using UnityEngine;

public class Bed : Interactable
{
    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("OrderGameFinished") == 1)
        {
            gameObject.SetActive(false);
        }
    }
    //public GameObject bedPopup;
    //public override void Interact()
    //{
    //    bedPopup.SetActive(true);
    //}
}
