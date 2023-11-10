using System;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] Consumable distractionPrefab;
    private IObjectPool<Consumable> distractionPool;

    private void Awake()
    {
        distractionPool = new ObjectPool<Consumable>(
            CreateDistraction,
            OnGet,
            OnRelease,
            OnDestroyDistraction,
            maxSize:10);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            distractionPool.Get();
        }
    }

    private Consumable CreateDistraction()
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

    private void OnDestroyDistraction(Consumable distraction)
    {
        Destroy(distraction.gameObject);
    }
}
