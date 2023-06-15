using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    public int initialGold;
    private int goldValue;

    public int goldCount
    {
        get => goldValue;
        private set
        {
            goldValue = value;
            onGoldChanged?.Invoke();
        }
    }

    public System.Action onGoldChanged;

    
    void Start()
    {
        goldValue = initialGold;
    }

    public void TakeGold(int gold)
    {
        goldCount += gold;
    }

    public void SpendGold(int gold)
    {
        goldCount -= gold;
        if(goldCount < 0)
        {
            goldCount = 0;
        }
    }

    
}
