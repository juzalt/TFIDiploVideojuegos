using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BrainChanges : MonoBehaviour
{
    [SerializeField] private float initialConcentration = 10f;
    [SerializeField] private float endGameKnowledge = 10f;
    [SerializeField] [Range(1, 2)] private float brainGrowthRate = 1.2f;
    
    [Header("Time Inmunity when distracted")]
    [SerializeField] private float inmunityTime = 1f;
    [SerializeField] private Material blinkingMaterial;
    [SerializeField] [Range(1, 100)]  private float blinkingRate;
    private bool isInmune;
    private SpriteRenderer brainSR;
    private Material brainMaterial;

    [Header("UI")]
    [SerializeField] Slider concentrationSlider;
    [SerializeField] Slider knowledgeSlider;

    private float concentration;
    private float knowledge;

    public event Action OnFinishGame;
    

    void Start()
    {
        concentration = initialConcentration;
        knowledge = 0;
        UpdateConcentrationUI();
        UpdateKnowledgeUI();
        brainSR = GetComponent<SpriteRenderer>();
        brainMaterial = brainSR.material;
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
            if (OnFinishGame != null)
                OnFinishGame();
            Destroy(gameObject);
        }
    }

    IEnumerator GainInmunity()
    {
        isInmune = true;
        StartCoroutine(nameof(InmunityBlink));

        yield return new WaitForSeconds(inmunityTime);

        isInmune = false;
    }

    IEnumerator InmunityBlink()
    {
        float timer = 0;
        while (timer < inmunityTime)
        {
            brainSR.material = blinkingMaterial;

            yield return new WaitForSeconds(inmunityTime / (blinkingRate * 2));
            
            brainSR.material = brainMaterial;
            
            yield return new WaitForSeconds(inmunityTime / (blinkingRate * 2));
            
            timer += inmunityTime / blinkingRate;
        }
    }

    void GainKnowledge(float knowledgeMagnitude)
    {
        knowledge += knowledgeMagnitude;
        transform.localScale = transform.localScale * brainGrowthRate;
        UpdateKnowledgeUI();
        if (knowledge >= endGameKnowledge)
        {
            if (OnFinishGame != null)
                OnFinishGame();
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
