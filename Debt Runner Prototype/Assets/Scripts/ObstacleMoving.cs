/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMoving : MonoBehaviour
{
    private float speed = 30;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }
}*/



using UnityEngine;

public class ObstacleMoving : MonoBehaviour
{
    [SerializeField] private float speed = 30f; // Expose speed to adjust in Inspector

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

        // Optional: Destroy obstacle if it moves off-screen
        if (transform.position.x < -10) // Adjust -10 to your scene boundary
        {
            Destroy(gameObject);
        }
    }
}
