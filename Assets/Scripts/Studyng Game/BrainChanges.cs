using UnityEngine;

public class BrainChanges : MonoBehaviour
{
    [SerializeField] private float initialConcentration = 10f;
    [SerializeField] private float endGameKnowledge = 10f;

    private float concentration;
    private float knowledge;

    void Start()
    {
        concentration = initialConcentration;
        knowledge = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Distraction")
        {
            Distraction distraction = collision.gameObject.GetComponent<Distraction>();
            LoseConcentration(distraction.DistractionMagnitude);
            distraction.DisableConsumable();
        }
        else if (collision.gameObject.tag == "Knowledge")
        {
            Knowledge knowledge = collision.gameObject.GetComponent<Knowledge>();
            GainKnowledge(knowledge.KnowledgeMagnitude);
            knowledge.DisableConsumable();
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

    void GainKnowledge(float knowledgeMagnitude)
    {
        knowledge += knowledgeMagnitude;
        Debug.Log(knowledge);
        if (knowledge >= endGameKnowledge)
        {
            Debug.Log("You are ready for the test");
        }
    }
}
