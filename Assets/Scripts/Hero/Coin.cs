using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public playAtk stat;
    private int coinValue;
    private GoldValue goldPos;
    private Gold gold;
    [SerializeField] private AudioSource sound;

    private void Start()
    {
        goldPos = FindObjectOfType<GoldValue>();
        gold = FindObjectOfType<Gold>();
        coinValue = stat.damage;
    }

    private void OnMouseDown()
    {
        if (sound != null) sound.Play();
        StartCoroutine(collectGold());
    }

    private IEnumerator collectGold()
    {
        while(transform.position != goldPos.transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, goldPos.transform.position, Time.deltaTime * 5f);
            yield return null;
        }
        
        gold.TakeGold(coinValue);
        if (stat != null)
        {
            stat.resetCoinTimer();
        }
        Destroy(gameObject);
    }
}
