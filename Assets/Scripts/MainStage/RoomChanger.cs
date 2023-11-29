using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RoomChanger : MonoBehaviour
{

    [Header("Camera Parameters")]
    [SerializeField] float rotationSmoothness = 5.0f;
    [SerializeField] Vector3 roomEulerRotation;
    [SerializeField] Vector3 livingEulerRotation;

    [Header("Player Parameters")]
    [SerializeField] PlayerInteractions player;
    [SerializeField] Vector3 roomPosition;
    [SerializeField] Vector3 livingPosition;

    [Header("UI components")]
    [SerializeField] Button goToLivingUI;
    [SerializeField] Button goToBedroomUI;


    IEnumerator ChangeCameraPosition(Vector3 eulerRotation)
    {
        float timer = 0;
        Quaternion target = Quaternion.Euler(eulerRotation);
        while (timer < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, target, timer * rotationSmoothness);
            yield return new WaitForSeconds(Time.deltaTime);  
            timer += Time.deltaTime;
        }
           
    }

    public void GoToLiving()
    {
        AudioManager.Instance.PlaySound(AudioManager.Sound.UIClick);
        StartCoroutine(nameof(ChangeCameraPosition), livingEulerRotation);
        player.MoveToPosition(livingPosition);
        goToLivingUI.gameObject.SetActive(false);
        goToBedroomUI.gameObject.SetActive(true);
    }

    public void GoToBedroom()
    {
        AudioManager.Instance.PlaySound(AudioManager.Sound.UIClick);
        StartCoroutine(nameof(ChangeCameraPosition), roomEulerRotation);
        player.MoveToPosition(roomPosition);
        goToLivingUI.gameObject.SetActive(true);
        goToBedroomUI.gameObject.SetActive(false);
    }

}
