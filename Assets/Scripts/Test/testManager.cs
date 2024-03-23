using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class testManager : MonoBehaviour
{
    public GameObject prefab;
    public CardObjects card;
    public TMP_Text text;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Instantiate(prefab, transform.position, transform.rotation);
            PlayerPrefs.SetInt(card.cardID, card.dmg);
            PlayerPrefs.Save();
        }

        if (Input.GetMouseButtonDown(1))
        {
            card.dmg += 1;
        }

        text.text = card.dmg.ToString();
    }

    public void Save()
    {
        testSave.SaveData(card);
    }

    public void Load()
    {
        card = testSave.LoadData(card);
    }
}
