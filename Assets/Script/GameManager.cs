using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // For restarting the scene or loading other scenes

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI; // Reference to the Game Over UI
    private int enemyCount;       // Track how many enemies are still alive

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

    // Method to restart the game (reset the scene)
    public void RestartGame()
    {
        // Reset the time scale in case the game was paused during Game Over
        Time.timeScale = 1f;

        // Reload the currently active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Method to load the mode selection scene
    public void Mode()
    {
        Time.timeScale = 1f; // Ensure the game is unpaused
        SceneManager.LoadScene("ChooseMode"); // Load the ChooseMode scene
    }

    // Method to choose game mode and load appropriate scene
    public void ChooseGameMode(string mode)
    {
        if (mode == "Easy")
        {
            PlayerPrefs.SetString("GameMode", "Easy");
            SceneManager.LoadScene("EasyScenes"); // Load Easy scene
        }
        else if (mode == "Hard")
        {
            PlayerPrefs.SetString("GameMode", "Hard");
            SceneManager.LoadScene("HardScenes"); // Load Hard scene
        }
    }

    // Option to quit the game
    public void QuitGame()
    {
        Time.timeScale = 1f;
        Debug.Log("Quitting Game...");
        SceneManager.LoadScene("MainMenu");
    }
}
