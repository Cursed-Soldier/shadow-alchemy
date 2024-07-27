using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class CrucibleController : MonoBehaviour
{
    public float timeToMix = 5.0f;

    //UI
    public GameObject outputIcon;
    public GameObject outputBubble;
    public GameObject gameManager;
    public GameObject inputBubble;
    public Image[] inputIcons;

    public bool mixing = false;

    public ItemValue outputValue;
    public bool outputWaiting = false;
    public Item output = null;

    public Item[] metals;

    public int index = 0;
    private Item[] itemsInCrucible;
    public Item metalLining = null;

    public Item testItem1;
    public Item testItem2;
    public Item testItem3;
    public Item testItem4;

    private void Start()
    {
        gameManager = GameObject.Find("RecipeManager");
        itemsInCrucible = new Item[3];
        metals = gameManager.GetComponent<Recipes>().metals;
        //Testing
        //itemsInCrucible[0] = testItem1;
        //itemsInCrucible[1] = testItem2;
        //itemsInCrucible[2] = testItem3;
        //metalLining = testItem4;

        //Mix(itemsInCrucible);

    }

    private void Update()
    {
        if (outputWaiting)
        {
            if (!outputBubble.activeInHierarchy)
            {
                outputIcon.GetComponent<SpriteRenderer>().sprite = output.icon;
                outputIcon.SetActive(true);
                outputBubble.SetActive(true);
            }
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

    public Item CollectItem()
    {
        Item ret = output;
        if (outputWaiting)
        {
            outputWaiting = false;
            output = null;
            outputIcon.GetComponent<SpriteRenderer>().sprite = null;
            outputIcon.SetActive(false);
            outputBubble.SetActive(false);
        }
        return ret;

    }

    public bool FreeCrucibleSpot()
    {
        if(index <= 2 && itemsInCrucible[2] == null)
        {
            return true;
        }
        return false;
    }

    public void AddToCrucible(Item item)
    {
        if (index >= 0 && itemsInCrucible[0] != null)
        {
            index++;
        }
        itemsInCrucible[index] = item;
        inputIcons[index].sprite = item.icon;
        
        if (!inputBubble.activeInHierarchy)
        {
            inputBubble.SetActive(true);
        }
        
    }

    public Item RemoveFromCrucible()
    {
        Item ret = null;
        if (itemsInCrucible[0] != null)
        {
            ret = itemsInCrucible[index];
            itemsInCrucible[index] = null;
            inputIcons[index].sprite = null;
            
            if(index > 0)
            {
                index--;
                
            }
            else
            {
                inputBubble.SetActive(false);
            }
        }    

       
        return ret;
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
        itemsInCrucible = new Item[3];
        index = 0;
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
