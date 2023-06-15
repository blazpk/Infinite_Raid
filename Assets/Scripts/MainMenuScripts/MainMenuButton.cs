using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("MapSelect");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
