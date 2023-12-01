using UnityEngine;

public class ClosePopup : MonoBehaviour
{
    static bool openedBefore = false;

    public void Awake()
    {
        Debug.Log("openedBefore::" + openedBefore);
        if (openedBefore)
        {
            gameObject.SetActive(false);
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
        openedBefore = true;
    }
}
