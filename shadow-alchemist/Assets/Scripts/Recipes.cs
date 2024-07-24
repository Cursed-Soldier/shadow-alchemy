using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipes : MonoBehaviour
{
    public Recipe[] dissolver_recipes;
    public Recipe[] separator_recipes;
    public Recipe[] crucible_recipes;

    public Recipe CheckDissolverRecipe(Item ingredient)
    {

        //ONLY WORKS FOR DISSOLVER
        for (int i = 0; i < dissolver_recipes.Length; i++)
        {
            Debug.Log(dissolver_recipes[i].name);
            if (ingredient.name.Equals(dissolver_recipes[i].ingredients[0].name)){
                return dissolver_recipes[i];
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
