using UnityEngine;
using UnityEngine.Pool;

public class Consumable : MonoBehaviour
{
    [SerializeField] Vector2 movementSpeedMinMax;
    protected float movementSpeed = 2f;

    protected IObjectPool<Consumable> consumablePool;
    public IObjectPool<Consumable> ConsumablePool { get => consumablePool; set => consumablePool = value; }

    protected Vector3 movementDirection;

    protected virtual void OnEnable()
    {
        DecideMovementSpeed();
    }

    void DecideMovementSpeed()
    {
        movementSpeed = Random.Range(movementSpeedMinMax.x, movementSpeedMinMax.y);
    }

    private void OnBecameInvisible()
    {
        if (gameObject.activeSelf)
            DisableConsumable();
    }

    public void DisableConsumable()
    {
        consumablePool.Release(this);
    }
}
