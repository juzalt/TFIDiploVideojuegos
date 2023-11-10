using UnityEngine;
using UnityEngine.Pool;

public class Distraction : MonoBehaviour
{
    [SerializeField] private float distractionMagnitude = 1f;
    public float DistractionMagnitude { get => distractionMagnitude; }
    

    [SerializeField] private float movementSpeed = 2f;

    private IObjectPool<Distraction> distractionPool;
    public IObjectPool<Distraction> DistractionPool { get => distractionPool; set => distractionPool = value; }


    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position += movementSpeed * Time.deltaTime * Vector3.right;
    }

    private void OnBecameInvisible()
    {
        if (gameObject.activeSelf)
            DisableDistraction();
    }

    public void DisableDistraction()
    {
        distractionPool.Release(this);
    }
}
