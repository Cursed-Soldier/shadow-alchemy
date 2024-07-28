using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;

    public ItemType type;
    public bool stackable;

    public float multiplier;
    public ItemValue value;

    public float default_price;

    public bool sellable;
    public float sell_price;

}



public enum ItemType 
{ 
    Flower,
    Spice,
    Essence,
    Stone,
    Metal,
    Bowl,
    Pot,
    MetalVial,
    Vial,
    Potion
}

