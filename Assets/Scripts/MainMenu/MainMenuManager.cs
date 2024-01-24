using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private MouseCursor mouseCursor;

    private void Awake()
    {
        mouseCursor = GetComponent<MouseCursor>();
    }

    private void Start()
    {
        PlayerPrefs.SetInt("StoryProgress", 0);
        PlayerPrefs.SetInt("OrderGameFinished", 0);
        PlayerPrefs.SetInt("StudyingGameFinished", 0);
        AudioManager.Instance.ChangeMusic();
    }

    private void Update()
    {
        UpdateMouseCursor();
    }

    public void QuitGame()
    {
        AudioManager.Instance.PlaySound(AudioManager.Sound.UIClick);
        Debug.Log("Quit");
        Application.Quit();
    }

    public void Options()
    {
        Debug.Log("falta hacer las opciones");
    }

    public void MoveToScene(int sceneID)
    {
        AudioManager.Instance.PlaySound(AudioManager.Sound.UIClick);
        SceneManager.LoadScene(sceneID);
    }

    private void UpdateMouseCursor()
    {
        if (mouseCursor.IsMouseOverUIWithIgnores())
        {
            mouseCursor.SetInteractableMouseCursor();
        }
        else
        {
            mouseCursor.SetDefaultMouseCursor();
        }
    }
}
