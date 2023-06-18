using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAtk : MonoBehaviour
{
    Animator anim;
    public GameObject bulletPrefab;
    public double firingRate;
    public int buyCost;
    public int damage;
    public int upgradeCost;
    public int upgradeDmg;

    private Gold gold;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        InvokeRepeating("autoAtk", 1f, (float)firingRate);
        gold = FindObjectOfType<Gold>();
    }

    // Update is called once per frame
    //void Update()
    //{

    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        //anim.SetBool("isAtking", true);
    //        //Debug.Log("Left Mouse Button Down");
    //        //Atk();
    //    }
    //    else if (Input.GetMouseButton(1)) {
    //        //anim.SetBool("isAtking", true);
    //        //Atk();
    //    }
    //    else
    //    {
    //        //anim.SetBool("isAtking", false);
    //    }

    //}

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

    void autoAtk()
    {
        if (IsEnemyOnLane() == true) 
        {
            Atk();
        }
    }

    public void upgrade()
    {
        damageValue += upgradeDmg;
        gold.SpendGold(upgradeCost);
    }
}
