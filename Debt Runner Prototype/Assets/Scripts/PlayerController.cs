using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f;      // Force applied to the player for jumping
    public float moveSpeed = 5f;       // Speed for left and right movement
    public float gravityModifier = 1f; // Gravity modifier to adjust gravity strength
    public bool isOnGround = true;     // Checks if the player is on the ground
    public bool gameOver = false;
    private Rigidbody playerRb;        // Rigidbody component of the player
    internal bool gameStarted;

    void Start()
    {
        // Get the Rigidbody component attached to the player
        playerRb = GetComponent<Rigidbody>();

        // Modify gravity if needed
        Physics.gravity *= gravityModifier;
    }

    void Update()
    {
        // Jumping logic
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }

        // Left and right movement
        float moveDirection = 0f;

        // Check if 'A' or 'D' is pressed and set move direction
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection = -1f;  // Move left
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveDirection = 1f;   // Move right
        }

        // Apply left or right movement if either key is pressed
        if (moveDirection != 0f)
        {
            Vector3 newPosition = playerRb.position + new Vector3(moveDirection * moveSpeed * Time.deltaTime, 0, 0);
            playerRb.MovePosition(newPosition);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player hits the ground to enable jumping again
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        } else if (collision.gameObject.CompareTag("Obstacle")) {
            gameOver = true;
            Debug.Log("Game Over!");
        }
    }
}
