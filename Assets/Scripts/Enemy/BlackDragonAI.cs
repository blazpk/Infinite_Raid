using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackDragonAI : MonoBehaviour
{
    public float moveSpeed = 0.2f;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject eggPrefab;
    [SerializeField] private float eggTime = 10;
    [SerializeField] private float cdAtkTime;
    private float timeCount;
    private float eggDelayCount = 0f;
    private Vector3 oldPos;
    private Vector3 newPos;
    private bool isMoving = false;
    private bool atStartPos = false;
    [SerializeField] private Vector3 startPos;
    private bool startSpawnEgg = false;

    private int currentHP;
    private bool atk80Performed = false;
    private bool atk60Performed = false;
    private bool atk40Performed = false;
    private bool atk20Performed = false;
    [SerializeField] private float atkAnimTime;
    [SerializeField] private AudioSource sound;

    enemyStat enemy;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    Animator anim;

    Vector3 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        enemy = GetComponent<enemyStat>();
        startPos = new Vector3(1.6f, 0, 0);
        anim.SetTrigger("isMove");
    }

    private void Update()
    {
        //move to start pos
        if(atStartPos == false)
        {
            moveToStartPos();
        }

        //start spawn egg
        if(startSpawnEgg == true)
        {
            spawnEgg(eggTime);
        }

        //dragon atk
        //triggerAtk();
        atkWithTimeCD(cdAtkTime);

        if(isMoving == true)
        {
            dragonMove();
        }
    }

    private void spawnEgg(float time)
    {
        eggDelayCount += Time.deltaTime;
        if(eggDelayCount <= eggTime)
        {
            return;
        }

        eggSpawner();

        eggDelayCount =0 ;
    }

    private void eggSpawner()
    {
        int row = Random.Range(0, 5);
        int col = Random.Range(0, 9);
        float posX = -0.96f + 0.32f * col;
        float posY = -0.64f + 0.32f * row;
        Vector3 spawnPos = new Vector3(posX, posY, 0);
        
        Instantiate(eggPrefab, spawnPos, this.transform.rotation);
    }

    //private void triggerAtk()
    //{
    //    currentHP = enemy.getEnemyHP();
    //    float hpPercent = ((float)currentHP / enemy.initialHealthPoint) * 100f;

    //    if(hpPercent <= 80f && !atk80Performed)
    //    {
    //        StartMove();
    //        atk80Performed = true;
    //    }
    //    else if (hpPercent <= 60f && !atk60Performed)
    //    {
    //        StartMove();
    //        atk60Performed = true;
    //    }
    //    else if (hpPercent <= 40f && !atk40Performed)
    //    {
    //        StartMove();
    //        atk40Performed = true;
    //    }
    //    else if (hpPercent <= 20f && !atk20Performed)
    //    {
    //        StartMove();
    //        atk20Performed = true;
    //    }
    //}

    private void atkWithTimeCD(float time)
    {
        timeCount += Time.deltaTime;
        if(timeCount >= cdAtkTime)
        {
            StartMove();
            timeCount = 0;
        }
    }

    private Transform checkClosestHero()
    {
        GameObject[] heroes = GameObject.FindGameObjectsWithTag("Player");
        int target = Random.Range(0, heroes.Length);
        return heroes[target].transform;
    }

    private void moveToStartPos()
    {
        oldPos = this.transform.position;
        newPos = startPos;
        float distance = Vector3.Distance(oldPos, newPos);
        direction = (newPos - oldPos).normalized;
        if (distance > 0.02f)
        {
            if (this.transform.position.x < startPos.x)
            {
                this.transform.position += direction * (moveSpeed * 5) * Time.deltaTime;
            }
            else
            {
                this.transform.position += direction * moveSpeed * Time.deltaTime;
            }
        }
        else
        {
            startSpawnEgg = true;
            anim.SetTrigger("stopMove");
            atStartPos = true;
        }
    }
    
    private void dragonMove()
    {
        if (isMoving == true)
        {
            float distance = Vector3.Distance(oldPos, newPos);
            oldPos = this.transform.position;
            if (distance > 0.02f)
            {
                this.transform.position += direction * (moveSpeed * 5) * Time.deltaTime;
            }
            else
            {
                EndMove();
                StartCoroutine(dragonAtk());
            }
        }
    }

    private void StartMove()
    {
        if (isMoving == true) return;
        oldPos = this.transform.position;
        newPos = checkClosestHero().position;

        //khoang cach toi thieu voi target
        newPos.x += 0.72f; 

        anim.SetTrigger("isMove");
        direction = (newPos - oldPos).normalized;
        isMoving = true;
    }

    private void EndMove()
    {
        Debug.Log("stopped");
        anim.SetTrigger("stopMove");
        isMoving = false;
    }

    private IEnumerator dragonAtk()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("isAtk");
        StartCoroutine(dragonBreath());
        yield return new WaitForSeconds(1.5f);
        anim.SetTrigger("isMove");
        atStartPos = false;
    }

    private IEnumerator dragonBreath()
    {
        yield return new WaitForSeconds(atkAnimTime);
        if (sound != null) sound.Play();
        Transform bulletPos = transform.GetChild(0);
        Instantiate(bulletPrefab, bulletPos.position, this.transform.rotation);
    }
}
