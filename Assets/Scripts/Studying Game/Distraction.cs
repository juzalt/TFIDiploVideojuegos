using UnityEngine;

public class Distraction : Consumable
{
    [SerializeField] private float baseDistractionMagnitude = 1f;
    [SerializeField] private Vector2 sizeRange;
    [SerializeField] private float damageRate = 1.8f;

    private float distractionMagnitude;
    public float DistractionMagnitude { get => distractionMagnitude; }

    protected override void OnEnable()
    {
        base.OnEnable();
        SetSize();
        SetMagnitude(transform.localScale.x * damageRate);
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position += movementSpeed * Time.deltaTime * MovementDirection;
    }

    void SetSize()
    {
        float xYScale = Random.Range(sizeRange.x, sizeRange.y);
        transform.localScale = new Vector3 (xYScale , xYScale, 1);
    }

    void SetMagnitude(float changeRate)
    {
        distractionMagnitude = (baseDistractionMagnitude + 1) * changeRate;
    }

}
