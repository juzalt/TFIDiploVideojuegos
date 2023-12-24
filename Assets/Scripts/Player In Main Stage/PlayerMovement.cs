using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] LayerMask interactionLayer = default;

    [SerializeField] GameObject playerModel;

    private Interactable currentInteractable;
    UnityEngine.AI.NavMeshAgent rudy;
    private Animator animator;
    private Vector3 destination;
    private Vector3 currentPosition;
    private MouseCursor mouseCursor;
    private bool canMove = true;

    public bool CanMove { get => canMove; set => canMove = value; }

    void Awake()
    {
        rudy = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        mouseCursor = GetComponent<MouseCursor>();
    }

    void Update()
    {
        Move();
        AdjustPlayerYPosition();
        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }

    private void Move()
    {
        if (Input.GetMouseButtonDown(0) && canMove)
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
}
