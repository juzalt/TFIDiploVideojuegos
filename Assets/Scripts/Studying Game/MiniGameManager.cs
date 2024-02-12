using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField] BrainChanges player;
    [SerializeField] [Range(0,10)] int secondsToStart = 3;
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] TextMeshProUGUI endGameText;
    [SerializeField] GameObject endGamePanel;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Button pauseMenuQuitBtn;
    [SerializeField] Button pauseMenuContinueBtn;

    private MouseCursor mouseCursor;

    void OnEnable()
    {
        player.OnFinishGame += EndGame;
    }

    void OnDisable()
    {
        player.OnFinishGame -= EndGame;
    }

    private void Awake()
    {
        mouseCursor = GetComponent<MouseCursor>();
    }

    private void Start()
    {
        mouseCursor.HideCursor();

        pauseMenuQuitBtn.onClick.AddListener(() => GoBackToMainScene(1));
        pauseMenuContinueBtn.onClick.AddListener(() => ResumeGame());
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        mouseCursor.UpdateMouseCursor();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeInHierarchy)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void GoBackToMainScene(int sceneID)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneID);
        AudioManager.Instance.PlaySound(AudioManager.Sound.UIClickNo);
        AudioManager.Instance.ChangeMusic();
    }

    void EndGame(bool win)
    {
        endGamePanel.SetActive(true);
        mouseCursor.ShowCursor();
        if (win)
        {
            endGameText.text = "Muy bien! \n Ya estudiaste para el examen \n ¿Querés seguir estudiando?";
            PlayerPrefs.SetInt("StudyingGameFinished", 1);
        }
        else
        {
            endGameText.text = "Perdiste la concentración \n ¿Querés seguir estudiando?";
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
