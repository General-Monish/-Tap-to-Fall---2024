using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiEnemy : MonoBehaviour
{
    [SerializeField] float speed = 3.0f; // Speed of the enemy
    MultiPlayerController[] players;
    MultiPlayerController nearestPlayer;
    public GameObject summonPrefab;
    Rigidbody rb;

    void Start()
    {
        players = FindObjectsOfType<MultiPlayerController>();
        rb = GetComponent<Rigidbody>();
        SummonPrefabEffect();
    }

    void Update()
    {
        MoveTowradsPlayer();
        // Check for game-ending conditions
        CheckOutOfBounds();
    }

    private void MoveTowradsPlayer()
    {
        float playerOne= Vector2.Distance(transform.position, players[0].transform.position);
        float playerTwo= Vector2.Distance(transform.position, players[1].transform.position);

        if(playerOne < playerTwo)
        {
            nearestPlayer = players[0];
        }else
        {
            nearestPlayer = players[1];
        }

        if(nearestPlayer != null)
        {
            /*transform.position = Vector3.MoveTowards(transform.position, nearestPlayer.transform.position , speed * Time.deltaTime);*/

            Vector3 lookDir = (nearestPlayer.transform.position - transform.position).normalized;
            rb.AddForce(lookDir * speed);
        }

    }

    private void CheckOutOfBounds()
    {
        // If the enemy falls out of the game area, destroy it and update the score
        if (transform.position.y < -10)
        {
           /* if (smanager != null)
            {
                smanager.DisplayScore(2); // Add to the score
            }*/
            Destroy(gameObject);
        }
    }

    private void SummonPrefabEffect()
    {
        // Instantiate the summon effect at the enemy's position
        if (summonPrefab != null)
        {
            PhotonNetwork.Instantiate(summonPrefab.name, transform.position, summonPrefab.transform.rotation);
        }
    }
}