using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{
    public BossState currentState = BossState.Idle;

    public float jumpForce = 10f;
    public float jumpInterval = 3f;
    public LayerMask groundLayer;
    private Rigidbody rb;

    [SerializeField] int fallSpeed = 10;
    [SerializeField] float raycastRay = 4f;

    public GameObject smallEnemyPrefab;
    public int numberOfSmallEnemies = 5;
    public float spawnRadius = 3f;
    public float spawnForce = 5f;

    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        InvokeRepeating("Jump", 2f, jumpInterval);
    }

    private void Update()
    {
        /*Debug.DrawRay(transform.position, Vector3.down * 5f, Color.red);*/
        isGrounded = Physics.Raycast(transform.position, Vector3.down, raycastRay, groundLayer);

        if (currentState == BossState.Jumping && rb.velocity.y < 0) // Falling Down
        {
            Slam();
        }

        if (isGrounded && currentState == BossState.Slamming) // When boss lands
        {
            Recover();
        }
    }

    private void Jump()
    {
        if (currentState == BossState.Idle && isGrounded) // Jump only when Idle
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            currentState = BossState.Jumping;
            Debug.Log("Boss is Jumping!");
        }
    }

    private void Slam()
    {
        if (currentState == BossState.Jumping) // Slam only when in air
        {
            rb.velocity = new Vector3(0, -fallSpeed, 0); // Only allow downward force
            currentState = BossState.Slamming;
            Debug.Log("Boss is Slamming!");
            StartCoroutine(CameraShake());
        }
    }


    private void Recover()
    {
        if (currentState == BossState.Slamming) // Recover after hitting ground
        {
            currentState = BossState.Recovering;
            Debug.Log("Boss is Recovering!");
            OnGroundImpact();
            Invoke("ResetToIdle", 1.5f); // Delay before returning to Idle
        }
    }

    private void ResetToIdle()
    {
        if (currentState == BossState.Recovering) // Return to idle after recovery
        {
            currentState = BossState.Idle;
            Debug.Log("Boss is Idle Again!");
        }
    }

    IEnumerator CameraShake()
    {
        Vector3 originalLocalPos = Camera.main.transform.localPosition;
        float duration = 0.2f;
        float magnitude = 0.3f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-magnitude, magnitude);
            float y = Random.Range(-magnitude, magnitude);
            Camera.main.transform.localPosition = originalLocalPos + new Vector3(x, y, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Camera.main.transform.localPosition = originalLocalPos; // Reset to original position
    }

    private void OnGroundImpact()
    {
        SpawnSmallEnemies();
    }

    private void SpawnSmallEnemies()
    {
        for (int i = 0; i < numberOfSmallEnemies; i++)
        {
            Vector3 spawnPosition = transform.position + new Vector3(
                Random.Range(-spawnRadius, spawnRadius),
                0.5f,
                Random.Range(-spawnRadius, spawnRadius)
            );

            GameObject smallEnemy = Instantiate(smallEnemyPrefab, spawnPosition, Quaternion.identity);

            Rigidbody rb = smallEnemy.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(Vector3.up * spawnForce, ForceMode.Impulse);
            }
        }
    }
}
