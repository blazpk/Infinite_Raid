using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHealth : MonoBehaviour
{
    public int initialWallHealth;
    private int wallHealthValue;

    public int wallHealth
    {
        get => wallHealthValue;
        private set
        {
            wallHealthValue = value;
            onHPChanged?.Invoke();
        }
    }

    public System.Action onHPChanged;

    void Start()
    {
        wallHealth = initialWallHealth;
    }

    public void TakeDamage(int damage)
    {
        wallHealth -= damage;
    }
}
