using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

public class Player_Interactor : Singleton<Player_Interactor>
{
    [Header("[References]")]
    [SerializeField] private Player.SimpleMovement playerMovement;

    [Header("[Configuration]")]
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private float rayLenght;

    private void Update()
    {
        if (StaticData.gameState != GameState.Gameplay) return;

        var maybeInteractable = interactableInFrontOfPlayer();

        // Show the player the object can be interacted with
        // TODO: this alert system should be part of the interactable interface
        updateAlertDisplay(maybeInteractable);

        if (maybeInteractable != null && Input.GetKeyDown(KeyCode.Space)) {
            maybeInteractable.GetComponent<IInteractable>().Interact();
        }
    }

    private GameObject alertObj;

    // Might return null
    private GameObject interactableInFrontOfPlayer() {
        var facingDirection = new Vector3(playerMovement.faceDirection.x, playerMovement.faceDirection.y);
        Debug.DrawLine(transform.position, transform.position + (facingDirection * rayLenght), Color.red);
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, facingDirection, rayLenght, interactableLayer);
        if (hit.collider != null) {
            return hit.transform.gameObject;
        }
        return null;
    }

    private void updateAlertDisplay(GameObject stuff)
    {
        if (stuff != null)
        {
            GameObject newAlertObj = stuff.transform.GetChild(0).gameObject;

            // Si el objeto aún no está activado, actívalo y actualiza la variable booleana
            if (!newAlertObj.activeSelf)
            {
                newAlertObj.SetActive(true);
                alertObj = newAlertObj; // Actualiza la referencia
            }
        }
        else
        {
            // Si el objeto está activado, desactívalo y actualiza la variable booleana
            if (alertObj != null && alertObj.activeSelf)
            {
                alertObj.SetActive(false);
            }
        }
    }
}
