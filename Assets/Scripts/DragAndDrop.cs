using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] Camera camera;

    public GameObject selectedObject;
    Vector3 offset;

    void Update()
    {
        //Debug.Log(Input.mousePosition);
        Vector3 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);

            if (targetObject)
            {
                selectedObject = targetObject.transform.gameObject;
                Debug.Log(selectedObject);
                offset = selectedObject.transform.position - mousePosition;
            }
        }

        if (selectedObject)
        {
            selectedObject.transform.position = mousePosition + offset;
        }

        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            selectedObject = null;
        }
    }
}