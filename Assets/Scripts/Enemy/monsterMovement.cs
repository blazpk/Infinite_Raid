using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterMovement : MonoBehaviour
{
    public float moveSpeed = 0.2f;
    public float delayAtkTime = 1f;
    public float atkAnimTime;

    private float delayAtkCount = 0f;
    private float originalSpeed;
    private float slowTime;
    private bool isSlow = false;
    private Coroutine coroutine;
    private bool isAtking = false;
    private enemyStat stat;

    [SerializeField] private AudioSource hitSound;

    [Header("Range Enemy:")]
    public float atkRange;
    public bool canAtkFar = false;
    public GameObject bulletPrefab;
    [SerializeField] private AudioSource atkSound;

    Rigidbody2D rb;
    SpriteRenderer sprite;
    Animator anim;

    public Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        stat = GetComponent<enemyStat>();
        direction.x = -1;
        originalSpeed = moveSpeed;
        //delayAtkCount = delayAtkCount + Random.Range(0f, 0.5f);
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
                sprite.color = Color.white;
            }
        }

        delayAtkCount += Time.deltaTime;

        if (canAtkFar == true)
        {
            if(this.transform.position.x <= atkRange)
            {
                direction.x = 0;
                anim.SetTrigger("stopMove");
                Shoot();
            }
        }

        //lose condition
        if(transform.position.x <= -2.1f) 
        {
            monsterPassWall();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Player") && isAtking == false && gameObject.name != "DragonEgg")
        {
            enemyStat enemy = gameObject.GetComponent<enemyStat>();
            Health wall = collision.gameObject.GetComponent<Health>();

            direction.x = 0;
            
            if (wall != null && enemy != null && gameObject.name != "Shield")
            {
                coroutine = StartCoroutine(repeatAtk(wall, enemy.enemyDamage, delayAtkTime));

                //wall.TakeDamage(enemy.enemyDamage);
            }
        }

        if(collision.gameObject.tag == "Bullet")
        {
            StartCoroutine(flashEffect());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Player") && coroutine != null && gameObject.name != "DragonEgg")
        {
            StartCoroutine(moveAfterDestroyPlayer(1));
        }
    }
    //call only when enemy atk box hit player's obj
    public void OnAtkBoxTrigger(Health wall)
    {
        enemyStat enemy = gameObject.GetComponent<enemyStat>();
        direction.x = 0;

        if (gameObject.name != "Shield")
        {
            isAtking = true;
            coroutine = StartCoroutine(repeatAtk(wall, enemy.enemyDamage, delayAtkTime));
        }
    }
    //call only when enemy atk box exit player's obj
    public void OnAtkBoxExit()
    {
        if (coroutine != null)
        {
            StartCoroutine(moveAfterDestroyPlayer(1));
        }
    }

    private IEnumerator moveAfterDestroyPlayer(float time)
    {
        yield return new WaitForSeconds(time);
        anim.SetTrigger("isMove");
        StopCoroutine(coroutine);
        direction.x = -1;
    }

    private IEnumerator repeatAtk(Health wall, int dmg, float time)
    {
        while (true)
        {
            yield return StartCoroutine(Atk());
            yield return wallTakeDmg(wall, dmg);
            yield return new WaitForSeconds(time);
        }
    }

    private IEnumerator Atk()
    {
        anim.SetTrigger("isAtk");
        yield return new WaitForSeconds(0.3f);
        if(hitSound != null) hitSound.Play();
        yield return new WaitForSeconds(0.2f);
    }

    private IEnumerator wallTakeDmg(Health wall, int dmg)
    {
        if(wall != null) wall.TakeDamage(dmg);
        yield return null;
    }

    private void Shoot()
    {
        delayAtkCount += Time.deltaTime;
        if (delayAtkCount <= delayAtkTime)
        {
            return;
        }

        anim.SetTrigger("isAtk");
        StartCoroutine(BulletShoot());
        //to make random different between all enemy
        delayAtkCount = Random.Range(-0.3f, 0.3f);
    }

    private IEnumerator BulletShoot()
    {
        //wait shooting anim is done
        yield return new WaitForSeconds(atkAnimTime);
        if(atkSound != null) atkSound.Play();
        Transform bulletPos = transform.GetChild(0);
        var bullet = Instantiate(bulletPrefab, bulletPos.position, this.transform.rotation);
        bullet.GetComponent<EnemyBullet>().damage = stat.enemyDamage;

    }

    public void SlowEffect(float percent, float duration)
    {
        if(isSlow == false)
        {
            moveSpeed = moveSpeed * (1f - percent);
            slowTime = duration;
            //sprite.color = new Color(56, 204, 233, 255);
            sprite.color = (Color)(new Color32(56, 204, 233, 255));
            //sprite.color = Color.blue;
            isSlow = true;
        }
        else
        {
            slowTime = duration;
        }
    }

    private IEnumerator flashEffect()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        if (isSlow == true) { sprite.color = (Color)(new Color32(56, 204, 233, 255)); }
        else sprite.color = Color.white;
    }

    private void monsterPassWall()
    {
        GameManager obj = FindObjectOfType<GameManager>();
        obj.losePanel.SetActive(true);
        Time.timeScale = 0;
    }
}
