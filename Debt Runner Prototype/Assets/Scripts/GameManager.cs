using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject playButton; // Reference to the Play button

    void Start()
    {
        // Show the Play button when the game starts
        playButton.SetActive(true);

        // Pause the game at the start
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        // Hide the Play button
        playButton.SetActive(false);

        // Resume the game
        Time.timeScale = 1f;

        Debug.Log("Game Started!");
    }
}
