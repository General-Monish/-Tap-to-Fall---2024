using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public RectTransform background;
    public RectTransform handle;
    public Vector2 inputDirection;

    private Vector2 originalPosition;

    void Start()
    {
        originalPosition = handle.anchoredPosition; // Store the initial position of the handle
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Calculate the drag direction within the background bounds
        Vector2 position = RectTransformUtility.WorldToScreenPoint(null, background.position);
        Vector2 radius = background.sizeDelta / 5;
        inputDirection = (eventData.position - position) / radius;

        // Clamp the input direction to a circle
        inputDirection = inputDirection.magnitude > 1 ? inputDirection.normalized : inputDirection;

        // Move the handle accordingly
        handle.anchoredPosition = inputDirection * radius;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Reset the joystick
        inputDirection = Vector2.zero;
        handle.anchoredPosition = originalPosition;
    }
}