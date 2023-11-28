using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void QuitGame()
    {
        AudioManager.Instance.PlayUIClickSound();
        Debug.Log("Quit");
        Application.Quit();
    }

    public void MoveToScene(int sceneID)
    {
        AudioManager.Instance.PlayUIClickSound();
        SceneManager.LoadScene(sceneID);
    }
}
