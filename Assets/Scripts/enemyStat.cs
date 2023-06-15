using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStat : MonoBehaviour
{
    public int initialHealthPoint;
    public int enemyDamage;
    public int enemyGold;

    private int healthPointValue;
    private Gold gold;
    private GameManager killed;

    public bool IsDead => healthPointValue <= 0;

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
    }

    public void TakeDamage(int damage)
    {
        HealthPoint -= damage;
        if (IsDead)
        {
            gold.TakeGold(enemyGold);
            Die();
            killed.updateKill();
            //Debug.Log("enemy die " + killed.showKill());
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        killed.updateEnemyDieCount();
        Debug.Log("enemy die " + killed.showDieEnemy());
    }
}
