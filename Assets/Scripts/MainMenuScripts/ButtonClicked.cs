using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClicked : MonoBehaviour
{
    public void RetryButton(string levelName)
    {
        SceneManager.LoadScene(levelName);
        Time.timeScale = 1;
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenuScene");
        Time.timeScale = 1;
    }
}
