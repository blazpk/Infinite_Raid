using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBullet : MonoBehaviour
{
    public float slowPercent;
    public float slowDuration;

    //private playAtk damage;
    private shurikenMoving damage;
    [SerializeField] private GameObject audioHit;
    [SerializeField] private GameObject audioIce;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //damage = FindObjectOfType<playAtk>();
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
                enemy.TakeDamage(damage.damage);

                if (collision.gameObject.tag == "Enemy")
                {
                    if (audioIce != null) Instantiate(audioIce, transform.position, transform.rotation);
                    monsterMovement enemyMove = collision.gameObject.GetComponent<monsterMovement>();
                    enemyMove.SlowEffect(slowPercent, slowDuration);
                }
                else if(collision.gameObject.tag == "Boss")
                {
                    if (audioHit != null) Instantiate(audioHit, transform.position, transform.rotation);
                }
            }
        }
    }
}
