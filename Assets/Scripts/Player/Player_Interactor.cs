using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using JetBrains.Annotations;

public class Player_Interactor : Singleton<Player_Interactor>
{
    [Header("[References]")]
    [SerializeField] private Player.SimpleMovement playerMovement;

    [Header("[Configuration]")]
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private float rayLenght;

    private void Update()
    {
        if(StaticData.gameState == GameState.Gameplay)
            HandleInteractableInFrontOfPlayer();
    }

    void HandleInteractableInFrontOfPlayer()
    {
        var maybeInteractable = interactableInFrontOfPlayer();
        if(maybeInteractable is null)
            return;

        Highlight(maybeInteractable);

        if(PlayerAskToInteractWith(maybeInteractable))
        {
            maybeInteractable.Interact();
        }
    }

    static bool PlayerAskToInteractWith(IInteractable maybeInteractable)
    {
        return maybeInteractable != null && Input.GetKeyDown(KeyCode.Space);
    }

    private GameObject alertObj;

    [CanBeNull] private IInteractable interactableInFrontOfPlayer() {
        var facingDirection = new Vector3(playerMovement.faceDirection.x, playerMovement.faceDirection.y);
        Debug.DrawLine(transform.position, transform.position + (facingDirection * rayLenght), Color.red);
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, facingDirection, rayLenght, interactableLayer);
        if (hit.collider != null) {
            return hit.transform.gameObject.GetComponent<IInteractable>();
        }
        return null;
    }

    private void Highlight(IInteractable stuff)
    {
        if (stuff != null)
        {
            GameObject newAlertObj = stuff.alertPrompt;

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
