using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Interactable : MonoBehaviour
{
    [SerializeField] int miniGameSceneIndex;
    [SerializeField] UIPopUp uiPopUp;
    [SerializeField] PlayerMovement player;
    [SerializeField] GameObject light;

    bool goesToMiniGame = false;

    public virtual void Awake()
    {
        gameObject.layer = 6;
    }

    public void Interact()
    {
        uiPopUp.popUp.SetActive(true);
        player.CanMove = false;
        uiPopUp.YesBTN.onClick.AddListener(PlayMiniGame);
        uiPopUp.titleText.text = uiPopUp.helpTitle;
        uiPopUp.yesBtnText.text = uiPopUp.yesText;
        uiPopUp.noBtnText.text = uiPopUp.noText;
    }

    public void PlayMiniGame()
    {
        if (goesToMiniGame) { return;  }
        Debug.Log("Play Mini Game" + gameObject.transform.name);
        AudioManager.Instance.PlaySound(AudioManager.Sound.UIClickYes);
        AudioManager.Instance.ChangeMusic();
        SceneManager.LoadScene(miniGameSceneIndex);
        goesToMiniGame = true;
    }

    public void StayInMainScene()
    {
        uiPopUp.popUp.SetActive(false);
        player.CanMove = true;
        AudioManager.Instance.PlaySound(AudioManager.Sound.UIClickNo);
    }


    public void OnMouseHover()
    {
        if (light != null)
            light.SetActive(true);
    }

    public void OffMouseHover()
    {
        if (light != null)
            light.SetActive(false);
    }

    [System.Serializable]
    public class UIPopUp
    {
        public GameObject popUp;
        public Button YesBTN;
        public TextMeshProUGUI titleText, yesBtnText, noBtnText;
        public string helpTitle, yesText, noText;
    }

    
}
