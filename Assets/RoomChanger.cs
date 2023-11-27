using System.Collections;
using UnityEngine;

public class RoomChanger : MonoBehaviour
{

    [SerializeField] float rotationSmoothness = 5.0f;
    [SerializeField] Vector3 roomEulerRotation;
    [SerializeField] Vector3 livingEulerRotation;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(nameof(ChangeCameraPosition), livingEulerRotation);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(nameof(ChangeCameraPosition), roomEulerRotation);
        }
    }

    IEnumerator ChangeCameraPosition(Vector3 eulerRotation)
    {
        float timer = 0;
        Quaternion target = Quaternion.Euler(eulerRotation);
        while (timer < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, target, timer * rotationSmoothness);
            Debug.Log(transform.rotation);
            yield return new WaitForSeconds(Time.deltaTime);
            Debug.Log(timer);
            timer += Time.deltaTime;
        }
        Debug.Log("end");    
    }
}
