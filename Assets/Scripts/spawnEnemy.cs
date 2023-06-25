using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemy : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] spawnPos;
    public float interval;
    public int totalEnemies;

    private int enemiesSpawned;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(spawn), interval, interval);
    }

    private void spawn()
    {
        var enemyIndex = Random.Range(0, enemyPrefabs.Length);
        var posIndex = Random.Range(0, spawnPos.Length);

        if (enemiesSpawned < totalEnemies)
        {
            Instantiate(enemyPrefabs[enemyIndex], spawnPos[posIndex].transform.position, transform.rotation);
            enemiesSpawned++;
            Debug.Log("enemies spawned: " + enemiesSpawned);
        }
        else
        {
            CancelInvoke("spawn");
        }
        
    }
}
