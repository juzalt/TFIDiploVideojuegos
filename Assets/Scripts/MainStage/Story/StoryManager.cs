using UnityEngine;
using TMPro;

public class StoryManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private StoryPartsCollectionSO storyPartsCollection;
    [SerializeField] private TextMeshProUGUI quote, middleText, quote2;

    private int playerProgress = 0;
    private void Start()
    {
        Debug.Log("StoryProgress::" + PlayerPrefs.GetInt("StoryProgress"));
        /*
        if (PlayerPrefs.GetInt("StoryProgress") != 0)
        {
            gameObject.SetActive(false);  
        }
        else
        {
            player.CanMove = false;
            PlayerPrefs.SetInt("StoryProgress", 1);
        }
        */
        playerProgress = GetPlayerProgress();
        player.CanMove = false;

        switch (playerProgress)
        {
            case 0:
                SetPopUpText(storyPartsCollection.intro);
                PlayerPrefs.SetInt("StoryProgress", 1);
                break;
            case 1:
                SetPopUpText(storyPartsCollection.intro2);
                break;
            case 2:
                SetPopUpText(storyPartsCollection.finishedOrderGame);
                break;
            case 3:
                SetPopUpText(storyPartsCollection.finishedStudyingGame);
                break;
            case 4:
                SetPopUpText(storyPartsCollection.finishedGame);
                break;
            default:
                gameObject.SetActive(false);
                break;
        }
    }

    public int GetPlayerProgress()
    {
        int storyProgress = PlayerPrefs.GetInt("StoryProgress");
        int orderGameProgress = PlayerPrefs.GetInt("OrderGameFinished");
        int studyingGameProgress = PlayerPrefs.GetInt("StudyingGameFinished");

        if (storyProgress == 0)
        {
            return 0;
        }
        else if (storyProgress == 4)
        {
            return 4;
        }
        else
        {
            if (orderGameProgress != 1 && studyingGameProgress != 1)
            {
                return 1; // none of the games are finished
            }
            else
            {
                if (studyingGameProgress != 1)
                {
                    return 2; // finished only order game
                }
                if (orderGameProgress != 1)
                {
                    return 3; // finished only studying game
                }
                else
                {
                    PlayerPrefs.SetInt("StoryProgress", 4);
                    return 4; // finished both games
                }
            }
        }
        
    }

    public void ClosePopUp()
    {
        AudioManager.Instance.PlaySound(AudioManager.Sound.UIClick);
        player.CanMove = true;
        gameObject.SetActive(false);
    }

    private void SetPopUpText(StorySO storyPart)
    {
        quote.text = storyPart.firstQuote;
        middleText.text = storyPart.Text;
        quote2.text = storyPart.secondQuote;
    }
}
