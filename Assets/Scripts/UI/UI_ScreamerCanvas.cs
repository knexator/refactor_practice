using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ScreamerCanvas : PersistentSingleton<UI_ScreamerCanvas>
{
    [Header("[References]")]
    [SerializeField] private AudioSource audiosourceScreamer;
    [SerializeField] private AudioSource audiosourceThunder;
    [SerializeField] private Animator screamerAnimator;
    [SerializeField] private GameObject screamerPanel;

    [Header("[Configuration]")]
    [SerializeField] private AudioClip screamerSFX;
    [SerializeField] private AudioClip thunderSFX;


    public void ShowScreamer()
    {
        StartCoroutine(Coroutine_ShowScreamer());

        IEnumerator Coroutine_ShowScreamer()
        {
            Core.GameStateController.instance.ChangeGameStateTo(Core.GameStateController.GameState.Pause);
            audiosourceScreamer.PlayOneShot(screamerSFX);
            audiosourceThunder.PlayOneShot(thunderSFX);
            screamerPanel.SetActive(true);
            screamerAnimator.Play("SCREAMER");

            yield return new WaitForSeconds(2f);
            UI_FadeCanvas.instance.Play_FadeIn();

            yield return new WaitForSeconds(1);
            screamerPanel.SetActive(false);
            UI_FadeCanvas.instance.Play_FadeOut();

            yield return new WaitForSeconds(1);
            Core.GameStateController.instance.ChangeGameStateTo(Core.GameStateController.GameState.Gameplay);
        }
    }

    public void ShowScreamerImage()
    {
        Core.GameStateController.instance.ChangeGameStateTo(Core.GameStateController.GameState.Pause);
        audiosourceScreamer.PlayOneShot(screamerSFX);
        audiosourceThunder.PlayOneShot(thunderSFX);
        screamerPanel.SetActive(true);
    }


}
