using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Export : MonoBehaviour
{
    public SpecialOrder specialOrder;
    public DetectionBar detection;
    public int detectionChange;
    
    public float SellPotion(Item item)
    {
        if (item.type == ItemType.Potion)
        {
            //Fulfil special order (only if its currently there)
            specialOrder.OrderFufilled(item);

            // Decrease detection
            detection.UpdateDetection(-detectionChange);
            Debug.Log(item.name);
            Debug.Log(item.sell_price);
            Debug.Log(item.multiplier);
        }

        return item.sell_price * item.multiplier;
    }
}
