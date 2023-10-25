using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Apple;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] LayerMask interactionLayer = default;
    private Interactable currentInteractable;
    NavMeshAgent rudy;
    private Animator animator;
    private Vector3 destination;

    void Start()
    {
        rudy = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        ShowMouseHover();
        InteractWithObjects();
        
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                animator.SetBool("isWalking", true);
                rudy.destination = hit.point;
                destination = hit.point;
            }
        }

        if (Vector3.Distance(transform.position, destination) < 0.1)
        {
            animator.SetBool("isWalking", false);
        }
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
