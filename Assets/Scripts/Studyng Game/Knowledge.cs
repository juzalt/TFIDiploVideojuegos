using UnityEngine;

public class Knowledge : Consumable
{
    [SerializeField] private float knowledgeMagnitude = 1f;
    public float KnowledgeMagnitude { get => knowledgeMagnitude; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position += movementSpeed * Time.deltaTime * movementDirection;
    }
}
