using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hero Upgrade", menuName = "HeroStat")]
public class CardObjects : ScriptableObject
{
    public string cardID;
    public int dmg;
    public int hp;
    public int upgradeLV;

    private void Awake()
    {
        dmg = PlayerPrefs.GetInt(cardID + "_Atk", 0);
        hp = PlayerPrefs.GetInt(cardID + "_HP", 0);
    }
    
}
