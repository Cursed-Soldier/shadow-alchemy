using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    public Slot[] itemSlots;

    public int stackLimit;
    public GameObject[] slots;
    public int index;

    public Item testItem;
    public Item testItem2;

    private void Start()
    {
        itemSlots = new Slot[9];
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i] = new Slot(i, null, 0);
        }
        ShowInventory();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            AddItem(testItem);
            ShowInventory();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            AddItem(testItem2);
            ShowInventory();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            UseItem(testItem, index);
            ShowInventory();
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            UseItem(testItem2, index);
            ShowInventory();
        }
    }

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