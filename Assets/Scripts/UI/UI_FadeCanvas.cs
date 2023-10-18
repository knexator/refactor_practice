using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FadeCanvas : Singleton<UI_FadeCanvas>
{
    [Header("[References]")]
    [SerializeField] private Animator fadeAnimator;

    private void Start()
    {
        Play_FadeOut();
    }


    public void Play_FadeIn()
    {
        fadeAnimator.Play("FADE IN");
    }

    public void Play_FadeOut()
    {
        fadeAnimator.Play("FADE OUT");
    }

}
