using UnityEngine;

public class GroundMover : MonoBehaviour
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
