using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events_Level3 : MonoBehaviour
{
    [Header("[Configuration]")]
    [SerializeField] private List<DialogScriptable> levelIntroDialog;


    private void Start()
    {
        Play_LevelIntro();
    }

    private void Play_LevelIntro()
    {
        StaticData.gameState = GameState.Cutscene;
        StartCoroutine(Coroutine_LevelIntro());

        IEnumerator Coroutine_LevelIntro()
        {
            UI_FadeCanvas.instance.Play_FadeOut();
            yield return new WaitForSeconds(2);

            UI_DialogPanel.instance.onEndDialog += OnEndDialog;
            UI_DialogPanel.instance.ShowDialog(levelIntroDialog);
        }
    }

    private void OnEndDialog()
    {
        UI_DialogPanel.instance.onEndDialog -= OnEndDialog;
        StaticData.gameState = GameState.Gameplay;
    }
}
