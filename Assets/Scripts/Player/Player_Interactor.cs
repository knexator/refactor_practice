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
        UpdateHighlightedInteractable(maybeInteractable);
        if(maybeInteractable is null)
            return;

        if(PlayerAskToInteractWith(maybeInteractable))
            maybeInteractable.Interact();
    }

    static bool PlayerAskToInteractWith(IInteractable maybeInteractable)
    {
        return maybeInteractable != null && Input.GetKeyDown(KeyCode.Space);
    }

    private GameObject lastHighlighted;

    [CanBeNull] private IInteractable interactableInFrontOfPlayer() {
        var facingDirection = new Vector3(playerMovement.faceDirection.x, playerMovement.faceDirection.y);
        Debug.DrawLine(transform.position, transform.position + (facingDirection * rayLenght), Color.red);
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, facingDirection, rayLenght, interactableLayer);
        if (hit.collider != null) {
            return hit.transform.gameObject.GetComponent<IInteractable>();
        }
        return null;
    }

    private void UpdateHighlightedInteractable(IInteractable stuff)
    {
        if (stuff != null)
            Highlight(stuff);
        else
            UnhighlightLastHighlighted();
    }

    void Highlight(IInteractable stuff)
    {
        if(stuff.alertPrompt.activeSelf)
            return;
        
        stuff.alertPrompt.SetActive(true);
        lastHighlighted = stuff.alertPrompt;
    }

    /// Sé que esta palabra no existe pero creo que aporta buena semántica.
    void UnhighlightLastHighlighted()
    {
        if(lastHighlighted != null && lastHighlighted.activeSelf)
        {
            lastHighlighted.SetActive(false);
        }
    }
}
