using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 3.0f; // Speed of the enemy
    private PlayerController[] playerControllers;
    private PlayerController nearestPlayer;
    private Rigidbody rb;               // Rigidbody for enemy movement
    private Smanager smanager;          // Reference to the game manager

    public GameObject summonPrefab;     // Visual effect when the enemy is spawned

    void Start()
    {
        // Find the Smanager instance
        smanager = Smanager.Instance;

        // Try to find the player's transform
       playerControllers = FindObjectsOfType<PlayerController>();

        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Trigger the spawn effect
        SummonPrefabEffect();
    }

    void Update()
    {
        float distanceOne = Vector2.Distance(transform.position , playerControllers[0].transform.position);
        float distanceTwo = Vector2.Distance(transform.position , playerControllers[1].transform.position);

        if (distanceOne < distanceTwo)
        {
            nearestPlayer = playerControllers[0];
        } else
        {
            nearestPlayer= playerControllers[1];
        }

        if(nearestPlayer != null)
        {
            MoveTowradsPlayer();
        }
       

        // Check for game-ending conditions
        CheckOutOfBounds();
    }

    private void MoveTowradsPlayer()
    {

        Vector3 lookDir = (nearestPlayer.transform.position - transform.position).normalized;
        rb.AddForce(lookDir * speed);
    }

    private void CheckOutOfBounds()
    {
        // If the enemy falls out of the game area, destroy it and update the score
        if (transform.position.y < -10)
        {
            if (smanager != null)
            {
                smanager.DisplayScore(2); // Add to the score
            }
            Destroy(gameObject);
        }
    }

    private void SummonPrefabEffect()
    {
        // Instantiate the summon effect at the enemy's position
        if (summonPrefab != null)
        {
            Instantiate(summonPrefab, transform.position, Quaternion.identity);
        }
    }
}