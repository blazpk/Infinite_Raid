using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBullet : MonoBehaviour
{
    public float slowPercent;
    public float slowDuration;

    private playAtk damage;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        damage = FindObjectOfType<playAtk>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
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

            monsterMovement enemyMove = collision.gameObject.GetComponent<monsterMovement>();

            if (enemy != null)
            {
                enemyMove.SlowEffect(slowPercent, slowDuration);
            }
        }
    }
}
