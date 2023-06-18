using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterMovement : MonoBehaviour
{
    public float moveSpeed = 0.2f;
    Rigidbody2D rb;
    //public int enemyDamage;

    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction.x = -1;
        //transform.position = transform.position + new Vector3(-1 * speed * Time.deltaTime, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = transform.position + new Vector3(-1 * moveSpeed * Time.deltaTime, 0, 0);
        transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Debug.Log("enemy hit wall");

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
}
