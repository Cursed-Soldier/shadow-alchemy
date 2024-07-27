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

    public void Dissolve(Item input_item)
    {
        if (!outputWaiting || !dissolving)
        {
            Item[] ingredient = { input_item };
            if (input_item.type == ItemType.Flower || input_item.type == ItemType.Stone || input_item.type == ItemType.Metal)
            {
                //Look for item in dissolver recipes
                Recipe foundRecipe = gameManager.GetComponent<Recipes>().CheckBasicRecipe(ingredient, Stations.Dissolver);
                //If recipe found for item
                if (foundRecipe != null)
                {
                    //Start machine 
                    
                    if (!dissolving)
                    {
                        Debug.Log("DISSOLVING");
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
