using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int bombDmg = 15;
    private Vector2 exploRadius;

    Collider2D[] inExploRadius;
    Animator anim;
    [SerializeField] private GameObject sound;
    [SerializeField] private GameObject exploAnim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        exploRadius = new Vector2(0.96f, 0.96f);
        StartCoroutine(BombTrigger());
    }

    private IEnumerator BombTrigger()
    {
        anim.SetTrigger("isTrigger");
        yield return new WaitForSeconds(0.8f);
        Instantiate(exploAnim, transform.position, transform.rotation);
        GameObject audioIns = Instantiate(sound, transform.position, transform.rotation);
        BombExplosion();
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

    private void BombExplosion()
    {
        inExploRadius = Physics2D.OverlapBoxAll(transform.position, exploRadius, 0f);

        foreach (Collider2D col in inExploRadius)
        {
            if (col.gameObject.tag == "Enemy")
            {
                enemyStat enemy = col.gameObject.GetComponent<enemyStat>();

                if (enemy != null)
                {
                    enemy.TakeDamage(bombDmg);
                }
            }
        }
    }
}
