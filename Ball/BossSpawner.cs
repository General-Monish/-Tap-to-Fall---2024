using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab; // Assign the large boss prefab
    public Transform spawnPoint; // Assign the spawn location

    public void SpawnBoss()
    {
        Instantiate(bossPrefab, spawnPoint.position, bossPrefab.transform.rotation);
        Debug.Log("Boss Spawned!");
    }
}
