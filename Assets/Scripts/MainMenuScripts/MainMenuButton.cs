using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    [SerializeField] private AudioSource sound;
    AudioSource clickSound;

    private void Start()
    {
        clickSound = GetComponent<AudioSource>();
    }

    public void PlayGame()
    {
        StartCoroutine(LoadScene("MapSelect"));
    }

    public void ShopButton()
    {
        StartCoroutine(LoadScene("ShopScene"));
    }

    public void QuitGame()
    {
        sound.Stop();
        Application.Quit();
    }

    private IEnumerator LoadScene(string name)
    {
        clickSound.Play();
        yield return new WaitForSeconds(0.5f);
        sound.Stop();
        SceneManager.LoadScene(name);
    }
}
