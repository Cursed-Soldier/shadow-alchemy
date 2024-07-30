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
    [SerializeField] bool isRandomEvent;
    [SerializeField] bool isOrderFufilled;
    [SerializeField] bool isRandomEventOnCooldown;

    public GameManager gm;

    int index;

    private void Awake()
    {
        isRandomEvent = false;
        isRandomEventOnCooldown = false;
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
            
        }
        if (isRandomEvent && isOrderFufilled)
        {
            randomEventUI.SetActive(false);
            StartCoroutine(RandomEventOnCooldown());
        }
        /*if (!isRandomEventOnCooldown)
        {
            //if no random event then set random event, once fufilled, start cooldown
            if (!isRandomEvent)
            { 

                Order();
            }
            else if (isRandomEvent && isOrderFufilled)
            {
                randomEventUI.SetActive(false);
                StartCoroutine(RandomEventOnCooldown());
            }
        }*/
    }

    public void Order()
    {
        Debug.Log("order");
        index = Random.Range(0, 5);
        Item itemNeeded = items[index];
        potionImage.sprite = itemNeeded.icon;
        potionText.text = itemNeeded.itemName;
        goldAmount = itemNeeded.sell_price + 100;
        goldText.text = goldAmount.ToString();
        randomEventUI.SetActive(true);
        isRandomEvent = true;
    }
    public void OrderFufilled(Item sellItem)
    {
        if (isRandomEvent)
        {
            if (sellItem == items[index])
            {
                isOrderFufilled = true;
                detectionBar.UpdateDetection(-20);
                gm.AddGold(goldAmount);
            }

        }
    }

    public IEnumerator RandomEventOnCooldown()
    {
        isRandomEventOnCooldown = true;
        isRandomEvent = false;
        yield return new WaitForSeconds(randomEventCooldown);
        Debug.Log("random event cooldown");
        isRandomEventOnCooldown = false;
        isOrderFufilled = false;
    }
}
