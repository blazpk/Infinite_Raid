using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterMovement : MonoBehaviour
{
    public float moveSpeed = 0.2f;

    private float originalSpeed;
    private float slowTime;
    private bool isSlow = false;

    Rigidbody2D rb;
    SpriteRenderer sprite;
    //public int enemyDamage;

    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        direction.x = -1;
        originalSpeed = moveSpeed;
        //transform.position = transform.position + new Vector3(-1 * speed * Time.deltaTime, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = transform.position + new Vector3(-1 * moveSpeed * Time.deltaTime, 0, 0);
        if(isSlow == false)
        {
            transform.Translate(direction.normalized * originalSpeed * Time.deltaTime);
        }
        else
        {
            slowTime -= Time.deltaTime;
            transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);

            if (slowTime <= 0)
            {
                isSlow = false;
                sprite.color = new Color(255, 255, 255, 255);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            //Debug.Log("enemy hit wall");

            enemyStat enemy = gameObject.GetComponent<enemyStat>();

            if (enemy != null)
            {
                enemy.Die();
            }

            WallHealth wall = collision.gameObject.GetComponent<WallHealth>();
            if (wall != null)
            {
                wall.TakeDamage(enemy.enemyDamage);
            }
        }
    }
  
    public void SlowEffect(float percent, float duration)
    {
        if(isSlow == false)
        {
            moveSpeed = moveSpeed * (1f - percent);
            slowTime = duration;
            sprite.color = new Color(56, 204, 233, 255);
            isSlow = true;
        }
        else
        {
            slowTime = duration;
        }
    }
}
