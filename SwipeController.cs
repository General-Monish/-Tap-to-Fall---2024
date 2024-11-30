using UnityEngine;

public class SwipeController : MonoBehaviour
{
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    private float swipeThreshold = 50f;  // Minimum swipe distance to trigger a swipe

    private bool isSwiping = false;
    private float swipeDirection = 0f;  // 1 for right swipe, -1 for left swipe

    public Transform[] cubes;  // Array to hold the cubes
    private int currentCubeIndex = 0;  // Track the currently active cube
    public float swipeSpeed = 300f;  // Adjust swipe speed

    void Start()
    {
        // Make sure we have cubes assigned
        if (cubes.Length == 0)
        {
            Debug.LogError("No cubes assigned!");
        }
    }

    void Update()
    {
        // Detect swipe touch input
        if (Input.GetMouseButtonDown(0))  // Detect touch or left mouse button press
        {
            touchStartPos = Input.mousePosition;
            isSwiping = true;
        }

        if (Input.GetMouseButtonUp(0))  // Detect touch release or mouse button release
        {
            touchEndPos = Input.mousePosition;

            if (isSwiping)
            {
                DetectSwipeDirection();
            }

            isSwiping = false;
        }

        // Apply swipe movement to switch cubes
        if (swipeDirection != 0)
        {
            SwitchCubes(swipeDirection);
        }
    }

    void DetectSwipeDirection()
    {
        float swipeDistance = touchEndPos.x - touchStartPos.x;

        // Determine swipe direction based on distance
        if (Mathf.Abs(swipeDistance) > swipeThreshold)
        {
            swipeDirection = swipeDistance > 0 ? 1f : -1f;
        }
        else
        {
            swipeDirection = 0f;
        }
    }

    void SwitchCubes(float direction)
    {
        if (direction > 0)
        {
            // Swipe right, go to next cube
            currentCubeIndex = (currentCubeIndex + 1) % cubes.Length;
        }
        else
        {
            // Swipe left, go to previous cube
            currentCubeIndex = (currentCubeIndex - 1 + cubes.Length) % cubes.Length;
        }

        // Show the active cube
        for (int i = 0; i < cubes.Length; i++)
        {
            cubes[i].gameObject.SetActive(i == currentCubeIndex);
        }

        // Optionally, rotate the selected cube
        RotateCube(currentCubeIndex);
    }

    void RotateCube(int cubeIndex)
    {
        // Apply rotation to the selected cube
        cubes[cubeIndex].Rotate(Vector3.up, swipeSpeed * Time.deltaTime);
    }
}
