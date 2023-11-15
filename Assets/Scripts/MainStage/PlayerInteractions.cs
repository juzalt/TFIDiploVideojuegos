using UnityEngine;
using UnityEngine.AI;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] LayerMask interactionLayer = default;
    private Interactable currentInteractable;
    NavMeshAgent rudy;
    private Animator animator;
    private Vector3 destination;
    private Vector3 currentPosition;
    

    void Start()
    {
        rudy = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        ShowMouseHover();
        InteractWithObjects();
        Move();
    }

    private void Move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                rudy.destination = hit.point;
                currentPosition = transform.position;
                rudy.updatePosition = false;
                destination = hit.point;
            }
        }

        var turnAngle = Vector3.Angle(transform.forward, destination - transform.position);
        if (turnAngle >= 30)
        {
            var cross = Vector3.Cross(transform.forward, destination - transform.position);
            animator.SetFloat("Turn", cross.normalized.y);
        }
        else
        {
            if (!rudy.updatePosition)
            {
                rudy.updatePosition = true;
                rudy.nextPosition = currentPosition;
                rudy.ResetPath();
                rudy.destination = destination;    
            }   
        }

        animator.SetFloat("Velocity", Mathf.Clamp01(rudy.velocity.magnitude));
        //animator.SetBool("isWalking", rudy.velocity != Vector3.zero);

        //if (Vector3.Distance(transform.position, destination) < 0.1)
        //{
           // animator.SetBool("isWalking", false);
        //}
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
