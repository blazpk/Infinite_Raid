using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private Button[] button;
    AudioSource clickSound;

    private void Start()
    {
        Time.timeScale = 1;

        clickSound = GetComponent<AudioSource>();

        //cheat unlock stage
        //PlayerPrefs.SetInt("Level1", 1);
        //PlayerPrefs.SetInt("Level2", 1);
        //PlayerPrefs.SetInt("Level3", 1);
        //PlayerPrefs.SetInt("Level4", 1);
        //PlayerPrefs.SetInt("Level5", 1);
        //PlayerPrefs.SetInt("Level6", 1);
        //PlayerPrefs.SetInt("Level7", 1);
        //PlayerPrefs.Save();

        //uncheat
        //PlayerPrefs.SetInt("Level1", 0);
        //PlayerPrefs.SetInt("Level2", 0);
        //PlayerPrefs.SetInt("Level3", 0);
        //PlayerPrefs.SetInt("Level4", 0);
        //PlayerPrefs.SetInt("Level5", 0);
        //PlayerPrefs.SetInt("Level6", 0);
        //PlayerPrefs.SetInt("Level7", 0);
        //PlayerPrefs.Save();

        //check if level is cleared to unlock
        for (int i = 0; i < button.Length; i++)
        {
            if (PlayerPrefs.GetInt("Level" + (i + 1), 0) == 1)
            {
                button[i].interactable = true;
                button[i].GetComponent<Image>().color = Color.white;
            }
            else
            {
                button[i].interactable = false;
                button[i].GetComponent<Image>().color = Color.gray;
            }
        }
    }

    public void Select(string levelName)
    {
        StartCoroutine(LoadScene(levelName));
    }

    public void BackButton()
    {
        StartCoroutine(LoadScene("MainMenuScene"));
    }

    public void EndlessButton()
    {
        StartCoroutine(LoadScene("Endless"));
    }

    private IEnumerator LoadScene(string name)
    {
        clickSound.Play();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(name);
    }
}
