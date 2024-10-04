using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // For restarting the scene or loading other scenes

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI; // Reference to the Game Over UI

    // Method to trigger Game Over
    public void GameOver()
    {
        // Enable the Game Over UI
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        // Optionally, you can also freeze the game by setting the time scale to 0
        Time.timeScale = 0f; // Stops time in the game (pause)
        Debug.Log("Game Over!");
    }

    // Option to restart the game (reset the scene)
    public void RestartGame()
    {
        // Reset the time scale in case the game was paused during Game Over
        Time.timeScale = 1f;

        // Reload the currently active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Mode()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("ChooseMode");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Option to quit the game
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        SceneManager.LoadScene("MainMenu");
    }
}
