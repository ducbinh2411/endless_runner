/*using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    public float moveSpeed = 2f;         // Speed at which the ground moves along the y-axis
    public float resetPositionY = -10f;  // Y position at which the ground resets
    public float startPositionY = 0f;    // Starting Y position for the ground reset

    void Update()
    {
        // Move the ground downward on the y-axis
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

        // Reset the ground position when it goes out of view
        if (transform.position.y <= resetPositionY)
        {
            transform.position = new Vector3(transform.position.x, startPositionY, transform.position.z);
        }
    }
}
*/

using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;         // Speed for ground movement
    [SerializeField] private float resetPositionY = -10f;  // Y position to reset ground
    [SerializeField] private float startPositionY = 0f;    // Y position to move ground back to

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

        // Move ground downward along the y-axis
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

        // Reset position when it reaches the specified reset Y position
        if (transform.position.y <= resetPositionY)
        {
            transform.position = new Vector3(transform.position.x, startPositionY, transform.position.z);
        }
    }
}
