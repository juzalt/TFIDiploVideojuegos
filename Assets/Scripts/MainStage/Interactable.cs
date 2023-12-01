using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Interactable : MonoBehaviour
{
    [SerializeField] int miniGameSceneIndex;
    [SerializeField] UIPopUp uiPopUp;
    [SerializeField] PlayerInteractions player;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] GameObject light;

    Color glowColor = new(0.4f, 0.4f, 0.4f);
    float glowfactor = 1.9f;
    //MeshRenderer meshRenderer;

    public virtual void Awake()
    {
        gameObject.layer = 6;
        //meshRenderer = GetComponent<MeshRenderer>();
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
        SceneManager.LoadScene(miniGameSceneIndex);
        AudioManager.Instance.PlaySound(AudioManager.Sound.UIClickYes);
        AudioManager.Instance.ChangeMusic();
    }

    public void StayInMainScene()
    {
        uiPopUp.popUp.SetActive(false);
        player.CanMove = true;
        AudioManager.Instance.PlaySound(AudioManager.Sound.UIClickNo);
    }


    public void OnMouseHover()
    {
        int glowMaterialIndex = meshRenderer.materials.Length - 1;
        meshRenderer.materials[glowMaterialIndex].SetColor("_EmissionColor", glowColor * glowfactor);
        if (light != null)
            light.SetActive(true);
    }

    public void OffMouseHover()
    {
        int glowMaterialIndex = meshRenderer.materials.Length - 1;
        meshRenderer.materials[glowMaterialIndex].SetColor("_EmissionColor", Color.black);
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
