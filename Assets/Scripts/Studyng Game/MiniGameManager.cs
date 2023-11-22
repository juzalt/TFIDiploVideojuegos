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

    void Awake()
    {
        
    }

    void OnEnable()
    {
        player.OnFinishGame += EndGame;
    }

    void OnDisable()
    {
        player.OnFinishGame -= EndGame;
    }

    void Update()
    {
        
    }


    void EndGame(bool win)
    {
        endGamePanel.SetActive(true);
        if (win)
        {
            endGameText.text = "Muy bien! \n Ya estudiaste para el examen \n �Quer�s seguir estudiando?";
        }
        else
        {
            endGameText.text = "Perdiste la concentraci�n \n �Quer�s seguir estudiando?";
        }
    }

    public void ContinueStudying()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoBackTotMainScene()
    {
        SceneManager.LoadScene(0);
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
