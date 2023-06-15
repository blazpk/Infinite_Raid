using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroUpgrade : MonoBehaviour
{
    [SerializeField] private playAtk hero;

    public void OnClick()
    {
        hero.upgrade();
    }
}
