using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact();

    public GameObject alertPrompt { get; }

    // if there were multiple ways of showing interactability, this would be
    // public ShowInteractable(bool isInteractable)
    // But since there's a single one, and the logic is not contained in the Interactable object
    // (it spills into Player_Interactor, which has to keep state), I'm using the concrete approach.
}
