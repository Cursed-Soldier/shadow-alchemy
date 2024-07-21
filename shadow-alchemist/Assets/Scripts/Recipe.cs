using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Items/Recipe")]
public class Recipe : ScriptableObject
{
    public string recipe_name;
    public Item[] ingredients;
    public Item output;
}
