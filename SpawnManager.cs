using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject obstaclePrefab;


    private Player player;
    private float startDelay = 2f;
    private float repeatRate = 2f;
    private Vector3 spawnPos= new Vector3 (25,0,0);
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating( "SpawnObs", startDelay,repeatRate);
        player=GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnObs()
    {
        if (player.isGameOver == false)
        {
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }
        
    }
}
