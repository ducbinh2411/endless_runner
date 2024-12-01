using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;        // Reference to the player's Transform
    public Vector3 offset;          // Offset between the camera and the player

    void Start()
    {
        if (player == null)
        {
            Debug.LogWarning("Player Transform not assigned. Please assign the player in the Inspector.");
        }
        else
        {
            // Initialize the offset
            offset = transform.position - player.position;
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Update the camera's position based on the player's position and the offset
            transform.position = player.position + offset;
        }
    }
}
