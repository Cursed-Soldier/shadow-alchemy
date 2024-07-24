using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DissolverController : MonoBehaviour
{
    public float timeToDissolve = 5.0f;

    //UI
    public GameObject outputIcon;
    public GameObject outputBubble;
    public GameObject gameManager;

    public bool dissolving = false;

    public bool outputWaiting = false;
    public Item output = null;

    public Item testItem;

    private void Start()
    {
        gameManager = GameObject.Find("RecipeManager");
        //Dissolve(testItem);
    }

    private void Update()
    {
        if (outputWaiting)
        {
            outputIcon.GetComponent<SpriteRenderer>().sprite = output.icon;
            outputWaiting = false;
        }
    }

    public void Dissolve(Item input_item)
    {
        if(input_item.type == ItemType.Flower || input_item.type == ItemType.Stone || input_item.type == ItemType.Metal)
        {
            //Look for item in dissolver recipes
            Recipe foundRecipe = gameManager.GetComponent<Recipes>().CheckDissolverRecipe(input_item);
            //If recipe found for item
            if (foundRecipe != null)
            {
                //Start machine 
                Debug.Log("DISSOLVING");
                if (!dissolving)
                {
                    StartCoroutine(DissolveCoroutine(foundRecipe));
                    
                }
                //After time has passed output item
            }
            else
            {
                Debug.Log("NO RECIPE");
            }

        }
    } 

    public IEnumerator DissolveCoroutine(Recipe recipe)
    {
        dissolving = true;
        yield return new WaitForSeconds(timeToDissolve);
        Debug.Log("DISSOLVED");
        output = recipe.output;
        outputWaiting = true;
        dissolving = false;
    }
}
