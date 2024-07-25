using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;


public class InventoryManager : MonoBehaviour
{
    public PlayerMovement playerMove;
    public Slot[] itemSlots;

    public int stackLimit;
    public GameObject[] slots;

    public int index;
    public Color slotColor;
    public Color highlightColor;

    public Item testItem;
    public Item testItem2;


    private void Start()
    {
        //playerMove = gameObject.GetComponent<PlayerMovement>();
        itemSlots = new Slot[9];
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i] = new Slot(i, null, 0);
        }

    }

    private void Update()
    {
        if(index != playerMove.scrollIndex)
        {
            //Update UI
            UpdateToolbarScroll();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            AddItem(testItem);
            UpdateToolbarUI();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            AddItem(testItem2);
            UpdateToolbarUI();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            UseItem(testItem, index);
            UpdateToolbarUI();
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            UseItem(testItem2, index);
            UpdateToolbarUI();
        }
    }

    /// <summary>
    /// Testing script used to see inventory items in console
    /// </summary>
    public void ShowInventory()
    {
        foreach (Slot slot in itemSlots)
        {
            if(slot.slotItem == null)
            {
                Debug.Log(slot.slotIndex.ToString() + ":" + "empty" + ":" + slot.slotAmount.ToString());

            }
            else
            {
                Debug.Log(slot.slotIndex.ToString() + ":" + slot.slotItem.name + ":" + slot.slotAmount.ToString());
            }
        }
    }


    public bool AddItem(Item item)
    {
        //Check if item already exists
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if(itemSlots[i].slotItem == item)
            {
                //If item amount in slot is less than the limit
                if(itemSlots[i].slotAmount < stackLimit)
                {
                    itemSlots[i].slotAmount++;
                    return true;
                }
            }
        }
        //Check for empty slots
        for (int j = 0; j < itemSlots.Length; j++)
        {
            //If empty slot, add item
            if(itemSlots[j].slotAmount == 0)
            {
                itemSlots[j].slotItem = item;
                itemSlots[j].slotAmount = 1;
                return true;
            }
        }

        return false;
    }

    public bool UseItem(Item item, int index)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            //If item in index specified and exists
            if (itemSlots[i].slotItem == item && i == index)
            {
                itemSlots[i].slotAmount--;
                //If items run out, reset slot
                if(itemSlots[i].slotAmount < 1)
                {
                    itemSlots[i].slotIndex = i;
                    itemSlots[i].slotItem = null;
                    itemSlots[i].slotAmount = 0;
                }
                return true;


            }
        }
        return false;
    }

    public void UpdateToolbarUI()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {

            if (itemSlots[i].slotItem == null)
            {
                slots[i].transform.Find("Icon").GetComponent<Image>().sprite = null;
                slots[i].transform.Find("AmountText").GetComponent<TextMeshProUGUI>().text = "";
                //Set opacity to 0
                Color opacity = slots[i].transform.Find("Icon").GetComponent<Image>().color;
                opacity.a = 0f;
                slots[i].transform.Find("Icon").GetComponent<Image>().color = opacity;

            }
            else
            {
                //Set opacity to 1
                Color opacity = slots[i].transform.Find("Icon").GetComponent<Image>().color;
                opacity.a = 1f;
                slots[i].transform.Find("Icon").GetComponent<Image>().color = opacity;

                // Check if there is more than 1 item in the slot
                if (itemSlots[i].slotAmount > 1)
                {
                    slots[i].transform.Find("AmountText").GetComponent<TextMeshProUGUI>().text = itemSlots[i].slotAmount.ToString();
                }
                else
                {
                    slots[i].transform.Find("AmountText").GetComponent<TextMeshProUGUI>().text = "";
                }

                slots[i].transform.Find("Icon").GetComponent<Image>().sprite = itemSlots[i].slotItem.icon;

            }
        }
    }

    public void UpdateToolbarScroll()
    {
        Color prevIndexColor = slots[index].gameObject.GetComponent<Image>().color;
        prevIndexColor = slotColor;
        slots[index].gameObject.GetComponent<Image>().color = prevIndexColor;

        index = playerMove.scrollIndex;

        Color newIndexColor = slots[index].gameObject.GetComponent<Image>().color;
        newIndexColor = highlightColor;
        slots[index].gameObject.GetComponent<Image>().color = newIndexColor;
    }

}

public class Slot
{
    public int slotIndex;
    public Item slotItem;
    public int slotAmount;

    public Slot(int index, Item item, int amount)
    {
        slotIndex = index;
        slotItem = item;
        slotAmount = amount;

    }
}