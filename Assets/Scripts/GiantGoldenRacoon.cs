using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantGoldenRacoon : MonoBehaviour
{
    Rigidbody2D rb;
    private enemyStat enemy;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy = gameObject.GetComponent<enemyStat>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void changeLane()
    {
        
    }
}
