using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroSpawner : MonoBehaviour
{
    [SerializeField] private int cardID;
    [SerializeField] private Transform heroPrefab;
    [SerializeField] private Transform heroSprite;
    [SerializeField] private LayerMask zoneMask;

    [SerializeField] private Image cardCD;
    [SerializeField] private float cdTime;
    private float startCDTime;
    private bool is1stCD = false;
    private bool onCD = false;

    private GameObject heroSelectCanvas;

    private Transform newHero;
    private Transform newHeroSprite;
    private Camera mainCamera;

    private Gold gold;
    public int cost;
    [SerializeField] private AudioSource sound;

    private void Start()
    {
        mainCamera = Camera.main;
        gold = FindObjectOfType<Gold>();
        heroSelectCanvas = GameObject.FindGameObjectWithTag("HeroSelect");
    }

    private void Update()
    {
        cdOnStart();
        OnCooldown();

        if(gold.goldCount < cost)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private void OnMouseDown()
    {
        //unselect hero in hero select mode _ if hero select canvas is active
        if (heroSelectCanvas.activeSelf)
        {
            Destroy(gameObject);
        }
        else
        {
            SpawnHeroSprite();
        }
    }

    private void SpawnHeroSprite()
    {
        if (newHeroSprite == null && gold.goldCount >= cost)
        {
            newHeroSprite = Instantiate(heroSprite, this.transform.position + Vector3.back, this.transform.rotation);
            
        }
    }

    private void SpawnHero(Transform obj)
    {
        if(newHero == null && gold.goldCount >= cost)
        {
            newHero = Instantiate(heroPrefab, obj.position + Vector3.back, obj.rotation);
            if (sound != null) sound.Play();
        }
    }

    private void OnMouseDrag()
    {
        if(newHeroSprite != null)
        {
            Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z = 0;
            newHeroSprite.position = newPosition;
        }
    }

    private void OnMouseUp()
    {
        if (newHeroSprite == null) return;

        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        var hitCollider = Physics2D.OverlapCircle(mousePosition, 0.01f, zoneMask);
        if (hitCollider && hitCollider.transform.childCount == 0)
        {
            SpawnHero(newHeroSprite.transform);
            newHero.SetParent(hitCollider.transform.transform, worldPositionStays: true);
            newHero.localPosition = Vector3.back;
            if (gold.goldCount >= cost)
            {
                gold.SpendGold(cost);
            }
            StartCooldown();
            Destroy(newHeroSprite.gameObject);
        }
        else
        {
            Destroy(newHeroSprite.gameObject);
        }
        newHeroSprite = null;
        newHero = null;
    }

    private void StartCooldown()
    {
        onCD = true;
        gameObject.GetComponent<Collider2D>().enabled = false;

        //check cd when start game
        if (is1stCD == false)
        {
            cardCD.fillAmount = (cdTime - startCDTime) / cdTime;
        }
        else
        {
            cardCD.fillAmount = 1;
        }
    }

    private void OnCooldown()
    {
        if (onCD == false) return;

        //because max fill = 1, so that time must divide by cdTime

        cardCD.fillAmount -= Time.deltaTime / cdTime;


        if (cardCD.fillAmount <= 0)
        {
            if (is1stCD == false) is1stCD = true;
            onCD = false;
            gameObject.GetComponent<Collider2D>().enabled = true;
        }
    }

    public void cdOnStart()
    {
        if (is1stCD == true || Time.timeScale == 0 || onCD == true) return;

        if (cdTime <= 30 && cdTime >= 16)
        {
            startCDTime = 10;
            StartCooldown();
        }
        else if (cdTime >= 31)
        {
            startCDTime = 15;
            StartCooldown();
        }
    }
}
