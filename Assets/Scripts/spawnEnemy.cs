using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spawnEnemy : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] spawnPos;
    public GameObject boss;

    public float spawnDelay;
    public int totalEnemies;
    public Slider progressBar;
    [SerializeField] private float spawnStartTime;
    [SerializeField] private int wave;
    [SerializeField] private int beforeLastWave;
    [SerializeField] private AudioSource alarm;
    private GameManager manager;

    [SerializeField] private bool isEndless;
    private int roundCount = 0;

    private int enemiesSpawned = 0;
    private bool firstWave = false;

    // Start is called before the first frame update
    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
        progressBar.maxValue = totalEnemies - beforeLastWave;
        InvokeRepeating(nameof(spawn), spawnStartTime, spawnDelay);
    }

    private void Update()
    {
        progressBar.value = enemiesSpawned;
        endlessEnemy();

    }

    private void spawn()
    {
        if (enemiesSpawned >= totalEnemies) return;

        var enemyIndex = Random.Range(0, enemyPrefabs.Length);
        var posIndex = Random.Range(0, spawnPos.Length);

        if(enemiesSpawned < totalEnemies / 2)
        {
            spawnSingle(enemyIndex, posIndex);
        }
        else if(enemiesSpawned >= totalEnemies / 2 && firstWave == false && wave > 1)
        {
            spawnAll(2);
        }
        else if(enemiesSpawned < totalEnemies - beforeLastWave)
        {
            spawnSingle(enemyIndex, posIndex);
        }
        else if (enemiesSpawned >= totalEnemies - beforeLastWave)
        {
            spawnAll(2);
        }

        //Debug.Log("enemies spawned: " + enemiesSpawned);
    }

    private void spawnSingle(int enemyIndex, int posIndex)
    {
        if (enemiesSpawned >= totalEnemies) return;
        Instantiate(enemyPrefabs[enemyIndex], spawnPos[posIndex].transform.position, transform.rotation);
        enemiesSpawned++;
    }

    private void spawnAll(int repeatNumber)
    {
        if(alarm != null) { alarm.Play(); }

        for (int i = 0; i < repeatNumber; i++)
        {
            if (enemiesSpawned >= totalEnemies) return;
            for (int j = 0; j < spawnPos.Length; j++)
            {
                if (enemiesSpawned >= totalEnemies) return;
                StartCoroutine(spawnWithDelay(0.2f, j));
                enemiesSpawned++;
            }
        }
        firstWave = true;
    }

    private IEnumerator spawnWithDelay(float time,int pos)
    {
        float randDelay = Random.Range(0, 9);
        yield return new WaitForSeconds(randDelay * time);
        var enemyIndex = Random.Range(0, enemyPrefabs.Length);
        Instantiate(enemyPrefabs[enemyIndex], spawnPos[pos].transform.position, transform.rotation);
    }

    private void endlessEnemy()
    {
        if (isEndless == false) return;

        if(manager.showDieEnemy() >= totalEnemies)
        {
            enemiesSpawned = 0;
            roundCount++;
            PlayerPrefs.SetInt("Diamond", PlayerPrefs.GetInt("Diamond", 0) + 10 * roundCount);
            PlayerPrefs.Save();

            if(roundCount >= 10)
            {
                manager.winPanel.SetActive(true);
                Time.timeScale = 0;

                roundCount = 0;

                if(totalEnemies < 50) 
                { 
                    totalEnemies += 10; 
                    beforeLastWave += 5; 
                }
                
                if(totalEnemies >= 45 && totalEnemies <= 60)
                {
                    wave += 1;
                }

                Instantiate(boss, this.transform.position, this.transform.rotation);
            }
        }
    }

    //old spawn function
    //private void spawn()
    //{
    //    var enemyIndex = Random.Range(0, enemyPrefabs.Length);
    //    var posIndex = Random.Range(0, spawnPos.Length);

    //    if (enemiesSpawned < totalEnemies)
    //    {
    //        Instantiate(enemyPrefabs[enemyIndex], spawnPos[posIndex].transform.position, transform.rotation);
    //        enemiesSpawned++;
    //        Debug.Log("enemies spawned: " + enemiesSpawned);
    //    }
    //    else
    //    {
    //        CancelInvoke("spawn");
    //    }

    //}
}
