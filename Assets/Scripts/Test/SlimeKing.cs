using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeKing : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject smallSlime;
    public float interval = 10;
    public float speed = 0.2f;

    public int hpTriggerSpawn = 20;
    public int hpTriggerMove = 30;

    private enemyStat stat;
    private spawnEnemy spawner;
    private Vector3 newPos;


    void Start()
    {
        stat = GetComponent<enemyStat>();
        spawner = FindObjectOfType<spawnEnemy>();
        changeLane();
        InvokeRepeating(nameof(Atk), 5, 5);
        InvokeRepeating(nameof(spawn), 5, 5);
    }

    private void Update()
    {
        if(stat.HealthPoint <= (stat.initialHealthPoint - hpTriggerMove))
        {
            if (transform.position == newPos)
            {
                changeLane();
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, newPos, Time.deltaTime * speed);
            }
        }
    }

    private void Atk()
    {
        Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);
    }

    private void spawn()
    {
        var posIndex = Random.Range(0, spawner.spawnPos.Length);
        Instantiate(smallSlime, spawner.spawnPos[posIndex].transform.position, transform.rotation);
    }

    private void changeLane()
    {
        var posIndex = Random.Range(0, spawner.spawnPos.Length);
        newPos = new Vector3(transform.position.x, spawner.spawnPos[posIndex].transform.position.y, 0);
    }
}
