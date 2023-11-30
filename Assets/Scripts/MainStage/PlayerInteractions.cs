using UnityEngine;
using UnityEngine.AI;

public class PlayerInteractions : MonoBehaviour
{
    [Header("Layer Mask Settings")]
    [SerializeField] LayerMask interactionLayer = default;
    [SerializeField] LayerMask environmentLayer = default;

    [SerializeField] GameObject playerModel;
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
        AdjustPlayerYPosition();
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
                if (rudy.velocity == Vector3.zero)
                    rudy.updatePosition = false;
                destination = hit.point;
            }
        }

        Turn();

        animator.SetFloat("Velocity", Mathf.Clamp01(rudy.velocity.magnitude));
    }

    public void MoveToPosition(Vector3 position)
    {
        rudy.destination = position;
        currentPosition = transform.position;
        if (rudy.velocity == Vector3.zero)
            rudy.updatePosition = false;
        destination = position;

        Turn();

        animator.SetFloat("Velocity", Mathf.Clamp01(rudy.velocity.magnitude));
    }

    private void AdjustPlayerYPosition()
    {
        playerModel.transform.position = new Vector3(playerModel.transform.position.x, 0, playerModel.transform.position.z);
    }

    private void Turn()
    {
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
                rudy.destination = destination;
            }
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
                    AudioManager.Instance.PlaySound(AudioManager.Sound.UIClick);
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
