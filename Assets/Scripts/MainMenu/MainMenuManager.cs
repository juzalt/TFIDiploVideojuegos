using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.SetInt("StoryProgress", 0);
        PlayerPrefs.SetInt("OrderGameFinished", 0);
        PlayerPrefs.SetInt("StudyingGameFinished", 0);
        AudioManager.Instance.ChangeMusic();
    }

    public void QuitGame()
    {
        AudioManager.Instance.PlaySound(AudioManager.Sound.UIClick);
        Debug.Log("Quit");
        Application.Quit();
    }

    public void MoveToScene(int sceneID)
    {
        AudioManager.Instance.PlaySound(AudioManager.Sound.UIClick);
        SceneManager.LoadScene(sceneID);
    }
}
