using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldValue : MonoBehaviour
{
    public TMP_Text goldText;
    public Gold gold;

    private void Start()
    {
        UpdateGold();
        gold.onGoldChanged += UpdateGold;
    }

    void UpdateGold()
    {
        goldText.text = gold.goldCount.ToString();
        if (gold.goldCount <= 0)
        {
            //goldText.text = "0";
        }
    }
}
