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
    public bool bossSpawned = false; // Track if boss has been spawned

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
            if (waveNum < 5)
            {
                Instantiate(powerupPrefab, GenerateRandomPosition(), powerupPrefab.transform.rotation);
                waveNum++;
                totalEnemiesToSpawn = waveNum;
                SpawnEnemyWave(waveNum);
                UIManager.instance.DisplayLevelNumber();
            }
            else if (!bossSpawned)
            {
                if (BossIntroUI.instance == null)
                {
                    BossIntroUI.instance = FindObjectOfType<BossIntroUI>(true); // 👈 Find even if inactive
                }

                if (BossIntroUI.instance != null)
                {
                    BossIntroUI.instance.gameObject.SetActive(true); // 👈 Activate before calling
                    BossIntroUI.instance.ShowBossIntro();
                    Time.timeScale = 0;
                    bossSpawned = true;
                    UIManager.instance.StartBossTimer();
                }
                else
                {
                    Debug.LogError("BossIntroUI is missing or not assigned!");
                }
            }
        }
        UIManager.instance.UpdateLevelProgress();
        GameOver();
    }


    public void OnBossIntroComplete() // Call this when player clicks "Continue"
    {
        Time.timeScale = 1; // Resume game
        SpawnBoss();
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
