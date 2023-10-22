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
    [SerializeField] private bool interacting;

    private void Update()
    {
        if(interacting == false)
        {
            if (Input.GetKeyDown(KeyCode.Space) && StaticData.gameState == GameState.Gameplay)
            {
                Interact();
            }
        }
        
        ActiveStuffs();
        
    }

    private GameObject alertObj;

    // Might return null
    private GameObject interactableInFrontOfPlayer() {
        Debug.DrawLine(transform.position, transform.position + (facingDirection * rayLenght), Color.red);
        var facingDirection = new Vector3(playerMovement.faceDirection.x, playerMovement.faceDirection.y);
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, facingDirection, rayLenght, interactableLayer);
        if (hit.collider != null) {
            return hit.transform.gameObject;
        }
        return null;
    }

    private void Interact()
    {
        var stuff = interactableInFrontOfPlayer();
        if(stuff != null)
        {
            interacting = true;
            stuff.GetComponent<IInteractable>().Interact();
        }

    }
    
    private void ActiveStuffs()
    {
        var stuff = interactableInFrontOfPlayer();

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

    public void EnableInteracting()
    {
        StartCoroutine(Coroutine_EnableInteracting());

        IEnumerator Coroutine_EnableInteracting()
        {
            yield return new WaitForSeconds(0.25f);
            interacting = false;
        }
    }
}
