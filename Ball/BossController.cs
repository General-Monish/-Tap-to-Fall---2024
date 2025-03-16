using UnityEngine;

public class BossController : MonoBehaviour
{
    public float jumpForce = 10f;
    public float jumpInterval = 3f;
    public LayerMask groundLayer; // Assign the Ground Layer in Inspector

    private Rigidbody rb;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        InvokeRepeating("Jump", 2f, jumpInterval);
    }

    void Update()
    {
        Debug.DrawRay(transform.position, Vector3.down * 5f, Color.red);
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 5f, groundLayer);
        Debug.Log("Is Grounded: " + isGrounded);
    }


    private void Jump()
    {
        if (isGrounded) // Jump only if on the ground
        {
            Debug.Log("Jump function triggered!");

            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("Boss Jumping!");
        }
    }
}
