using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipes : MonoBehaviour
{
    public Recipe[] dissolverRecipes;
    public Recipe[] separatorRecipes;
    public Recipe[] crucibleRecipes;

    public Recipe CheckBasicRecipe(Item ingredient, Stations station)
    {
        Recipe[] stationRecipes;
        if (station == Stations.Dissolver)
        {
            stationRecipes = dissolverRecipes;
        }
        else if (station == Stations.Separator)
        {
            stationRecipes = separatorRecipes;
        }
        else
        {
            stationRecipes = crucibleRecipes;
        }
        
        for (int i = 0; i < stationRecipes.Length; i++)
        {
            Debug.Log(stationRecipes[i].name);
            if (ingredient.name.Equals(stationRecipes[i].ingredients[0].name)){
                return stationRecipes[i];
            }
        }
        
        return null;
    }

}

public enum Stations
{
    Dissolver,
    Separator,
    Crucible
}
