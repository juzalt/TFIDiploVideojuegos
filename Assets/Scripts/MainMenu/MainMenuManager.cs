using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
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
