using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] private CardObjects card;
    public int damage;
    public float triggerTime = 2f;

    private float timer = 0f;

    [SerializeField] private List<enemyStat> listEnemies = new List<enemyStat>();
    
    [SerializeField] private AudioSource sound;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        timer = triggerTime;

        if (card != null)
        {
            damage += card.dmg;
        }

        listEnemies.Clear();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(listEnemies.Count != 0 && timer >= triggerTime)
        {
            timer = 0;
            StartCoroutine(activeTrap());
        }
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        timer += Time.deltaTime;
    //        if (timer >= triggerTime)
    //        {
    //            enemyStat enemy = collision.gameObject.GetComponent<enemyStat>();

    //            if (enemy != null)
    //            {
    //                StartCoroutine(activeTrap(enemy,damage));
    //            }
    //            timer = 0f;
    //        }
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemyStat enemy = collision.gameObject.GetComponent<enemyStat>();

            if (enemy != null)
            {
                if (!listEnemies.Contains(enemy))
                {
                    listEnemies.Add(enemy);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemyStat enemy = collision.gameObject.GetComponent<enemyStat>();

            if (enemy != null)
            {
                if (listEnemies.Contains(enemy))
                {
                    listEnemies.Remove(enemy);
                }
            }
        }
    }


    private IEnumerator activeTrap()
    {
        if (listEnemies.Count != 0)
        {
            anim.SetTrigger("isAtk");
            yield return new WaitForSeconds(0.2f);
            if (sound != null) sound.Play();

            foreach (enemyStat enemy in listEnemies)
            {
                if (enemy != null) enemy.TakeDamage(damage);
            }
        }
    }
}
