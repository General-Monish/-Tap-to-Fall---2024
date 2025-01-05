using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
  
    [SerializeField] GameObject enemeyPrefab;

    /*public Transform[] spawnPoints;*/
    public float startTimeBetweenSpawn;
    public float timeBetweenSpawn;
    float spawnPos = 9f;
    public GameObject summonPrefab;
    // Start is called before the first frame update
    void Start()
    {
        timeBetweenSpawn = startTimeBetweenSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient == false || PhotonNetwork.CurrentRoom.PlayerCount != 2)
        {
            return;
        }
        if(timeBetweenSpawn <= 0)
        {
            /*Vector3 randomSpawnpointsPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].position;*/
            PhotonNetwork.Instantiate(enemeyPrefab.name,GenerateRandomPosition(),enemeyPrefab.transform.rotation);
            SummonPrefabEffect();
            timeBetweenSpawn = startTimeBetweenSpawn;
        }
        else
        {
            timeBetweenSpawn -= Time.deltaTime;
        }
    }
    private void SummonPrefabEffect()
    {
        // Instantiate the summon effect at the enemy's position
        if (summonPrefab != null)
        {
            PhotonNetwork.Instantiate(summonPrefab.name,transform.position, summonPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateRandomPosition()
    {
        float spawnX = Random.Range(-spawnPos, spawnPos);
        float spawnZ = Random.Range(-spawnPos, spawnPos);
        Vector3 randomPos = new Vector3(spawnX, 0, spawnZ);
        return randomPos;
    }

}
