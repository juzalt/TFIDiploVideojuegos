using UnityEngine;
using UnityEngine.Pool;

public abstract class Consumable : MonoBehaviour
{
    [SerializeField] protected float movementSpeed = 2f;

    protected IObjectPool<Consumable> consumablePool;
    public IObjectPool<Consumable> ConsumablePool { get => consumablePool; set => consumablePool = value; }

    protected Vector3 movementDirection;

    public void SetInitialPositionAndMovementDirection()
    {
        int side = Random.Range(0, 4);

        Vector2 spawnPoint = Vector2.zero;

        switch (side)
        {
            case 0:
                spawnPoint = new Vector2(0, Random.value);
                movementDirection = new Vector3(1, Random.Range(-1f, 1f), 0);
                break;
            case 1:
                spawnPoint = new Vector2(1, Random.value);
                movementDirection = new Vector3(-1, Random.Range(-1f, 1f), 0);
                break;
            case 2:
                spawnPoint = new Vector2(Random.value, 0);
                movementDirection = new Vector3(Random.Range(-1f, 1f), 1, 0);
                break;
            case 3:
                spawnPoint = new Vector2(Random.value, 1);
                movementDirection = new Vector3(Random.Range(-1f, 1f), -1, 0);
                break;
        }

        Vector3 worldSpawnPoint = Camera.main.ViewportToWorldPoint(spawnPoint);

        worldSpawnPoint.z = 0;

        gameObject.transform.position = worldSpawnPoint;
        Debug.Log("Cambiando posicion inicial");
    }
}
