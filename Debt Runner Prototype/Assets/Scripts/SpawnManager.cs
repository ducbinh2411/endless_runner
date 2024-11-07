/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject obstaclePrefab;
    private Vector3 spawnPos = new Vector3(0, 0, 45);
    private float startDelay = 2;
    private float repeatRate = 2;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        
    }

     void SpawnObstacle () {
        
        
        Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}*/


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

    // X positions for lanes (e.g., left, center, right)
    private float[] lanePositionsX = { -3f, 0f, 3f };

    void Start()
    {
        // Start spawning obstacles at regular intervals
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    void SpawnObstacle()
    {
        // Select a random lane for the obstacle
        float spawnX = lanePositionsX[Random.Range(0, lanePositionsX.Length)];
        
        // Set the spawn position with fixed Y position (ground level) and Z distance from player
        Vector3 spawnPos = new Vector3(spawnX, groundYPosition, spawnDistanceZ);

        // Instantiate the obstacle at the chosen lane position
        Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
    }
}
