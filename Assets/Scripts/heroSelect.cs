using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class heroSelect : MonoBehaviour
{
    [SerializeField]
    private LayerMask heroMask;
    [SerializeField]
    private LayerMask cellMask;
    [SerializeField]
    private LayerMask destroyMask;
    private Camera mainCamera;

    private Vector3 oldPosition;
    private Transform oldChild;
    private Transform oldParent;

    private Gold gold;
    private infoUI info;
    private playAtk dmg;

    public GameObject heroInfoUi;

    private void Start()
    {
        mainCamera = Camera.main;
        dmg = transform.gameObject.GetComponent<playAtk>();
        info = FindObjectOfType<infoUI>();
        gold = FindObjectOfType<Gold>();
        heroInfoUi.SetActive(false);

    }

    private void OnMouseDown()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        oldPosition = mousePosition;
        oldParent = transform.parent;

        // hien infoUI
        info.ui.SetActive(true);
        info.uiPanel.SetActive(true);
        Debug.Log(dmg.damage);
        UpgradeDmg();
        dmg.onDmgChanged += UpgradeDmg;

        heroInfoUi.SetActive(true);

        HideOtherHeroUIs();
    }

    private void HideOtherHeroUIs()
    {
        heroSelect[] heroes = FindObjectsOfType<heroSelect>();
        for (int i = 0; i < heroes.Length; i++)
        {
            if (heroes[i] != this)
            {
                heroes[i].heroInfoUi.SetActive(false);
            }
        }
    }

    private void UpgradeDmg()
    {
        info.dmgValueText.text = dmg.damageValue.ToString();
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

        var hitCollider = Physics2D.OverlapCircle(mousePosition, 0.1f, cellMask);
        if (hitCollider && hitCollider.transform.childCount == 0)
        {
            transform.SetParent(hitCollider.transform.transform, worldPositionStays: true);
            transform.localPosition = Vector3.zero;
        }
        else if (hitCollider && hitCollider.transform.childCount != 0)
        {
            //lay child ra roi set vao parent cu~
            oldChild = hitCollider.transform.GetChild(0);
            hitCollider.transform.parent = null;
            oldChild.SetParent(oldParent.transform, worldPositionStays: true);
            oldChild.localPosition = Vector3.zero;

            //dua hero hien tai vao parent moi
            transform.SetParent(hitCollider.transform.transform, worldPositionStays: true);
            transform.localPosition = Vector3.zero;
        }
        else
        {
            transform.SetParent(oldParent.transform, worldPositionStays: true);
            transform.localPosition = Vector3.zero;
        }

        var hitColliderDestroy = Physics2D.OverlapCircle(mousePosition, 0.1f, destroyMask);
        if (hitColliderDestroy)
        {
            playAtk cost = transform.gameObject.GetComponent<playAtk>();
            gold.TakeGold(cost.buyCost);
            Destroy(gameObject);
        }
        //newHero = null;
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            //anim.SetBool("isAtking", true);
            //Debug.Log("Left Mouse Button Down");
            //Atk();
        }
        else if (Input.GetMouseButton(1))
        {
            info.ui.SetActive(false);
            info.uiPanel.SetActive(false);
            heroInfoUi.SetActive(false);
        }
        else
        {
            //anim.SetBool("isAtking", false);
        }

    }
}
