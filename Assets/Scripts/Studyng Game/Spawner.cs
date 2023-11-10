using System;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] Consumable distractionPrefab;
    [SerializeField] float timeBetweenSpawns = 2f;

    [Header("Parameters to change spawning rate over time")]
    [SerializeField] bool spawningGetsFasterWithTime;
    [Tooltip("Only matters if spawningGetsFasterWithTime is true")]
    [SerializeField] private float spawningChangingRate;
    [Tooltip("Only matters if spawningGetsFasterWithTime is true")]
    [SerializeField] private float minSpawningRate;


    private float timerForSpawning = 0;
    
    private IObjectPool<Consumable> distractionPool;
    
    private void Awake()
    {
        distractionPool = new ObjectPool<Consumable>(
            CreateConsumable,
            OnGet,
            OnRelease,
            OnDestroyConsumable,
            maxSize:10);
    }

    private void Update()
    {
        UpdateTimerandSpawnObjects();
    }

    private void UpdateTimerandSpawnObjects()
    {
        timerForSpawning += Time.deltaTime;
        if (timerForSpawning > timeBetweenSpawns)
        {
            distractionPool.Get();
            timerForSpawning = 0;

            if (spawningGetsFasterWithTime && 
                timeBetweenSpawns - spawningChangingRate >= minSpawningRate)
            {
                timeBetweenSpawns -= spawningChangingRate;
            }
        }
    }

    private Consumable CreateConsumable()
    {
        Consumable distraction = Instantiate(distractionPrefab);
        distraction.ConsumablePool = distractionPool;
        return distraction;
    }

    private void OnGet(Consumable distraction)
    {
        distraction.gameObject.SetActive(true);
    }

    private void OnRelease(Consumable distraction)
    {
        distraction.gameObject.SetActive(false);
    }

    private void OnDestroyConsumable(Consumable distraction)
    {
        Destroy(distraction.gameObject);
    }
}
