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
        Destroy(gameObject, 3);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Debug.Log("bullet hit enemy");
            //Destroy(gameObject);

            enemyStat enemy = collision.gameObject.GetComponent<enemyStat>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage.damage);
            }
        }
    }

}
