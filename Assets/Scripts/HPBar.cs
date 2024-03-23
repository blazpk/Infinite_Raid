using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    [SerializeField] private Slider hpBar;
    [SerializeField] private Image fill;
    [SerializeField] private GameObject losePanel;
    private Health wallHP;

    private void Start()
    {
        wallHP = wall.GetComponent<Health>();
        hpBar.maxValue = wallHP.initialHealth;
        hpBar.value = wallHP.HP;
        wallHP.onHPChanged += UpdateHPBar;
    }

    private void UpdateHPBar()
    {
        hpBar.value = wallHP.HP;
        if(wallHP.HP <= wallHP.initialHealth * 0.6)
        {
            fill.color = Color.yellow;
        }
        if (wallHP.HP <= wallHP.initialHealth * 0.3)
        {
            fill.color = Color.red;
        }

        if(wallHP.HP <= 0)
        {
            losePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
