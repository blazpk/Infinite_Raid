using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClicked : MonoBehaviour
{
    public void RetryButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
