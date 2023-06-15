using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class infoUI : MonoBehaviour
{
    [SerializeField]
    private LayerMask heroMask;

    private Camera mainCamera;

    public GameObject ui;
    public GameObject uiPanel;
    public TMP_Text dmgValueText;

    void Start()
    {
        mainCamera = Camera.main;
        ui.SetActive(false);
        uiPanel.SetActive(false);
        //UIbg.SetActive(false);
    }

    public void displayUI()
    {
        //UIbg.SetActive(true);
    }

}
