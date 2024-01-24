using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] LayerMask interactionLayer = default;

    private Interactable currentInteractable;
    private MouseCursor mouseCursor;
    private PlayerMovement player;
    

    void Awake()
    {
        mouseCursor = GetComponent<MouseCursor>();
        player = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        ShowMouseHover();
        InteractWithObjects();
        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }

    private void InteractWithObjects()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionLayer))
            {
                currentInteractable = hit.collider.GetComponent<Interactable>();
                if (currentInteractable != null)
                {
                    AudioManager.Instance.PlaySound(AudioManager.Sound.UIClick);
                    currentInteractable.Interact();
                }
            }
        }
    }

    private void ShowMouseHover()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (IsMouseOverUIWithIgnores())
        {
            mouseCursor.SetInteractableMouseCursor();
        }
        else if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactionLayer) && player.CanMove)
        {
            Interactable newInteractable = hit.collider.GetComponent<Interactable>();
            if (newInteractable != null && currentInteractable != newInteractable)
            {
                currentInteractable = newInteractable;
                mouseCursor.SetInteractableMouseCursor();
                currentInteractable.OnMouseHover();
            }
        }
        else
        {
            if (currentInteractable != null)
            {
                
                currentInteractable.OffMouseHover();
            }
            mouseCursor.SetDefaultMouseCursor();
            currentInteractable = null;
        }
    }

    private bool IsMouseOverUIWithIgnores()
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
