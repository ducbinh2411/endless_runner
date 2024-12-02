using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;               // The coin prefab to spawn
    public float spawnInterval = 12f;           // Time interval between coin spawns
    public float spawnRangeX = 5f;              // Range on the X-axis for random spawn positions
    public float minSpawnDistanceZ = 10f;       // Minimum distance in front of the player to avoid close spawns
    public float maxSpawnDistanceZ = 45f;       // Maximum distance in front of the player to spawn coins
    public float minSpawnHeightY = 2f;          // Minimum Y position for coins to appear
    public float maxSpawnHeightY = 6f;          // Maximum Y position for coins to appear
    private Transform playerTransform;          // Reference to the player's position
    private PlayerController playerControllerScript; // Reference to the PlayerController for game over status

    private void Start()
    {
        // Find the player object by tag and set up a reference to its transform
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
            playerControllerScript = playerObject.GetComponent<PlayerController>();
        }
        else
        {
            Debug.LogWarning("Player GameObject not found.");
        }

        // Start spawning coins at regular intervals
        InvokeRepeating("SpawnCoin", 0f, spawnInterval);
    }

    void SpawnCoin()
    {
        // Stop spawning if the game is over
        if (playerControllerScript != null && playerControllerScript.gameOver)
        {
            CancelInvoke("SpawnCoin"); // Stop further invokes of SpawnCoin
            return;
        }

        // Ensure we have a player reference before spawning coins
        if (playerTransform == null) return;

        // Generate a random X position within the specified range
        float spawnX = Random.Range(-spawnRangeX, spawnRangeX);
        
        // Generate a random Z position that is at least `minSpawnDistanceZ` in front of the player
        float spawnZ = playerTransform.position.z + Random.Range(minSpawnDistanceZ, maxSpawnDistanceZ);

        // Generate a random Y position within the specified height range
        float spawnY = Random.Range(minSpawnHeightY, maxSpawnHeightY);

        // Set the spawn position with the calculated values
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, spawnZ);

        // Instantiate the coin at the chosen position with the upright rotation
        Instantiate(coinPrefab, spawnPosition, Quaternion.Euler(-90, 0, 0)); // Set rotation to x = -90, y = 0, z = 0
    }
}
