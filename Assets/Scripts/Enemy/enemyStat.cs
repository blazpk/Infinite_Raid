using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStat : MonoBehaviour
{
    public int initialHealthPoint;
    public int enemyDamage;
    public int enemyGold;
    [SerializeField] private bool isNotCountDead;

    private int healthPointValue;
    private Gold gold;
    private GameManager killed;
    private monsterMovement enemy;
    private bool _isDead = false;

    public bool IsDead => healthPointValue <= 0;
    Animator anim;

    public int HealthPoint 
    {
        get => healthPointValue;
        set => healthPointValue = value;
    }

    private void Start()
    {
        HealthPoint = initialHealthPoint;
        gold = FindObjectOfType<Gold>(); //link to gold component
        killed = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
        enemy = GetComponent<monsterMovement>();
    }

    public void TakeDamage(int damage)
    {
        Transform childShield = transform.Find("Shield");
        if(childShield == null)
        {
            HealthPoint -= damage;
        }
        
        if(IsDead && gameObject.name == "Shield")
        {
            StartCoroutine(destroyShield());
        }

        if (IsDead && gameObject.name != "Shield")
        {
            gold.TakeGold(enemyGold);
            Die();
            killed.updateKill();
            //Debug.Log("enemy die " + killed.showKill());
        }
    }

    public void Die()
    {
        if (_isDead == true) return;

        _isDead = true;
        if (gameObject.tag == "BlackDragon")
        {
            BlackDragonAI boss = GetComponent<BlackDragonAI>();
            boss.StopAllCoroutines();
        }
        StartCoroutine(dieAnim());
        //Destroy(gameObject);
        
        
        Debug.Log("enemy die " + killed.showDieEnemy());
    }

    private IEnumerator dieAnim()
    {
        if (enemy != null)
        {
            enemy.direction.x = 0;
        }
        
        anim.SetTrigger("isDead");
        
        if (gameObject.tag == "Boss")
        {
            yield return new WaitForSeconds(1f);
            anim.SetTrigger("isDestroy");
        }

        yield return new WaitForSeconds(0.5f);

        if (isNotCountDead == false)
        {
            killed.updateEnemyDieCount();
        }

        Destroy(gameObject);
    }

    private IEnumerator destroyShield()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    public int getEnemyHP()
    {
        return healthPointValue;
    }
}
