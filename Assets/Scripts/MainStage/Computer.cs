using UnityEngine;

public class Computer : Interactable
{
    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("StudyingGameFinished") == 1)
        {
            gameObject.SetActive(false);
        }
    }
    //public GameObject computerPopup;
    //public override void Interact()
    //{
    //    computerPopup.SetActive(true);
    //}

}
