using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDialogInteractor : MonoBehaviour, IInteractable
{
    [Header("[Configuration]")]
    [SerializeField] private List<DialogScriptable> dialogList;

    [SerializeField] private Animator animator;

    [field: SerializeField] public GameObject alertPrompt { get; private set; }

    public void Interact()
    {
        animator.Play("Talk");
        UI_DialogPanel.instance.onEndDialog += OnEndDialog;

        StaticData.gameState = GameState.Cutscene;
        UI_DialogPanel.instance.ShowDialog(dialogList);
    }

    private void OnEndDialog()
    {
        animator.Play("Idle");
        UI_DialogPanel.instance.onEndDialog -= OnEndDialog;
        StaticData.gameState = GameState.Gameplay;
    }
}
