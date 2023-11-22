using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField] [Range(0,10)] int secondsToStart = 3;
    [SerializeField] TextMeshProUGUI countdownText;

    void Awake()
    {
        StartCoroutine(nameof(StartGame));
    }

    
    void Update()
    {
        
    }

    void StartGame()
    {  
        Time.timeScale = 1f;   
    }

    IEnumerator CountDown()
    {
        countdownText.enabled = false;

        for (int i = 0; i < secondsToStart; i++)
        {
            Debug.Log((secondsToStart - i).ToString());
            countdownText.text = (secondsToStart - i).ToString();
            yield return new WaitForSeconds(1);
        }

        countdownText.enabled = false;
    }
}
