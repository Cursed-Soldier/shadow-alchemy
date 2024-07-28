using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int gold;
    public int goldRequired;
    public TMP_Text goldText;
    public GameObject WinScreenUI;

    void Start()
    {
        gold = 200;
        goldRequired = 300;
        goldText.text = gold.ToString();
    }

    void Update()
    {
        if(gold == goldRequired)
        {
            Time.timeScale = 0;
            WinScreenUI.SetActive(true);
        }
    }


}
