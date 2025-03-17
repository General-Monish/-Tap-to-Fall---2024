using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool IsGrounded()
    {
        RaycastHit hit;
        float distance = 1.1f; // Adjust based on character size
        Vector3 origin = transform.position + Vector3.up * 0.1f; // Slightly above ground

        if (Physics.Raycast(origin, Vector3.down, out hit, distance))
        {
            return true;
        }
        return false;
    }
}
