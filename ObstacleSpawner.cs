using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;       // Reference to the Obstacle prefab
    public float spawnInterval = 2f;        // Time between spawns
    public float spawnHeight = 10f;         // Height at which obstacles spawn
    public Vector2 spawnRange = new Vector2(-5f, 5f); // Range for random horizontal spawn position

    private float timer;

    void Update()
    {
        // Count up the time
        timer += Time.deltaTime;

        // Check if it's time to spawn a new obstacle
        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0f;  // Reset the timer after spawning
        }
    }

    void SpawnObstacle()
    {
        // Randomize the horizontal spawn position
        float spawnX = Random.Range(spawnRange.x, spawnRange.y);
        Vector3 spawnPosition = new Vector3(spawnX, spawnHeight, 0);

        // Instantiate the obstacle at the calculated position
        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    }
}
