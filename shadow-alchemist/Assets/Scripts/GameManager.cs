using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int gold;
    public TMP_Text goldText;

    void Start()
    {
        gold = 200;
        goldText.text = gold.ToString();
    }

}
