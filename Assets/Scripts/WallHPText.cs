using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WallHPText : MonoBehaviour
{
    public TMP_Text hpText;
    public Health wallHP;

    public GameObject losePanel;

    private void Start()
    {
        UpdateHP();
        wallHP.onHPChanged += UpdateHP;
    }

    void UpdateHP()
    { 
        hpText.text = wallHP.HP.ToString();
        if (wallHP.HP <= 0)
        {
            hpText.text = "YOU LOSE";

            losePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    //private void OnValidate() => hpText = GetComponent<TMP_Text>();
}
