using UnityEngine;

public class Distraction : Consumable
{
    [SerializeField] private float baseDistractionMagnitude = 1f;
    [SerializeField] private Vector2 sizeRange;

    private float distractionMagnitude;
    public float DistractionMagnitude { get => distractionMagnitude; }

    protected override void OnEnable()
    {
        base.OnEnable();
        SetInitialPositionAndMovementDirection();
        SetSize();
        SetMagnitude(transform.localScale.x);
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position += movementSpeed * Time.deltaTime * movementDirection;
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

    public void SetInitialPositionAndMovementDirection()
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
                movementDirection = new Vector3(1, movementLooseDirection, 0);
                break;
            case 1:
                spawnPoint = new Vector2(1, spawnPointLooseCoordinate);
                movementDirection = new Vector3(-1, movementLooseDirection, 0);
                break;
            case 2:
                spawnPoint = new Vector2(spawnPointLooseCoordinate, 0);
                movementDirection = new Vector3(movementLooseDirection, 1, 0);
                break;
            case 3:
                spawnPoint = new Vector2(spawnPointLooseCoordinate, 1);
                movementDirection = new Vector3(movementLooseDirection, -1, 0);
                break;
        }

        Vector3 worldSpawnPoint = Camera.main.ViewportToWorldPoint(spawnPoint);

        worldSpawnPoint.z = 0;

        gameObject.transform.position = worldSpawnPoint;
    }

}
