using UnityEngine;
using UnityEngine.InputSystem;

public class BrainMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float maxSpeed = 1;
    private Rigidbody2D rb;
    private PlayerInput playerInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
        if (input != Vector2.zero)
        {
            rb.AddForce(input.normalized * speed);
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        }
    }
}
