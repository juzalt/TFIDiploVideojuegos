using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePopup : MonoBehaviour
{
    public void Close()
    {
        this.gameObject.transform.parent.gameObject.SetActive(false);
    }
}
