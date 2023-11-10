using UnityEngine;

public class Distraction : MonoBehaviour
{
    [SerializeField] private float distractionMagnitude = 1f;
    public float DistractionMagnitude { get => distractionMagnitude; }

    [SerializeField] private float movementSpeed = 2f;

    void Start()
    {
        
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position += movementSpeed * Time.deltaTime * Vector3.right;
    }
}
