using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public RectTransform background;
    public RectTransform handle;
    public CanvasGroup canvasGroup; // To control joystick visibility
    public Vector2 inputDirection;

    private Vector2 originalPosition;

    void Start()
    {
        originalPosition = handle.anchoredPosition; // Store the initial position of the handle
        canvasGroup.alpha = 0; // Hide the joystick initially
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(" User Touched");
        // Show the joystick
        canvasGroup.alpha = 1;

        // Center the joystick on the touch position
        Vector2 touchPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)background.parent, eventData.position, eventData.pressEventCamera, out touchPosition);
        background.anchoredPosition = touchPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Calculate the drag direction within the background bounds
        Vector2 position = RectTransformUtility.WorldToScreenPoint(null, background.position);
        Vector2 radius = background.sizeDelta / 2;
        inputDirection = (eventData.position - position) / radius;

        // Clamp the input direction to a circle
        inputDirection = inputDirection.magnitude > 1 ? inputDirection.normalized : inputDirection;

        // Move the handle accordingly
        handle.anchoredPosition = inputDirection * radius;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Reset the joystick
        inputDirection = Vector2.zero;
        handle.anchoredPosition = originalPosition;

        // Hide the joystick
        canvasGroup.alpha = 0;
    }
}
