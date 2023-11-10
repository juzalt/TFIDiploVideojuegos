using UnityEngine;

public class Distraction : Consumable
{
    [SerializeField] private float distractionMagnitude = 1f;
    public float DistractionMagnitude { get => distractionMagnitude; }
    


    private void OnEnable()
    {
        SetInitialPositionAndMovementDirection();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position += movementSpeed * Time.deltaTime * movementDirection;
    }

    private void OnBecameInvisible()
    {
        if (gameObject.activeSelf)
            DisableDistraction();
    }

    public void DisableDistraction()
    {
        consumablePool.Release(this);
    }
}
