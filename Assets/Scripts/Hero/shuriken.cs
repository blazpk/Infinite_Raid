using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shuriken : MonoBehaviour
{
    private shurikenMoving damage;
    [SerializeField] private GameObject sound;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        damage = GetComponent<shurikenMoving>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss")
        {
            //Debug.Log("bullet hit enemy");
            Destroy(gameObject);

            enemyStat enemy = collision.gameObject.GetComponent<enemyStat>();

            if (enemy != null)
            {
                if (sound != null) Instantiate(sound, transform.position, transform.rotation);
                enemy.TakeDamage(damage.damage);
            }
        }
    }
}
