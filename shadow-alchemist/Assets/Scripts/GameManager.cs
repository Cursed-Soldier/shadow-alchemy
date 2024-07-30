using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float gold;
    public float goldRequired;
    public TMP_Text goldText;
    public GameObject WinScreenUI;

    void Start()
    {
        goldText.text = gold.ToString();
    }

    void Update()
    {
        if(gold >= goldRequired)
        {
            Time.timeScale = 0;
            WinScreenUI.SetActive(true);
        }
    }

    public void AddGold(float added)
    {
        gold += added;
        goldText.text = gold.ToString();
    }


}
