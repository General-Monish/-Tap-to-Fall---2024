using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 3.0f; // Speed of the enemy
    private Transform playerTransform;  // Reference to the player's transform
    private Rigidbody rb;               // Rigidbody for enemy movement
    private Smanager smanager;          // Reference to the game manager

    public GameObject summonPrefab;     // Visual effect when the enemy is spawned

    void Start()
    {
        // Find the Smanager instance
        smanager = Smanager.Instance;

        // Try to find the player's transform
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            playerTransform = player.transform;
        }

        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Trigger the spawn effect
        SummonPrefabEffect();
    }

    void Update()
    {
        // Move towards the player if they exist
        if (playerTransform != null)
        {
            MoveTowradsPlayer();
        }

        // Check for game-ending conditions
        CheckOutOfBounds();
    }

    private void MoveTowradsPlayer()
    {

        Vector3 lookDir = (playerTransform.position - transform.position).normalized;
        rb.AddForce(lookDir * speed);
    }

    private void CheckOutOfBounds()
    {
        // If the enemy falls out of the game area, destroy it and update the score
        if (transform.position.y < -10)
        {
            if (smanager != null)
            {
                smanager.DisplayScore(1); // Add to the score
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
    } // noths
}