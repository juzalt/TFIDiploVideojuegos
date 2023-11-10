
using UnityEngine;

public class BrainConcentration : MonoBehaviour
{
    [SerializeField] private float initialConcentration = 10f;

    private float concentration;

    void Start()
    {
        concentration = initialConcentration;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Distraction")
        {
            Distraction distraction = collision.gameObject.GetComponent<Distraction>();
            LoseConcentration(distraction.DistractionMagnitude);
            distraction.DisableDistraction();
        }
    }

    void LoseConcentration(float distractionMagnitude)
    {
        concentration -= distractionMagnitude;
        Debug.Log(concentration);
        if (concentration <= 0)
        {
            Destroy(gameObject);
        }
    }
}
