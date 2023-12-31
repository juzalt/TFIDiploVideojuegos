using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField] BrainChanges player;
    [SerializeField] [Range(0,10)] int secondsToStart = 3;
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] TextMeshProUGUI endGameText;
    [SerializeField] GameObject endGamePanel;


    void OnEnable()
    {
        player.OnFinishGame += EndGame;
    }

    void OnDisable()
    {
        player.OnFinishGame -= EndGame;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }

    void EndGame(bool win)
    {
        endGamePanel.SetActive(true);
        if (win)
        {
            endGameText.text = "Muy bien! \n Ya estudiaste para el examen \n �Quer�s seguir estudiando?";
            PlayerPrefs.SetInt("StudyingGameFinished", 1);
        }
        else
        {
            endGameText.text = "Perdiste la concentraci�n \n �Quer�s seguir estudiando?";
        }
    }

    public void ContinueStudying()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        AudioManager.Instance.PlaySound(AudioManager.Sound.UIClickYes);
    }

    public void GoBackTotMainScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
        AudioManager.Instance.PlaySound(AudioManager.Sound.UIClickNo);
        AudioManager.Instance.ChangeMusic();
    }

    IEnumerator CountDown()
    {
        countdownText.enabled = false;

        for (int i = 0; i < secondsToStart; i++)
        {
            Debug.Log((secondsToStart - i).ToString());
            countdownText.text = (secondsToStart - i).ToString();
            yield return new WaitForSeconds(1);
        }

        countdownText.enabled = false;
    }
}
