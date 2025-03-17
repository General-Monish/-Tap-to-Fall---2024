using UnityEngine;
using System.Collections;

public class SmallEnemy : MonoBehaviour
{
    private Rigidbody rb;
    private bool isGrounded = false;
    private bool hasDashed = false; // Prevent multiple dashes

    [SerializeField] private float raycastLength = 0.5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float destroyAfterDash = 2f; // Delay before destroying

    private Transform player;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find player
    }

    private void Update()
    {
        CheckGrounded();
    }

    private void CheckGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, raycastLength, groundLayer);

        if (isGrounded && !hasDashed)
        {
            hasDashed = true;
            StartCoroutine(PhantomRush());
        }
    }

    private IEnumerator PhantomRush()
    {
        if (player == null) yield break;

        yield return new WaitForSeconds(Random.Range(0.5f, 2f)); // Random delay

        Vector3 dashDirection = (player.position - transform.position).normalized;
        float randomSpeed = dashSpeed * Random.Range(0.5f, 0.8f); // Slight speed variation

        rb.AddForce(dashDirection * randomSpeed, ForceMode.Impulse);

        yield return new WaitForSeconds(destroyAfterDash);
        Destroy(gameObject);
    }

}
