using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events_Level4 : Singleton<Events_Level4>
{
    [Header("[References]")]
    [SerializeField] private UI_EndingCredits creditsCanvas;
    [SerializeField] private MazeManager mazeManager;

    [Header("[Configuration]")]
    [SerializeField] private List<DialogScriptable> levelIntroDialog;
    [SerializeField] private List<DialogScriptable> santaCompanaDialog;

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

    public void Play_Ending()
    {
        StaticData.gameState = GameState.Cutscene;
        creditsCanvas.Play_Ending();
    }

    private void OnEndDialog()
    {
        UI_DialogPanel.instance.onEndDialog -= OnEndDialog;
        StaticData.gameState = GameState.Gameplay;
    }

}
