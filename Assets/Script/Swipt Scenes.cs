using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Main : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("ChooseMode");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoBackToMainMenu()
{
    SceneManager.LoadScene("MainMenu");
}

    public void EasyMode()
    {
        SceneManager.LoadScene("EasyScenes");
    }

    public void HardMode()
    {
        SceneManager.LoadScene("HardScenes");
    }


}