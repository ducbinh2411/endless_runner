

using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;         // Speed at which the ground moves along the z-axis
    [SerializeField] private float resetPositionZ = -10f;  // z position at which the ground resets
    [SerializeField] private float startPositionZ = 0f;    // Starting z position for the ground reset

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
        // Stop ground movement if the game is over
        if (playerControllerScript != null && playerControllerScript.gameOver)
        {
            return; // Exit Update to stop movement
        }

        // Move the ground downward on the z-axis
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

        // Reset the ground position when it goes out of view
        if (transform.position.z <= resetPositionZ)
        {
            // Reset the ground position back to the starting z position to create a looping effect
            transform.position = new Vector3(transform.position.x, transform.position.y, startPositionZ);
        }
    }
}

