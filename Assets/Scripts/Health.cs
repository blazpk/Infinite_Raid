using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private CardObjects card;
    public int initialHealth;
    private int healthValue;
    SpriteRenderer sprite;

    public int HP
    {
        get => healthValue;
        private set
        {
            healthValue = value;
            onHPChanged?.Invoke();
        }
    }

    public System.Action onHPChanged;

    void Start()
    {
        if(card != null)
        {
            initialHealth += card.hp;
        }
        HP = initialHealth;

        if(gameObject.GetComponent<SpriteRenderer>() != null)
        {
            sprite = GetComponent<SpriteRenderer>();
        }
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        if(HP <= 0)
        {
            Destroy(gameObject);
        }
        StartCoroutine(flashEffect());
    }

    private IEnumerator flashEffect()
    {
        if(sprite != null)
        {
            var originalColor = sprite.color;
            sprite.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            sprite.color = originalColor;
        }
    }
}
