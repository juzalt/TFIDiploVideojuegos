using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BrainChanges : MonoBehaviour
{
    [SerializeField] private float initialConcentration = 10f;
    [SerializeField] private float endGameKnowledge = 10f;
    [SerializeField] [Range(1, 2)] private float brainGrowthRate = 1.2f;
    [SerializeField] private float inmunityTime = 1f;

    [Header("UI")]
    [SerializeField] Slider concentrationSlider;
    [SerializeField] Slider knowledgeSlider;

    private float concentration;
    private float knowledge;

    private bool isInmune;
    

    void Start()
    {
        concentration = initialConcentration;
        knowledge = 0;
        UpdateConcentrationUI();
        UpdateKnowledgeUI();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Distraction" && !isInmune)
        {
            StartCoroutine(nameof(GainInmunity));
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
        UpdateConcentrationUI();

        if (concentration <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator GainInmunity()
    {
        isInmune = true;

        yield return new WaitForSeconds(inmunityTime);

        isInmune = false;
    }

    void GainKnowledge(float knowledgeMagnitude)
    {
        knowledge += knowledgeMagnitude;
        transform.localScale = transform.localScale * brainGrowthRate;
        UpdateKnowledgeUI();
        if (knowledge >= endGameKnowledge)
        {
            Debug.Log("You are ready for the test");
        }
    }

    void UpdateConcentrationUI()
    {
        concentrationSlider.value = concentration / initialConcentration;
        if (concentration <= 0)
        {
            concentrationSlider.fillRect.gameObject.SetActive(false);
        }
    }

    void UpdateKnowledgeUI()
    {
        knowledgeSlider.value = knowledge / endGameKnowledge;
        if (knowledge <= 0)
        {
            knowledgeSlider.fillRect.gameObject.SetActive(false);
        }
        else if (!knowledgeSlider.fillRect.gameObject.activeSelf)
        {
            knowledgeSlider.fillRect.gameObject.SetActive(true);
        }
    }
}
