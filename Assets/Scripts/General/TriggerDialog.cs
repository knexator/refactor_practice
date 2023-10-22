using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialog : MonoBehaviour
{
    [Header("[Configuration]")]
    [SerializeField] private List<DialogScriptable> dialogList;


    public void ShowDialog()
    {
        UI_DialogPanel.instance.onEndDialog += OnEndDialog;

        StaticData.gameState = GameState.Cutscene;
        UI_DialogPanel.instance.ShowDialog(dialogList);
    }

    private void OnEndDialog()
    {
        UI_DialogPanel.instance.onEndDialog -= OnEndDialog;
        StaticData.gameState = GameState.Gameplay;
    }
}
