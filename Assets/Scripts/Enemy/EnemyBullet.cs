using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    public int damage;

    //[SerializeField] private AudioClip hitSound;
    [SerializeField] private GameObject _hitSound;

    private Vector2 direction;

    Rigidbody2D rb;

    void Start()
    {
        direction.x = -1;
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Player")
        {
            Health wall = collision.gameObject.GetComponent<Health>();
            if (wall != null)
            {
                //if (hitSound != null) AudioSource.PlayClipAtPoint(hitSound, transform.position);
                //if (hitSound != null) hitSound.Play();
                if(_hitSound != null) Instantiate(_hitSound, transform.position, transform.rotation);
                wall.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
