using UnityEngine;

public class RotateCam : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.up, 0.1f);
    }
}
