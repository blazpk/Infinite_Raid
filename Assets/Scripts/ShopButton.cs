using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ShopButton : MonoBehaviour
{
    [SerializeField] private TMP_Text[] text;
    [SerializeField] private GameObject[] hero;
    [SerializeField] private CardObjects[] card;
    [SerializeField] private AudioSource clickSound;
    [SerializeField] private TMP_Text diaText;
    private int spendTime = 0;

    private void Start()
    {
        //cheat line
        //PlayerPrefs.SetInt("Diamond", 0);
        //PlayerPrefs.Save();

        //unupgrade heroes
        //for (int i = 0; i < card.Length; i++)
        //{
        //    PlayerPrefs.SetInt(card[i].cardID + "_HP", 0);
        //    PlayerPrefs.SetInt(card[i].cardID + "_Atk", 0);
        //    PlayerPrefs.Save();
        //}

        diaText.text = PlayerPrefs.GetInt("Diamond", 0).ToString();
    }

    private void Update()
    {
        for (int i = 0; i < hero.Length; i++)
        {
            int atk;
            int hp;

            if (hero[i].GetComponent<playAtk>() != null)
            {
                atk = hero[i].GetComponent<playAtk>().damage + card[i].dmg;
            }
            else if (hero[i].GetComponent<SpikeTrap>() != null)
            {
                atk = hero[i].GetComponent<SpikeTrap>().damage + card[i].dmg;
            }
            else
            {
                atk = 0;
            }

            if (hero[i].GetComponent<Health>() != null)
            {
                hp = hero[i].GetComponent<Health>().initialHealth + card[i].hp;
            }
            else
            {
                hp = 0;
            }

            text[i * 2].text = atk.ToString();
            text[(i * 2) + 1].text = hp.ToString();
        }
    }

    public void addAtk(CardObjects card)
    {
        if (PlayerPrefs.GetInt("Diamond", 0) < 50) return;

        if(card.dmg < 20)
        {
            card.dmg += 1;
        }
        else
        {
            card.dmg += card.dmg / 10;
        }
        spendDiamond();
        if (clickSound != null) clickSound.Play();
    }

    public void addHp(CardObjects card)
    {
        if (PlayerPrefs.GetInt("Diamond", 0) < 50) return;

        if (card.hp < 20)
        {
            card.hp += 1;
        }
        else
        {
            card.hp += card.hp / 10;
        }
        spendDiamond();
        if (clickSound != null) clickSound.Play();
    }

    public void saveButton()
    {
        for (int i = 0; i < card.Length; i++)
        {
            PlayerPrefs.SetInt(card[i].cardID + "_HP", card[i].hp);
            PlayerPrefs.SetInt(card[i].cardID + "_Atk", card[i].dmg);
            PlayerPrefs.Save();
        }
        spendTime = 0;
        if (clickSound != null) clickSound.Play();
    }

    public void backButton()
    {
        for (int i = 0; i < card.Length; i++)
        {
            card[i].hp = PlayerPrefs.GetInt(card[i].cardID + "_HP", 0);
            card[i].dmg = PlayerPrefs.GetInt(card[i].cardID + "_Atk", 0);
        }
        //return dia if cancel upgrade
        if(spendTime > 0)
        {
            PlayerPrefs.SetInt("Diamond", PlayerPrefs.GetInt("Diamond", 0) + spendTime * 50);
            PlayerPrefs.Save();
            diaText.text = PlayerPrefs.GetInt("Diamond", 0).ToString();
        }
        
        StartCoroutine(loadScene("MainMenuScene"));
    }

    private IEnumerator loadScene(string name)
    {
        if (clickSound != null) clickSound.Play();
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(name);
    }

    private void spendDiamond()
    {
        spendTime++;
        PlayerPrefs.SetInt("Diamond", PlayerPrefs.GetInt("Diamond", 0) - 50);
        PlayerPrefs.Save();
        diaText.text = PlayerPrefs.GetInt("Diamond", 0).ToString();
    }
}
