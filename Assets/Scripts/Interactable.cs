using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public virtual void Awake()
    {
        gameObject.layer = 6;
    }

    public abstract void Interact();

    public void OnMouseHover()
    {
        Debug.Log("Mouse on the object");
    }

    public void OffMouseHover()
    {
        Debug.Log("Mouse off the object");
    }
}
