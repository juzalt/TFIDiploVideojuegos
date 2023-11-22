using System.Collections;
using UnityEngine;
using TMPro;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField] BrainChanges player;
    [SerializeField] [Range(0,10)] int secondsToStart = 3;
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] TextMeshProUGUI endGameText;

    void Awake()
    {
        
    }

    void OnEnable()
    {
        player.OnFinishGame += EndGame;
    }

    void OnDisable()
    {
        player.OnFinishGame -= EndGame;
    }

    void Update()
    {
        
    }


    void EndGame(bool win)
    {
        if (win)
        {
            endGameText.text = "You Are Ready for The Test";
        }
        else
        {
            endGameText.text = "You Lost Your Concentration";
        }
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
