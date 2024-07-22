using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int gold;
    public TMP_Text goldText;
    // Start is called before the first frame update
    void Start()
    {
        gold = 200;
        goldText.text = gold.ToString();
    }
}
