using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayDestroy : MonoBehaviour
{
    [SerializeField] private float delayTime = 2f;

    void Start()
    {
        StartCoroutine(delayDestroy());
    }

    private IEnumerator delayDestroy()
    {
        yield return new WaitForSeconds(delayTime);
        Destroy(gameObject);
    }
}
