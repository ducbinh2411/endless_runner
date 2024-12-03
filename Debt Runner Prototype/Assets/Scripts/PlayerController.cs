using UnityEngine;
using TMPro; // Required for TextMeshPro

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f;      // Force applied to the player for jumping
    public float moveSpeed = 5f;       // Speed for left and right movement
    public float gravityModifier = 1f; // Gravity modifier to adjust gravity strength
    public float xRange = 10f;         // Range limit for left and right movement
    public bool isOnGround = true;     // Checks if the player is on the ground
    public bool gameOver = false;      // Game over state
    public AudioClip jumpSound;
    public AudioClip crashSound;
    internal bool gameStarted;

    public int moneyCount = 0;         // Tracks the player's collected coins
    public AudioClip coinCollectSound; // Sound effect for coin collection
    public ParticleSystem coinEffect;  // Particle effect for coin collection
    public ParticleSystem explosionParticle;

    public TextMeshProUGUI coinCounterText; // UI Text for displaying coin count

    private Rigidbody playerRb;        // Rigidbody component of the player
    private AudioSource playerAudio;   // Audio source for sound effects
    private Animator playerAnim;
    private GameManager gameManager;   // Reference to GameManager for game state control

    void Start()
    {
        // Get components
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();

        // Modify gravity if needed
        Physics.gravity *= gravityModifier;

        // Initialize coin counter UI
        UpdateCoinCounter();
    }

    void Update()
    {
        // Jumping logic
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound, 1.0f);
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

            // Clamp the player's X position within the specified range
            newPosition.x = Mathf.Clamp(newPosition.x, -xRange, xRange);

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

            // Trigger Game Over in GameManager
            if (gameManager != null)
            {
                gameManager.GameOver();
            }

            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Handle coin collection
        if (other.CompareTag("Coin"))
        {
            moneyCount += 1;
            Debug.Log("Coin collected! Money count: " + moneyCount);

            // Update the coin counter UI
            UpdateCoinCounter();

            // Play coin collection sound effect
            if (coinCollectSound != null && playerAudio != null)
            {
                playerAudio.PlayOneShot(coinCollectSound);
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

    private void UpdateCoinCounter()
    {
        if (coinCounterText != null)
        {
            coinCounterText.text = "Coins: " + moneyCount;
        }
    }
}
