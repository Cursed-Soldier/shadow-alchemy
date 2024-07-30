using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpecialOrder : MonoBehaviour
{

    public DetectionBar detectionBar;
    public int currentDetection;
    public int randomEventCooldown;
    public float goldAmount;
    public GameObject randomEventUI;
    public TMP_Text potionText;
    public Image potionImage;
    public TMP_Text goldText;
    public Item[] items = new Item[5];
    bool isRandomEvent;
    bool isOrderFufilled;
    bool isRandomEventOnCooldown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentDetection = detectionBar.currentDetection;
        if(80 < currentDetection && !isRandomEventOnCooldown )
        {
            //if no random event then set random event, once fufilled, start cooldown
            if(!isRandomEvent)
            {
                Order();
            }
            else if(isRandomEvent && isOrderFufilled)
            {
                RandomEventUI.SetActive(false);
                StartCoroutine(RandomEventOnCooldown());
            }
        }
    }

    public void Order()
    {
        Random rnd = new Random();
        int index = rnd.Next(items.Length);
        Item itemNeeded = item[index];
        potionImage.sprite = itemNeeded.icon;
        potionText.text = itemNeeded.itemName;
        goldAmount = itemNeeded.sell_price + 100;
        goldText.text = goldAmount.ToString();
        randomEventUI.SetActive(true);
        isRandomEvent = true;
    }
    public void OrderFufilled()
    {
        isOrderFufilled = true;
        detectionBar.UpdateDetection(-20);
    }

    public IEnumerator RandomEventOnCooldown()
    {
        isRandomEventOnCooldown = true;
        yield return new WaitForSeconds(randomEventCooldown);
        Debug.Log("random event cooldown");
        isRandomEventOnCooldown = false;
    }
}
