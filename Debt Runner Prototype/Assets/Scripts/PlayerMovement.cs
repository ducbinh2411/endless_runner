using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 10f;      // Force applied to the player for jumping
    public float moveSpeed = 5f;       // Speed for left and right movement
    private Rigidbody rb;              // Rigidbody component of the player

    void Start()
    {
        // Get the Rigidbody component attached to the player
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check if the spacebar is pressed for jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
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
            Vector3 newPosition = rb.position + new Vector3(moveDirection * moveSpeed * Time.deltaTime, 0, 0);
            rb.MovePosition(newPosition);
        }
    }

    void Jump()
    {
        // Apply an upward force to the Rigidbody for jumping
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
