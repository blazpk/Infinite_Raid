using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class WallHPText : MonoBehaviour
{
    public TMP_Text hpText;
    public WallHealth wallHP;

    public GameObject losePanel;

    private void Start()
    {
        UpdateHP();
        wallHP.onHPChanged += UpdateHP;

        losePanel.SetActive(false);
    }

    void UpdateHP()
    { 
        hpText.text = wallHP.wallHealth.ToString();
        if (wallHP.wallHealth <= 0)
        {
            hpText.text = "YOU LOSE";

            losePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void OnValidate() => hpText = GetComponent<TMP_Text>();
}
