using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMoving : MonoBehaviour
{
    [SerializeField] private float speed = 30f; // Expose speed to adjust in Inspector
    private float destroyBound = -10f;          // X-position where the obstacle will be destroyed

    private PlayerController playerControllerScript;

    void Start()
    {
        // Find the Player GameObject and get the PlayerController component
        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null)
        {
            playerControllerScript = playerObject.GetComponent<PlayerController>();
        }
        else
        {
            Debug.LogWarning("Player GameObject not found.");
        }
    }

    void Update()
    {
        // Stop obstacle movement if the game is over
        if (playerControllerScript != null && playerControllerScript.gameOver)
        {
            return; // Exit Update to stop movement
        }

        // Move obstacle to the left
        transform.Translate(Vector3.left * Time.deltaTime * speed);

        // Destroy obstacle if it moves out of bounds
        if (transform.position.z < destroyBound)
        {
            Destroy(gameObject);
        }
    }
}
