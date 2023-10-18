using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] LayerMask interactionLayer = default;
    private Interactable currentInteractable;
       
    void Update()
    {
        ShowMouseHover();
        InteractWithObjects();
    }

    private void InteractWithObjects()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionLayer))
            {
                currentInteractable = hit.collider.GetComponent<Interactable>();
                if (currentInteractable != null)
                {
                    currentInteractable.Interact();
                }
            }
        }
    }

    private void ShowMouseHover()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactionLayer))
        {
            Interactable newInteractable = hit.collider.GetComponent<Interactable>();
            if (newInteractable != null && currentInteractable != newInteractable)
            {
                currentInteractable = newInteractable;
                currentInteractable.OnMouseHover();
            }
        }
        else
        {
            if (currentInteractable != null)
            {
                currentInteractable.OffMouseHover();
            }
            currentInteractable = null;
        }
    }
}
