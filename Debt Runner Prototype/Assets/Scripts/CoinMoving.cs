
using UnityEngine;

public class CoinMoving : MonoBehaviour
{
    [SerializeField] private float speed = 10f;     // Speed at which the coin moves up or down
    private float destroyBoundZ = -5f;             // Y-position where the coin will be destroyed

    private PlayerController playerControllerScript;

    void Start()
    {
        // Get reference to the PlayerController component from the Player GameObject
        GameObject playerObject = GameObject.FindWithTag("Player");
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
        // Stop coin movement if the game is over
        if (playerControllerScript != null && playerControllerScript.gameOver)
        {
            return; // Exit Update to stop movement
        }

        // Move the coin along the Y-axis
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // Destroy the coin if it goes out of bounds on the Y-axis
        if (transform.position.y < destroyBoundZ)
        {
            Destroy(gameObject);
        }
    }
}
