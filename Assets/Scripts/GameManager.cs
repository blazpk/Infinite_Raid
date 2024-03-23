using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject pausePanel;
    public GameObject BG;
    public GameObject pauseButton;
    private int killCount = 0;
    public int maxKill;
    private Health hp;
    private int enemyDieCount = 0;
    private spawnEnemy spawned;
    public GameObject healthPanel;
    public GameObject playerZone;
    public GameObject progressBar;
    public GameObject hpBar;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private int bonusDiamond = 50;
    [SerializeField] private bool isEndless;

    void Start()
    {
        hp = FindObjectOfType<Health>();
        spawned = FindObjectOfType<spawnEnemy>();
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        pausePanel.SetActive(false);
        pauseButton.SetActive(false);
        //healthPanel.SetActive(false);
        BG.SetActive(true);
        playerZone.SetActive(false);
        progressBar.SetActive(false);
        hpBar.SetActive(false);
        Time.timeScale = 0;
        levelText.text = SceneManager.GetActiveScene().name;
    }

    public void updateKill()
    {
        killCount++;
        if(killCount >= maxKill)
        {
            //Win();
        }
    }

    public int showKill()
    {
        return killCount;
    }

    public void updateEnemyDieCount()
    {
        enemyDieCount ++;
        if (enemyDieCount >= spawned.totalEnemies && hp.HP > 0)
        {
            if(isEndless == false) Win();
        }
    }

    public int showDieEnemy()
    {
        return enemyDieCount;
    }

    private void Win()
    {
        Time.timeScale = 0;
        winPanel.SetActive(true);
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
        int dia = PlayerPrefs.GetInt("Diamond", 0) + bonusDiamond;
        PlayerPrefs.SetInt("Diamond", dia);
        PlayerPrefs.Save();
    }
}
