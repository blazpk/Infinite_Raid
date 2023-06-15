using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shurikenMoving : MonoBehaviour
{
    public float speed = 0.8f;

    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        direction.x = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = transform.position + new Vector3(1 * speed * Time.deltaTime, 0, 0);
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        //Debug.Log("destroyed when off screen");
        Destroy(gameObject);
    }
}
