using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private SpriteRenderer render;
    private Color originalColor;
    private Color highlight;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
        originalColor = render.material.color;
        highlight = originalColor;
        highlight.a = 0f;
        render.material.color = highlight;
    }

    private void OnMouseEnter()
    {
        render.material.color = originalColor;
    }

    private void OnMouseExit()
    {
        render.material.color = highlight;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            render.material.color = originalColor;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            render.material.color = highlight;
        }
    }
}
