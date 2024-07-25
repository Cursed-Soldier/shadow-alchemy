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

    public bool canInteract;
    public InteractType interactType;

    public InventoryManager invManager;
    public DissolverController dissolver;
    public SeparatorController separator;
    public CrucibleController crucible;


    public float moveSpeed = 10f;

    private void Awake()
    {
        input = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
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
            if (interactType == InteractType.Dissolver)
            {
                Item usedItem = invManager.UseItem(0);
                invManager.UpdateToolbarUI();
                dissolver.Dissolve(usedItem);
            }
            else if (interactType == InteractType.Separator)
            {
                Item usedItem = invManager.UseItem(0);
                invManager.UpdateToolbarUI();
                separator.Separate(usedItem);
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
    Table
}


