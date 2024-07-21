using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class TestDisplayItem : MonoBehaviour
{
    public TextMeshProUGUI item_name_ui;
    public Image item_img_ui;
    public Item equipped_item;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        item_name_ui.text = equipped_item.name;
        item_img_ui.sprite = equipped_item.icon;

    }
}
