using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public PlayerMovement playerMove;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.CompareTag("Interact"))
        {
            playerMove.interactableObjects.Add(collision.gameObject);
            playerMove.canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (playerMove.interactableObjects.Contains(collision.gameObject))
        {
            playerMove.interactableObjects.Remove(collision.gameObject);
            if (playerMove.interactableObjects.Count == 0)
            {
                playerMove.canInteract = false;
            }
        }
    }
}
