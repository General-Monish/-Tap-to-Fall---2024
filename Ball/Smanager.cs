using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Smanager : MonoBehaviour
{
    [SerializeField] GameObject enemeyPrefab;
    [SerializeField] GameObject powerupPrefab;
    float spawnPos = 9f;
    int enemyCount;
    int waveNum=2;
    public TextMeshProUGUI levelText;
    int levelCount;
    // Start is called before the first frame update
    void Start()
    {
        levelCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount=FindObjectsOfType<Enemy>().Length;
        if(enemyCount == 0)
        {
            Instantiate(powerupPrefab, GenerateRandomPosition(), powerupPrefab.transform.rotation);
            waveNum++;
            SpawnEnemyWave(waveNum);
            DisplayLevelNumber();
        }
    }
    // nothing

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemeyPrefab, GenerateRandomPosition(), enemeyPrefab.transform.rotation);
        }
        levelCount++;
    }

    private Vector3 GenerateRandomPosition()
    {
        float spawnX = Random.Range(-spawnPos, spawnPos);
        float spawnZ = Random.Range(-spawnPos, spawnPos);
        Vector3 randomPos = new Vector3(spawnX, 0, spawnZ);
        return randomPos;
    }

    private void DisplayLevelNumber()
    {
        levelText.text="Level: " + levelCount.ToString();
    }
}
