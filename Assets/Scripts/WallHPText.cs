using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class WallHPText : MonoBehaviour
{
    public TMP_Text hpText;
    public WallHealth wallHP;

    private void Start()
    {
        UpdateHP();
        wallHP.onHPChanged += UpdateHP;
    }

    void UpdateHP()
    { 
        hpText.text = wallHP.wallHealth.ToString();
        if (wallHP.wallHealth <= 0)
        {
            hpText.text = "YOU LOSE";
        }
    }

    private void OnValidate() => hpText = GetComponent<TMP_Text>();
}
