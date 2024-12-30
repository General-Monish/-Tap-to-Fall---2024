using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab; // Ensure this prefab is in the Resources folder.

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private void Start()
    {
        // Generate a random position within the specified range
        Vector3 randomPos = new Vector3(Random.Range(minX, maxX),0, Random.Range(minY, maxY));

        // Use PhotonNetwork.Instantiate to spawn the player and get the instance
        GameObject spawnedPlayer = PhotonNetwork.Instantiate(playerPrefab.name, randomPos, playerPrefab.transform.rotation);

        // Set the player controller reference in the Smanager
        PlayerController playerController = spawnedPlayer.GetComponent<PlayerController>();
        if (Smanager.Instance != null && playerController != null)
        {
            Smanager.Instance.SetPlayerController(playerController);
        }
    }
}
