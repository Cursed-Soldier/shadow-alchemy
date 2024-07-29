using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopButton : MonoBehaviour
{
    public Button shopBtn;
    public Item output;
    public InventoryManager invMan;
    public GameManager gm;

    public TextMeshProUGUI title;
    public TextMeshProUGUI goldText;
    public Image icon;

    // Start is called before the first frame update
    void Start()
    {

        shopBtn.onClick.AddListener(Buy);
        title.text = output.name;
        icon.sprite = output.icon;
        goldText.text = output.default_price.ToString();
        gm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
    }


    public void Buy()
    {
        Debug.Log("buying");
        if (gm.gold >= output.default_price)
        {
            Item sold = invMan.AddItem(output);
            if (sold != null)
            {
                gm.AddGold(-output.default_price);
                invMan.UpdateToolbarUI();
                Debug.Log("bought item : " + output.name);
            }
            else
            {
                Debug.Log("not bought");
            }
        }

    }
}
