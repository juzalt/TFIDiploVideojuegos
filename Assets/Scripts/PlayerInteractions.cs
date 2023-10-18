using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] LayerMask interactionLayer = default;
    private Interactable currentInteractable;
       
    void Update()
    {
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
}
