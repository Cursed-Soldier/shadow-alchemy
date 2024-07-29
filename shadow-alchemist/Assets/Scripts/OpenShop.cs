using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShop : MonoBehaviour
{
    public GameObject shopUI;

    public void ToggleShop()
    {
        shopUI.SetActive(!shopUI.activeInHierarchy);
    }
}
