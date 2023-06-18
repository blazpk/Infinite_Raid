using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicShuriken : MonoBehaviour
{
    //public int damage;
    private playAtk damage;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        damage = FindObjectOfType<playAtk>();
    }

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

}
