using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    Color glowColor = new(0.4f, 0.4f, 0.4f);
    float glowfactor = 1.3f;
    MeshRenderer meshRenderer;

    public virtual void Awake()
    {
        gameObject.layer = 6;
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public abstract void Interact();

    public void OnMouseHover()
    {
        Debug.Log("Mouse on the object");
        meshRenderer.materials[1].SetColor("_EmissionColor", glowColor * glowfactor);
    }

    public void OffMouseHover()
    {
        Debug.Log("Mouse off the object");
        meshRenderer.materials[1].SetColor("_EmissionColor", Color.black);
    }
}
