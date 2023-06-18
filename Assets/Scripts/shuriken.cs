using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shuriken : MonoBehaviour
{
    //public int damage;
    private playAtk damage;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        damage = FindObjectOfType<playAtk>();
    }

    //private void DeliveryDamage(RaycastHit2D hitInfo)
    //{
    //    var health = hitInfo.collider.GetComponent<enemyStat>();
    //    if (health != null) 
    //    {
    //        health.TakeDamage(damage);
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Debug.Log("bullet hit enemy");
            Destroy(gameObject);

            enemyStat enemy = collision.gameObject.GetComponent<enemyStat>();

            if (enemy != null) 
            {
                enemy.TakeDamage(damage.damage);
            }
        }
    }

    //public int getDamage()
    //{
    //    return damage;
    //}
}
