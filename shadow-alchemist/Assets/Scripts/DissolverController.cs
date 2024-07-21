using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DissolverController : MonoBehaviour
{
    public float time_to_dissolve = 5.0f;

    //UI
    public Image output_icon;
    public Image output_bubble;

    public Item Dissolve(Item input_item)
    {
        if(input_item.type == ItemType.Flower || input_item.type == ItemType.Stone || input_item.type == ItemType.Metal)
        {
            //Look for item in dissolver recipes
            //Start machine 
            //After time has passed output item
        }

        //Change this later
        return null;

    } 
}
