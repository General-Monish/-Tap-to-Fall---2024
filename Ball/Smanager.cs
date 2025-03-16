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
    private BossSpawner bossSpawner;


    [SerializeField] GameObject powerupPrefab;
    [SerializeField] GameObject enemeyPrefab;
    


    public float totalEnemiesToSpawn; // Total enemies in a wave
    float spawnPos = 9f;
    public int enemyCount;
    public int waveNum = 5;

    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        bossSpawner = GameObject.FindAnyObjectByType<BossSpawner>();
        playerController = GameObject.FindAnyObjectByType<PlayerController>();
    }

    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {

            if (waveNum < 5) // level 10 set wavenum < 14
            {
                Instantiate(powerupPrefab, GenerateRandomPosition(), powerupPrefab.transform.rotation);
                waveNum++;
                totalEnemiesToSpawn = waveNum; // Update the total enemies count
                SpawnEnemyWave(waveNum);
                UIManager.instance.DisplayLevelNumber();
            }
            else
            {
                // BOSS CODE
                
                
            }
        }
        UIManager.instance.UpdateLevelProgress();
        GameOver();
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        totalEnemiesToSpawn = enemiesToSpawn; // Set the new total
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

    /*
        public void SetPlayerController(PlayerController pc)
        {
            playerController = pc;
        }*/
    private void GameOver()
    {
        if (playerController.isGameOver)
        {
            UIManager.instance.MMBtn.SetActive(true);
            UIManager.instance.restartBtn.SetActive(true);
        }
    }
}
