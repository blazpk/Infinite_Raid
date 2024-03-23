using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClicked : MonoBehaviour
{
    //public GameObject winPanel;
    public GameObject healthPanel;
    public GameObject BG;
    public GameObject heroSelectCanvas;
    public GameObject playerZone;
    public GameObject progressBar;
    public GameObject hpBar;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private Transform[] heroBoardSlot;
    [SerializeField] private AudioSource bgm;
    AudioSource clickSound;

    private void Start()
    {
        clickSound = GetComponent<AudioSource>();
    }

    public void RetryButton()
    {
        clickSound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void NextButton(string levelName)
    {
        clickSound.Play();
        SceneManager.LoadScene(levelName);
        Time.timeScale = 1;
    }

    public void MainMenuButton()
    {
        clickSound.Play();
        SceneManager.LoadScene("MapSelect");
        Time.timeScale = 1;
    }

    public void PauseButton()
    {
        clickSound.Play();
        Time.timeScale = 0;
        bgm.Pause();
        pausePanel.SetActive(true);
    }

    public void ResumeButton()
    {
        clickSound.Play();
        pausePanel.SetActive(false);
        bgm.UnPause();
        Time.timeScale = 1;
    }

    public void PlayGameButton()
    {
        //check if any selected hero or not, must select hero to start
        bool cardSelected = false;

        for (int i = 0; i < heroBoardSlot.Length; i++)
        {
            if(heroBoardSlot[i].childCount != 0)
            {
                cardSelected = true;
                break;
            }
        }

        if (cardSelected == false) return;

        //healthPanel.SetActive(true);
        BG.SetActive(false);
        heroSelectCanvas.SetActive(false);
        playerZone.SetActive(true);
        progressBar.SetActive(true);
        pauseButton.SetActive(true);
        hpBar.SetActive(true);
        Time.timeScale = 1;
        bgm.Play();
        clickSound.Play();

    }

    public void BackButton()
    {
        clickSound.Play();
        Time.timeScale = 1;
        SceneManager.LoadScene("MapSelect");
    }
}
