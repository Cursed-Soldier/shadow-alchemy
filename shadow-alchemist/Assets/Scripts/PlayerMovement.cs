using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls input = null;
    private Vector2 moveVector = Vector2.zero;
    private Rigidbody2D rb = null;

    public int maxIndex = 8;
    public int scrollIndex;

    //Interact vars
    public List<GameObject> interactableObjects = new List<GameObject>();
    public bool canInteract;
    public GameObject currentInteract;
    public InteractType interactType;

    public InventoryManager invManager;
    public DissolverController dissolver;
    public SeparatorController separator;
    public CrucibleController crucible;
    public Export exportDepot;


    public float moveSpeed = 10f;

    private void Awake()
    {
        input = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        Time.timeScale = 1;
    }

    private void Start()
    {
        currentInteract = null;
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.Lerp(rb.velocity, moveVector * moveSpeed, 0.5f);
    }

    //Enable the input system for moving player
    private void OnEnable()
    {
        //Enable moves

        input.Player.Enable();
        
        //Enable functions
        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCancelled;
        input.Player.Scroll.performed += OnScroll;
        input.Player.Interact.performed += OnInteract;

    }

    //Disable input system
    private void OnDisable()
    {

        //Disable functions
        input.Player.Disable();
        input.Player.Movement.performed -= OnMovementPerformed;
        input.Player.Movement.canceled -= OnMovementCancelled;
        input.Player.Scroll.performed -= OnScroll;
        input.Player.Interact.performed -= OnInteract;
    }



    //Move player action
    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        //Grab vector 2 from input
        moveVector = value.ReadValue<Vector2>();
        
    }

    //Cancel movement (when object not active)
    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;
    }

    private void OnScroll(InputAction.CallbackContext value)
    {
        Vector2 scrollVector = value.ReadValue<Vector2>();
        float scroll = scrollVector.y;

        if (scroll > 0f)
        {
            //Scrolling upwards
            scrollIndex++;
            if (scrollIndex >= maxIndex)
            {
                scrollIndex = 0;
            }
        }
        else if (scroll < 0f)
        {
            // Scroll downwards
            scrollIndex--;
            if (scrollIndex < 0)
            {
                scrollIndex = maxIndex - 1;
            }

        }
    }

    private void OnInteract(InputAction.CallbackContext value)
    {
        if (canInteract)
        {
            if (interactableObjects.Count > 0)
            {
                InteractType objType = interactableObjects[0].GetComponent<Interact>().type;             


                switch (objType)
                {
                    case InteractType.None:
                        break;
                    case InteractType.Dissolver:
                        if (invManager.EmptySlot())
                        {
                            //Collect item if possible
                            Item collectedItem = dissolver.CollectItem();
                            if(collectedItem == null)
                            {
                                Debug.Log("No item to collect");
                            }
                            else
                            {
                                Item added = invManager.AddItem(collectedItem);
                                if(added == null)
                                {
                                    Debug.Log("not added");
                                }
                                else
                                {
                                    Debug.Log("added");
                                    invManager.UpdateToolbarUI();

                                }

                            }
                        }
                        else
                        {
                            //If item of correct type or not null
                            if (invManager.CheckItem(Stations.Dissolver))
                            {
                                //If dissolver currently not working or has an output
                                if (!dissolver.dissolving && !dissolver.outputWaiting)
                                {
                                    Item usedItem = invManager.UseItem();
                                    if (usedItem != null)
                                    {
                                        invManager.UpdateToolbarUI();
                                        dissolver.Dissolve(usedItem);
                                    }
                                }
                            }
                            else
                            {
                                Debug.Log("Cannot dissolve item");
                            }
                        }
                        break;
                    case InteractType.Separator:
                        if (invManager.EmptySlot())
                        {
                            //Collect item if possible
                            Item collectedItem = separator.CollectItem();
                            if (collectedItem == null)
                            {
                                Debug.Log("No item to collect");
                            }
                            else
                            {
                                Item added = invManager.AddItem(collectedItem);
                                if (added == null)
                                {
                                    Debug.Log("not added");
                                }
                                else
                                {
                                    Debug.Log("added");
                                    invManager.UpdateToolbarUI();

                                }

                            }
                        }
                        else
                        {
                            //If item of correct type or not null
                            if (invManager.CheckItem(Stations.Separator))
                            {
                                //If separator currently not working or has an output
                                if (!separator.separating && !separator.outputWaiting)
                                {
                                    Debug.Log("separating");
                                    Item usedItem = invManager.UseItem();
                                    invManager.UpdateToolbarUI();
                                    separator.Separate(usedItem);
                                }

                            }
                            else
                            {
                                Debug.Log("Cannot separate item");
                            }
                        }
                        break;
                    case InteractType.Crucible:
                        if (invManager.EmptySlot())
                        {
                            //Collect item if possible
                            Item collectedItem = crucible.CollectItem();
                            if (collectedItem == null)
                            {
                                Debug.Log("No item to collect");
                                //Check if there is a free slot
                                if (invManager.FreeSlot())
                                {
                                    //Check if we can remove item from crucible
                                    Item crucibleItem = crucible.RemoveFromCrucible();
                                    if (crucibleItem == null)
                                    {
                                        Debug.Log("Not possible to remove item");
                                    }
                                    else
                                    {
                                        Debug.Log("Item returned");
                                        Item added = invManager.AddItem(crucibleItem);
                                        if (added == null)
                                        {
                                            Debug.Log("not added");
                                        }
                                        else
                                        {
                                            Debug.Log("added");
                                            invManager.UpdateToolbarUI();

                                        }
                                    }

                                }
                            }
                            else
                            {
                                Item added = invManager.AddItem(collectedItem);
                                if (added == null)
                                {
                                    Debug.Log("not added");
                                }
                                else
                                {
                                    Debug.Log("added");
                                    invManager.UpdateToolbarUI();

                                }

                            }
                        }
                        else
                        {

                            //If separator currently not working or has an output
                            if (!crucible.mixing && !crucible.outputWaiting)
                            {
                                Debug.Log("going in");
                                if (invManager.CheckItem(Stations.Crucible))
                                {
                                    Item usedItem = invManager.UseItem();
                                    Debug.Log(usedItem.type);
                                    //Check if item is a metal
                                    if (usedItem.type == ItemType.MetalVial)
                                    {
                                        Debug.Log("Metal");
                                        invManager.UpdateToolbarUI();
                                        crucible.AddMetal(usedItem);

                                    }
                                    else if (crucible.FreeCrucibleSpot())
                                    {
                                        Debug.Log("normal add");
                                        invManager.UpdateToolbarUI();
                                        crucible.AddToCrucible(usedItem);
                                    }
                                }
                                   
                                    
                            }

                            
                            else
                            {
                                Debug.Log("Cannot mix item");
                            }
                        }
                        break;
                    case InteractType.Shop:
                        break;
                    case InteractType.Export:
                        if (invManager.EmptySlot())
                        {
                            Debug.Log("Cannot sell");
                        }
                        else
                        {
                            Item usedItem = invManager.UseItem();
                            Debug.Log(usedItem.name);
                            invManager.UpdateToolbarUI();
                            float goldEarned = exportDepot.SellPotion(usedItem);
                            Debug.Log(goldEarned);
                            // gold += usedItem.sellPrice
                        }
                        
                        break;
                    case InteractType.Pickup:
                        break;
                    case InteractType.Table:
                        break;
                    default:
                        break;

                }

                currentInteract = interactableObjects[0];

            }
            
            
        }
    }

}

public enum InteractType 
{
    None,
    Dissolver,
    Separator,
    Crucible,
    Shop,
    Export,
    Pickup,
    Table
}


