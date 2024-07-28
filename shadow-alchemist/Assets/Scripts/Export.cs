using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Export : MonoBehaviour
{
    //public DetectionBar detection;
    public int detectionChange;
    
    public float SellPotion(Item item)
    {
        // Increase detection
        //detection.UpdateDetection(detectionChange);
        Debug.Log(item.name);
        Debug.Log(item.sell_price);
        Debug.Log(item.multiplier);
        return item.sell_price * item.multiplier;
    }
}
