using UnityEngine;

public class Knowledge : Consumable
{
    [SerializeField] private float knowledgeMagnitude = 1f;
    public float KnowledgeMagnitude { get => knowledgeMagnitude; }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position += movementSpeed * Time.deltaTime * movementDirection;
    }

}
