using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public RectTransform background; // Joystick background
    public RectTransform handle;     // Joystick handle
    public Vector2 inputDirection;   // Direction of joystick movement

    private Vector2 originalPosition; // Initial handle position

    void Start()
    {
        originalPosition = handle.anchoredPosition; // Store the initial position of the handle
        background.gameObject.SetActive(false);     // Hide the joystick at the start
    }

    void Update()
    {
        // Check for touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Activate and position the joystick at the touch point
                background.gameObject.SetActive(true);
                background.position = touch.position;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Calculate the drag direction within the background bounds
        Vector2 position = RectTransformUtility.WorldToScreenPoint(null, background.position);
        Vector2 radius = background.sizeDelta / 2; // Use half size as radius
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

        // Hide the joystick background
        background.gameObject.SetActive(false);
    }
}
