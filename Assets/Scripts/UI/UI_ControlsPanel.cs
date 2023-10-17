using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ControlsPanel : Singleton<UI_ControlsPanel>
{
    public static bool dontDestroyOnLoad = false;
    [Header("[References]")]
    [SerializeField] private Animator controlsAnimator;

    public void Show_BasicControls()
    {
        controlsAnimator.Play("SHOW BASICS");
    }

    public void Show_AdvancedControls()
    {
        controlsAnimator.Play("SHOW ADVANCED");
    }

    public void HideAll()
    {
        controlsAnimator.Play("HIDE ALL");
    }

}
