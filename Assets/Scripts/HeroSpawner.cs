using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeroSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform heroPrefab;
    [SerializeField]
    private LayerMask cellMask;

    private Transform newHero;
    private Camera mainCamera;

    private Gold gold;
    public int cost;

    private void Start()
    {
        mainCamera = Camera.main;
        gold = FindObjectOfType<Gold>();
        //cost = FindObjectOfType<playAtk>();
    }

    private void OnMouseDown()
    {
        SpawnHero();
    }

    private void SpawnHero()
    {
        if(newHero == null && gold.goldCount >= cost)
        {
            newHero = Instantiate(heroPrefab, this.transform.position, this.transform.rotation);
        }
    }

    private void OnMouseDrag()
    {
        if(newHero != null)
        {
            Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z = 0;
            newHero.position = newPosition;
        }
    }

    private void OnMouseUp()
    {
        if (newHero == null) return;

        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        var hitCollider = Physics2D.OverlapCircle(mousePosition, 0.1f, cellMask);
        if (hitCollider && hitCollider.transform.childCount == 0)
        {
            newHero.SetParent(hitCollider.transform.transform, worldPositionStays: true);
            newHero.localPosition = Vector3.zero;
            if (gold.goldCount >= cost)
            {
                gold.SpendGold(cost);
            }
        }
        else
        {
            Destroy(newHero.gameObject);
        }
        newHero = null;
    }
}
