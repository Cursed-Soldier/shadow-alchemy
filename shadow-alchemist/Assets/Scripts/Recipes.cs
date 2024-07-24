using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Recipes : MonoBehaviour
{
    public Recipe[] dissolverRecipes;
    public Recipe[] separatorRecipes;
    public Recipe[] crucibleRecipes;

    public Item[] metals;

    public Recipe CheckBasicRecipe(Item[] ingredients, Stations station)
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
            for (int j = 0; j < ingredients.Length; j++)
            {
                
               if (stationRecipes[i].ingredients.All(item => ingredients.Contains(item)))
               {
                    return stationRecipes[i];
               }
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
