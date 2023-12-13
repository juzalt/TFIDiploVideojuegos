using UnityEngine;

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
}
