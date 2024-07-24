using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeparatorController : MonoBehaviour
{
    public float timeToSeparate = 5.0f;

    //UI
    public GameObject outputIcon;
    public GameObject outputBubble;
    public GameObject gameManager;

    public bool separating = false;

    public bool outputWaiting = false;
    public Item output = null;
    public int outputAmount;

    public Item testItem;

    private void Start()
    {
        gameManager = GameObject.Find("RecipeManager");
        //Separate(testItem);
   

    }

    private void Update()
    {
        if (outputWaiting)
        {
            outputIcon.GetComponent<SpriteRenderer>().sprite = output.icon;
            
        }
    }

    public void Separate(Item input_item)
    {
        

        if (input_item.type == ItemType.Bowl | input_item.type == ItemType.Essence)
        {
            Item[] ingredient = { input_item };
            //Look for item in separator recipes
            Recipe foundRecipe = gameManager.GetComponent<Recipes>().CheckBasicRecipe(ingredient, Stations.Separator);
            //If recipe found for item
            if (foundRecipe != null)
            {
                //Start machine 
                Debug.Log("SEPARATING");
                if (!separating)
                {
                    StartCoroutine(SeparateCoroutine(foundRecipe));

                }
                //After time has passed output item
            }
            else
            {
                Debug.Log("NO RECIPE");
            }

        }
    }

    public IEnumerator SeparateCoroutine(Recipe recipe)
    {
        separating = true;
        yield return new WaitForSeconds(timeToSeparate);
        Debug.Log("SEPARATED");
        outputAmount =  Random.Range(2, 4);
        Debug.Log(outputAmount);
        output = recipe.output;
        outputWaiting = true;
        separating = false;
    }
}
