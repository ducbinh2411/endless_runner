using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject playButton;   // Reference to the Play button
    public GameObject replayButton; // Reference to the Replay button

    void Start()
    {
        // Show the Play button and hide the Replay button initially
        playButton.SetActive(true);
        replayButton.SetActive(false);

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

    public void GameOver()
    {
        // Show the Replay button when the game is over
        replayButton.SetActive(true);

        // Stop the game
        Time.timeScale = 0f;

        Debug.Log("Game Over!");
    }

    public void ReplayGame()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Resume the game
        Time.timeScale = 1f;

        Debug.Log("Game Restarted!");
    }
}
