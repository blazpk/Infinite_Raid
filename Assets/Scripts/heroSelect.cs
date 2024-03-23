using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class heroSelect : MonoBehaviour
{
    [SerializeField]
    private LayerMask heroMask;
    [SerializeField]
    private LayerMask zoneMask;
    [SerializeField]
    private LayerMask destroyMask;
    private Camera mainCamera;

    private Vector3 oldPosition;
    private Transform oldChild;
    private Transform oldParent;

    private Gold gold;
    private infoUI info;
    private playAtk thisHeroStat;

    //public GameObject heroInfoUi;
    [SerializeField] private TMP_Text heroLvText;
    [SerializeField] private AudioSource audioPut;
    [SerializeField] private GameObject audioCoin;
    [SerializeField] private AudioSource audioLV;

    private void Start()
    {
        mainCamera = Camera.main;
        thisHeroStat = transform.gameObject.GetComponent<playAtk>();
        info = FindObjectOfType<infoUI>();
        gold = FindObjectOfType<Gold>();
        //heroInfoUi.SetActive(false);
    }

    private void OnMouseDown()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        oldPosition = mousePosition;
        oldParent = transform.parent;

        // hien infoUI
        //info.ui.SetActive(true);
        //info.uiPanel.SetActive(true);
        //Debug.Log(thisHeroStat.damage);
        //UpgradeDmg();
        //thisHeroStat.onDmgChanged += UpgradeDmg;

        //heroInfoUi.SetActive(true);

        //HideOtherHeroUIs();
    }

    private void HideOtherHeroUIs()
    {
        heroSelect[] heroes = FindObjectsOfType<heroSelect>();
        for (int i = 0; i < heroes.Length; i++)
        {
            if (heroes[i] != this)
            {
                //heroes[i].heroInfoUi.SetActive(false);
            }
        }
    }

    private void UpgradeDmg()
    {
        //info.dmgValueText.text = thisHeroStat.damageValue.ToString();
    }

    private void OnMouseDrag()
    {
        transform.parent = null;
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0;
        transform.position = newPosition;
    }

    private void OnMouseUp()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        var hitCollider = Physics2D.OverlapCircle(mousePosition, 0.01f, zoneMask);
        // if no child
        if (hitCollider && hitCollider.transform.childCount == 0)
        {
            transform.SetParent(hitCollider.transform.transform, worldPositionStays: true);
            transform.localPosition = Vector3.back;
            if (audioPut != null) audioPut.Play();
        }
        // if has child - hitCollider.transform.GetChild(0).gameObject.GetComponent<playAtk>().heroID != 1004
        else if (hitCollider && hitCollider.transform.childCount != 0
            && hitCollider.transform.GetChild(0).tag == "Player")
        {
            //merge heroes and lv up
            playAtk targetHero = hitCollider.transform.GetChild(0).GetComponent<playAtk>();
            if(targetHero.heroID == thisHeroStat.heroID && targetHero.heroLV == thisHeroStat.heroLV && thisHeroStat.heroID != 1004)
            {
                hitCollider.transform.parent = null;
                Destroy(hitCollider.transform.GetChild(0).gameObject);
                transform.SetParent(hitCollider.transform.transform, worldPositionStays: true);
                transform.localPosition = Vector3.back;

                thisHeroStat.heroLV += 1;
                if(heroLvText != null) heroLvText.text = thisHeroStat.heroLV.ToString();
                thisHeroStat.damage += thisHeroStat.upgradeDmg * thisHeroStat.heroLV;
                thisHeroStat.buyCost += thisHeroStat.upgradeDmg * thisHeroStat.heroLV;

                if (audioLV != null) audioLV.Play();
            }
            //swap slot
            else
            {
                //lay child ra roi set vao parent cu~
                oldChild = hitCollider.transform.GetChild(0);
                hitCollider.transform.parent = null;
                oldChild.SetParent(oldParent.transform, worldPositionStays: true);
                oldChild.localPosition = Vector3.back;

                //dua hero hien tai vao parent moi
                transform.SetParent(hitCollider.transform.transform, worldPositionStays: true);
                transform.localPosition = Vector3.back;
                if (audioPut != null) audioPut.Play();
            }
            
        }
        else
        {
            transform.SetParent(oldParent.transform, worldPositionStays: true);
            transform.localPosition = Vector3.back;
        }

        var hitColliderDestroy = Physics2D.OverlapCircle(mousePosition, 0.1f, destroyMask);
        if (hitColliderDestroy)
        {
            if (audioCoin != null) Instantiate(audioCoin, transform.position, transform.rotation);
            playAtk cost = transform.gameObject.GetComponent<playAtk>();
            gold.TakeGold(cost.buyCost / 2);
            Destroy(gameObject);
        }
        //newHero = null;
    }

}
