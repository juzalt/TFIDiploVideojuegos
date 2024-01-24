using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections.Generic;

public class MouseCursor : MonoBehaviour
{
    [SerializeField] Texture2D defaultMouseCursor;
    [SerializeField] Texture2D interactableMouseCursor;

    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;

    public void SetDefaultMouseCursor()
    {
        Cursor.SetCursor(defaultMouseCursor, hotSpot, cursorMode);
    }

    public void SetInteractableMouseCursor()
    {
        Cursor.SetCursor(interactableMouseCursor, hotSpot, cursorMode);
    }

    public bool IsMouseOverUIWithIgnores()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> rayCastResultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, rayCastResultList);

        for (int i = 0; i < rayCastResultList.Count; i++)
        {

            bool condition1 = !(rayCastResultList[i].gameObject.layer == LayerMask.NameToLayer("UI"));
            bool condition2 = rayCastResultList[i].gameObject.GetComponent<MouseCursorIgnore>() != null;
            if (condition1 || condition2)
            {
                rayCastResultList.RemoveAt(i);
                i--;
            }
        }
        return rayCastResultList.Count > 0;
    }
}
