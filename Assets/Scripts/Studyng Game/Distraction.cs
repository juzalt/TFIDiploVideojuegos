using UnityEngine;

public class Distraction : Consumable
{
    [SerializeField] private float distractionMagnitude = 1f;
    public float DistractionMagnitude { get => distractionMagnitude; }
    

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position += movementSpeed * Time.deltaTime * movementDirection;
    }

}
