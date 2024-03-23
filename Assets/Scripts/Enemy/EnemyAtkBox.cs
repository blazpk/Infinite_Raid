using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtkBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            Health wall = collision.gameObject.GetComponent<Health>();
            monsterMovement enemy = GetComponentInParent<monsterMovement>();
            if (enemy != null)
            {
                enemy.OnAtkBoxTrigger(wall);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        monsterMovement enemy = GetComponentInParent<monsterMovement>();
        if (collision.gameObject.tag == "Wall")
        {
            enemy.OnAtkBoxExit();
        }
    }
}
