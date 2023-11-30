using UnityEngine;

public class ClosePopup : MonoBehaviour
{
    static bool openedBefore = false;

    public void Awake()
    {
        Debug.Log("openedBefore::" + openedBefore);
        if (openedBefore)
        {
            this.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    public void Close()
    {
        this.gameObject.transform.parent.gameObject.SetActive(false);
        openedBefore = true;
    }
}
