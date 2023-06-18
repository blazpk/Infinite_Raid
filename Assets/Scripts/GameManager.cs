using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject winPanel;
    private int killCount;
    public int maxKill;
    private WallHealth hp;
    private int enemyDieCount;
    private spawnEnemy spawned;

    void Start()
    {
        hp = FindObjectOfType<WallHealth>();
        spawned = FindObjectOfType<spawnEnemy>();
        winPanel.SetActive(false);
    }

    public void updateKill()
    {
        killCount++;
        if(killCount >= maxKill)
        {
            winPanel.SetActive(true);
        }
    }

    public int showKill()
    {
        return killCount;
    }

    public void updateEnemyDieCount()
    {
        enemyDieCount ++;
        if (enemyDieCount >= spawned.totalEnemies && hp.wallHealth > 0)
        {
            winPanel.SetActive(true);
        }
    }

    public int showDieEnemy()
    {
        return enemyDieCount;
    }
}
