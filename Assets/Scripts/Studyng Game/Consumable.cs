using UnityEngine;
using UnityEngine.Pool;

public class Consumable : MonoBehaviour
{
    [SerializeField] Vector2 movementSpeedMinMax;
    protected float movementSpeed = 2f;

    protected IObjectPool<Consumable> consumablePool;
    public IObjectPool<Consumable> ConsumablePool { get => consumablePool; set => consumablePool = value; }

    private Vector3 movementDirection;
    public Vector3 MovementDirection { get => movementDirection; set => movementDirection = value; }

    protected virtual void OnEnable()
    {
        DecideMovementSpeed();
        SetInitialPositionAndMovementDirection();
    }

    void DecideMovementSpeed()
    {
        movementSpeed = Random.Range(movementSpeedMinMax.x, movementSpeedMinMax.y);
    }

    public virtual void SetInitialPositionAndMovementDirection()
    {
        int side = Random.Range(0, 4);

        Vector2 spawnPoint = Vector2.zero;
        float spawnPointLooseCoordinate = Random.value;
        float movementLooseDirection;

        // Improve movement direction so distraction dont go directly out of the screen
        if (spawnPointLooseCoordinate < 0.25f)
        {
            movementLooseDirection = Random.Range(0, 1f);
        }
        else if (spawnPointLooseCoordinate < 0.75f)
        {
            movementLooseDirection = Random.Range(-1f, 1f);
        }
        else
        {
            movementLooseDirection = Random.Range(-1f, 0f);
        }

        if (Random.value < 0.3) { movementLooseDirection = 0; } // Put it randomly vertical or Horizontal direction

        switch (side)
        {
            case 0:
                spawnPoint = new Vector2(0, spawnPointLooseCoordinate);
                MovementDirection = new Vector3(1, movementLooseDirection, 0);
                break;
            case 1:
                spawnPoint = new Vector2(1, spawnPointLooseCoordinate);
                MovementDirection = new Vector3(-1, movementLooseDirection, 0);
                break;
            case 2:
                spawnPoint = new Vector2(spawnPointLooseCoordinate, 0);
                MovementDirection = new Vector3(movementLooseDirection, 1, 0);
                break;
            case 3:
                spawnPoint = new Vector2(spawnPointLooseCoordinate, 1);
                MovementDirection = new Vector3(movementLooseDirection, -1, 0);
                break;
        }

        Vector3 worldSpawnPoint = Camera.main.ViewportToWorldPoint(spawnPoint);

        worldSpawnPoint.z = 0;

        gameObject.transform.position = worldSpawnPoint;
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
