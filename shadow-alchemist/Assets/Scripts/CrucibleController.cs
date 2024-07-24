using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CrucibleController : MonoBehaviour
{
    public float timeToMix = 5.0f;

    //UI
    public GameObject outputIcon;
    public GameObject outputBubble;
    public GameObject gameManager;

    public bool mixing = false;

    public ItemValue outputValue;
    public bool outputWaiting = false;
    public Item output = null;

    public Item[] metals;

    private Item[] itemsInCrucible;
    public Item metalLining = null;

    public Item testItem1;
    public Item testItem2;
    public Item testItem3;
    public Item testItem4;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager");
        itemsInCrucible = new Item[3];
        metals = gameManager.GetComponent<Recipes>().metals;
        //Testing
        itemsInCrucible[0] = testItem1;
        itemsInCrucible[1] = testItem2;
        itemsInCrucible[2] = testItem3;
        metalLining = testItem4;

        Mix(itemsInCrucible);

    }

    private void Update()
    {
        if (outputWaiting)
        {
            outputIcon.GetComponent<SpriteRenderer>().sprite = output.icon;
            outputWaiting = false;
        }
    }

    private void CheckLining()
    {

        {
            if (metalLining.name.Equals(metals[0].name))//Copper
            {
                outputValue = ItemValue.Copper;

            }
            else if (metalLining.name.Equals(metals[1].name))//Silver
            {
                outputValue = ItemValue.Silver;

            }
            else if (metalLining.name.Equals(metals[2].name))//Gold
            {
                outputValue = ItemValue.Gold;

            }
            else
            {
                outputValue = ItemValue.None;
            }
        }
    }

    private bool CheckIngredients(Item[] ingredients)
    {
        if (ingredients.Length == 3)
        {
            int count = 0;
            for (int i = 0; i < ingredients.Length; i++)
            {
                if (ingredients[i].type == ItemType.Vial || ingredients[i].type == ItemType.Spice || ingredients[i].type == ItemType.Essence)
                {
                    Debug.Log(ingredients[i].name);
                    count++;
                }
            }
            if (count == 3)
            {
                return true;
            }
        }
        
        return false;
    }

    public void Mix(Item[] ingredients)
    {
        if (CheckIngredients(ingredients))
        {
            Debug.Log("Checked ingredients");
                //Based on ingredients, look for recipe
                Recipe foundRecipe = gameManager.GetComponent<Recipes>().CheckBasicRecipe(ingredients, Stations.Crucible);
                //If recipe found for item
                if (foundRecipe != null)
                {
                    //Start machine 
                    Debug.Log("MIXING");
                    if (!mixing)
                    {
                        StartCoroutine(MixingCoroutine(foundRecipe));

                    }
                    //After time has passed output item
                }
                else
                {
                    Debug.Log("NO RECIPE");
                }
            }
        }
        
    

    public IEnumerator MixingCoroutine(Recipe recipe)
    {
        mixing = true;
        yield return new WaitForSeconds(timeToMix);
        Debug.Log("MIXED");
        //Check metal value of new potion
        CheckLining();
        output = recipe.output;
        outputWaiting = true;
        mixing = false;
    }

    public enum ItemValue 
    {
        Copper,
        Silver,
        Gold,
        None
    }

}
