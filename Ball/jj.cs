using UnityEngine;
using UnityEngine.EventSystems;

public class jj : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public RectTransform background;
    public RectTransform handle;
    public Vector2 inputDirection;

    private Canvas canvas;
    private Camera uiCamera;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        uiCamera = canvas.renderMode == RenderMode.ScreenSpaceCamera ? canvas.worldCamera : null;
        background.gameObject.SetActive(false); // Hide joystick initially
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Show joystick and position at touch
        background.gameObject.SetActive(true);

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            background.parent as RectTransform, eventData.position, uiCamera, out localPoint);
        background.anchoredPosition = localPoint;

        handle.anchoredPosition = Vector2.zero; // Reset handle position
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Calculate direction within bounds
        Vector2 position = RectTransformUtility.WorldToScreenPoint(uiCamera, background.position);
        Vector2 radius = background.sizeDelta / 8;
        inputDirection = (eventData.position - position) / radius;

        // Clamp input to unit circle
        inputDirection = inputDirection.magnitude > 1 ? inputDirection.normalized : inputDirection;

        handle.anchoredPosition = inputDirection * radius; // Move handle
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Hide joystick and reset handle
        inputDirection = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
        background.gameObject.SetActive(false);
    }
}
