using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Smanager : MonoBehaviour
{
    public static Smanager Instance;
    private PlayerController playerController;

    [SerializeField] GameObject powerupPrefab;
    [SerializeField] GameObject enemeyPrefab;
    [SerializeField] GameObject bossPrefab; // Add boss reference

    public float totalEnemiesToSpawn; // Total enemies in a wave
    float spawnPos = 9f;
    public int enemyCount;
    public int waveNum = 5;
    private bool bossSpawned = false; // Track if boss has been spawned

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        playerController = GameObject.FindAnyObjectByType<PlayerController>();
    }

    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount == 0)
        {
            if (waveNum < 14) // Before final wave, continue normal waves , level 10 set wave < 14
            {
                Instantiate(powerupPrefab, GenerateRandomPosition(), powerupPrefab.transform.rotation);
                waveNum++;
                totalEnemiesToSpawn = waveNum;
                SpawnEnemyWave(waveNum);
                UIManager.instance.DisplayLevelNumber();
            }
            else if (!bossSpawned) // Final wave - spawn boss
            {
                SpawnBoss();
                bossSpawned = true;
            }
        }
        UIManager.instance.UpdateLevelProgress();
        GameOver();
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        totalEnemiesToSpawn = enemiesToSpawn;
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Vector3 spawnPosition = GenerateRandomPosition();
            Instantiate(enemeyPrefab, spawnPosition, enemeyPrefab.transform.rotation);
        }
        UIManager.instance.levelCount++;
    }

    private Vector3 GenerateRandomPosition()
    {
        float spawnX = Random.Range(-spawnPos, spawnPos);
        float spawnZ = Random.Range(-spawnPos, spawnPos);
        Vector3 randomPos = new Vector3(spawnX, 0, spawnZ);
        return randomPos;
    }

    private void SpawnBoss()
    {
        Vector3 bossSpawnPosition = new Vector3(0, 1, 0); // Adjust position as needed
        Instantiate(bossPrefab, bossSpawnPosition, Quaternion.identity);
        Debug.Log("Boss Spawned!");
    }

    private void GameOver()
    {
        if (playerController.isGameOver)
        {
            UIManager.instance.MMBtn.SetActive(true);
            UIManager.instance.restartBtn.SetActive(true);
        }
    }
}
