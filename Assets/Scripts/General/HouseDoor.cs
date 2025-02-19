using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class HouseDoor : MonoBehaviour
{
    [Header("[References]")]
    [SerializeField] private AudioSource audiosource;
    [SerializeField] private CinemachineConfiner cameraConfiner;
    [SerializeField] private PolygonCollider2D exteriorConfiner;

    [Header("[Configuration]")]
    [SerializeField] private GameObject exteriorPosition;
    [SerializeField] private AudioClip doorSFX;
    [SerializeField] private List<DialogScriptable> dialogList;

    [Header("[Values]")]
    public bool talkedToDad;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && talkedToDad == false)
        {
            UI_DialogPanel.instance.onEndDialog += OnEndDialog;

            StaticData.gameState = GameState.Cutscene;
            UI_DialogPanel.instance.ShowDialog(dialogList);
        }
        else
        {
            StartCoroutine(Coroutine_TeleportToExterior());
        }
    }

    private void OnEndDialog()
    {
        UI_DialogPanel.instance.onEndDialog -= OnEndDialog;
        StaticData.gameState = GameState.Gameplay;
    }

    IEnumerator Coroutine_TeleportToExterior()
    {
        UI_FadeCanvas.instance.Play_FadeIn();
        audiosource.PlayOneShot(doorSFX);
        StaticData.gameState = GameState.Cutscene;
        yield return new WaitForSeconds(2f);

        Player_Interactor.instance.gameObject.transform.position = exteriorPosition.transform.position;
        cameraConfiner.m_BoundingShape2D = exteriorConfiner;
        UI_FadeCanvas.instance.Play_FadeOut();
        yield return new WaitForSeconds(1f);

        StaticData.gameState = GameState.Gameplay;
    }
}
