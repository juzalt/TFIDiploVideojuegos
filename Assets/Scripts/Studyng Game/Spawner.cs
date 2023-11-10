using System;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] Distraction distractionPrefab;
    private IObjectPool<Distraction> distractionPool;

    private void Awake()
    {
        distractionPool = new ObjectPool<Distraction>(
            CreateDistraction,
            OnGet,
            OnRelease);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            distractionPool.Get();
        }
    }

    private Distraction CreateDistraction()
    {
        Distraction distraction = Instantiate(distractionPrefab, transform.position, Quaternion.identity);
        distraction.DistractionPool = distractionPool;
        return distraction;
    }

    private void OnGet(Distraction distraction)
    {
        distraction.gameObject.SetActive(true);
        distraction.transform.position = transform.position;
    }

    private void OnRelease(Distraction distraction)
    {
        distraction.gameObject.SetActive(false);
    }
}
