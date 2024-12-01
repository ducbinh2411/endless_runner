using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f;      // Force applied to the player for jumping
    public float moveSpeed = 5f;       // Speed for left and right movement
    public float gravityModifier = 1f; // Gravity modifier to adjust gravity strength
    public bool isOnGround = true;     // Checks if the player is on the ground
    public bool gameOver = false;      // Game over state
    internal bool gameStarted;

    public int moneyCount = 0;         // Tracks the player's collected coins
    public AudioClip coinCollectSound; // Sound effect for coin collection
    public ParticleSystem coinEffect;  // Particle effect for coin collection

    private Rigidbody playerRb;        // Rigidbody component of the player
    private AudioSource audioSource;   // Audio source for sound effects
    private Animator playerAnim;

    void Start()
    {
        // Get the Rigidbody component attached to the player
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();

        // Modify gravity if needed
        Physics.gravity *= gravityModifier;

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        
    }

    void Update()
    {
        // Stop all player input if game over
        if (gameOver) return;
        
        // Jumping logic
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
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
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Handle coin collection
        if (other.CompareTag("Coin"))
        {
            moneyCount += 1; // Increment the money counter
            Debug.Log("Coin collected! Money count: " + moneyCount);

            // Play coin collection sound effect
            if (coinCollectSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(coinCollectSound);
            }

            // Instantiate particle effect at the coin's position
            if (coinEffect != null)
            {
                Instantiate(coinEffect, other.transform.position, Quaternion.identity);
            }

            // Destroy the coin
            Destroy(other.gameObject);
        }
    }
}
