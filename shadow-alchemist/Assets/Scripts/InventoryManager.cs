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
    public Item testItem3;
    public Item testItem4;
    public Item testItem5;

    private void Start()
    {
        //playerMove = gameObject.GetComponent<PlayerMovement>();
        itemSlots = new Slot[9];
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i] = new Slot(i, null, 0);
        }
        UpdateToolbarScroll();

    }

    private void Update()
    {
        if(index != playerMove.scrollIndex)
        {
            //Update UI
            UpdateToolbarScroll();
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


    public Item AddItem(Item item)
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
                    return itemSlots[i].slotItem;
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
                return itemSlots[j].slotItem;
            }
        }

        return null;
    }

    public bool CheckItem(Stations station)
    {
        Item input_item = itemSlots[index].slotItem;
        if (input_item != null)
        {
            switch (station)
            {
                case Stations.Dissolver:
                    if (input_item.type == ItemType.Flower || input_item.type == ItemType.Stone || input_item.type == ItemType.Metal)
                    {
                        return true;
                    }
                    break;
                case Stations.Separator:
                    if (input_item.type == ItemType.Bowl || input_item.type == ItemType.Pot)
                    {
                        return true;
                    }
                    break;
                case Stations.Crucible:
                    if (input_item.type == ItemType.Vial || input_item.type == ItemType.Spice || input_item.type == ItemType.Essence)
                    {
                        return true;
                    }
                    else if (input_item.type == ItemType.MetalVial)
                    {
                        Debug.Log("metal vial");
                        return true;
                    }
                    break;
                default:
                    break;
            }
        }
        return false;
    }

    public bool EmptySlot()
    {
        if (itemSlots[index].slotAmount == 0)
        {
            Debug.Log(itemSlots[index].slotAmount);
            return true;
        }
        return false;
    }

    public bool FreeSlot()
    {
        for (int j = 0; j < itemSlots.Length; j++)
        {
            //If empty slot
            if (itemSlots[j].slotAmount == 0)
            {
                return true;
            }
        }
        return false;
    }

    public Item UseItem()
    {
        if (itemSlots[index].slotAmount != 0)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                //If item in index specified and exists
                if (itemSlots[i].slotItem != null && i == index)
                {
                    Debug.Log(itemSlots[i].slotItem.name);
                    itemSlots[i].slotAmount--;
                    Item output = itemSlots[i].slotItem;
                    //If items run out, reset slot
                    if (itemSlots[i].slotAmount < 1)
                    {
                        itemSlots[i].slotIndex = i;
                        itemSlots[i].slotItem = null;
                        itemSlots[i].slotAmount = 0;
                    }
                    return output;


                }
            }
        }
        else
        {
            Debug.Log("No item selected to use");
        }
        return null;
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