using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    private float startDelay = 2f;              // Initial delay before spawning starts
    private float repeatRate = 2f;              // Time interval between spawns
    private float spawnDistanceZ = 45f;         // Distance in front of the player where obstacles spawn
    private float groundYPosition = 0f;         // Y position for ground level
    private PlayerController playerControllerScript; // Reference to PlayerController

    // X positions for lanes (e.g., left, center, right)
    private float[] lanePositionsX = { -3f, 0f, 3f };

    void Start()
    {
        // Get reference to the PlayerController script
        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null)
        {
            playerControllerScript = playerObject.GetComponent<PlayerController>();
        }
        else
        {
            Debug.LogWarning("Player GameObject not found.");
        }

        // Start spawning obstacles at regular intervals
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    void SpawnObstacle()
    {
        // Check if the game is over before spawning a new obstacle
        if (playerControllerScript != null && playerControllerScript.gameOver)
        {
            CancelInvoke("SpawnObstacle"); // Stop further spawns if game over
            return; // Exit the method without spawning
        }

        // Select a random lane for the obstacle
        float spawnX = lanePositionsX[Random.Range(0, lanePositionsX.Length)];
        
        // Set the spawn position with fixed Y position (ground level) and Z distance from player
        Vector3 spawnPos = new Vector3(spawnX, groundYPosition, spawnDistanceZ);

        // Instantiate the obstacle at the chosen lane position
        Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
    }
}
