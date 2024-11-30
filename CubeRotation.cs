using UnityEngine;

public class CubeRotation : MonoBehaviour
{
    public float rotationSpeed = 50f;

    // Update is called once per frame
    void Update()
    {
        // Apply rotation around a diagonal axis (X and Z)
        // This simulates the behavior of the cube spinning from one of its edges
        transform.Rotate(new Vector3(-rotationSpeed, -rotationSpeed, 0) * Time.deltaTime);
    }
}
