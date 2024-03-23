using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAtk : MonoBehaviour
{
    Animator anim;
    [SerializeField] private CardObjects card;
    public GameObject bulletPrefab;
    public float firingRate;
    public int buyCost;
    public int damage;
    public int upgradeCost;
    public int upgradeDmg;
    public int heroID;
    public int heroLV = 0;
    private float delayCount = 0f;
    private bool isSpawnCoin = false;
    [SerializeField] private AudioSource sound;

    private Gold gold;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        gold = FindObjectOfType<Gold>();

        if (heroID != 1004)
        {
            InvokeRepeating("autoAtk", 1f, firingRate);
        }

        if (card != null)
        {
            damage += card.dmg;
        }
    }

    private void Update()
    {
        if (heroID == 1004)
        {
            spawnCoin();
        }
    }

    public int damageValue
    {
        get => damage;
        private set
        {
            damage = value;
            onDmgChanged?.Invoke();
}
    }

    public System.Action onDmgChanged;

    void Atk() {
        anim.SetTrigger("isAtk");
        var bullet = Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);
        if (sound != null) sound.Play();
        shurikenMoving bulletDmg = bullet.GetComponent<shurikenMoving>();
        bulletDmg.damage = damage;
    }

    bool IsEnemyOnLane()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            //if (enemy.transform.position.y == this.transform.position.y)
            if (((enemy.transform.position.y + 0.01) >= this.transform.position.y) &&
                ((enemy.transform.position.y - 0.01) <= this.transform.position.y))
            {
                return true;
            }
        }
        return false;
    }

    bool IsBossOnLane()
    {
        GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss");
        foreach (GameObject boss in bosses)
        {
            //if (enemy.transform.position.y == this.transform.position.y)
            if (((boss.transform.position.y + 0.33) >= this.transform.position.y) &&
                ((boss.transform.position.y - 0.33) <= this.transform.position.y))
            {
                return true;
            }
        }
        return false;
    }

    void autoAtk()
    {
        if (IsEnemyOnLane() == true || IsBossOnLane() == true) 
        {
            Atk();
        }
    }

    public void upgrade()
    {
        damageValue += upgradeDmg;
        gold.SpendGold(upgradeCost);
    }

    private void spawnCoin()
    {
        delayCount += Time.deltaTime;
        if (delayCount <= firingRate || isSpawnCoin == true)
        {
            return;
        }
        StartCoroutine(spawnCoinAnim());
    }

    private IEnumerator spawnCoinAnim()
    {
        isSpawnCoin = true;
        anim.SetTrigger("isWake");
        yield return new WaitForSeconds(2f);
        anim.SetTrigger("isItem");
        Transform spawnPos = transform.GetChild(0);
        var newCoin = Instantiate(bulletPrefab, spawnPos.position + Vector3.back, this.transform.rotation);
        newCoin.GetComponent<Coin>().stat = this.gameObject.GetComponent<playAtk>();
        if (sound != null) sound.Play();
    }

    public void resetCoinTimer()
    {
        anim.SetTrigger("isSleep");
        isSpawnCoin = false;
        delayCount = 0;
    }
}
