using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseHeroBattle : MonoBehaviour
{
    public GameObject heroCard;
    public Transform[] heroBoard;
    public int sortOrder = 2;
    private bool isChoose = false;
    private int parent;
    [SerializeField] private AudioSource clickSound;

    public void OnClick()
    {   
        for (int i = 0; i < heroBoard.Length; i++)
        {
            if(heroBoard[i].childCount == 0)
            {
                GameObject hero = Instantiate(heroCard, heroBoard[i].position + Vector3.back, heroBoard[i].rotation);
                hero.transform.SetParent(heroBoard[i]);
                if (clickSound != null) clickSound.Play();

                parent = i;
                isChoose = true;
                gameObject.GetComponent<Button>().interactable = false;
                break;
            }
        }
    }

    private void Update()
    {
        if(isChoose == true)
        {
            if (heroBoard[parent].childCount == 0)
            {
                gameObject.GetComponent<Button>().interactable = true;
                isChoose = false;
            }
        }
    }
}
