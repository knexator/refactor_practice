using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

public class Player_DropBread : Singleton<Player_DropBread>
{
    [Header("[References]")]
    [SerializeField] private GameObject breadPrefab;

    [Header("[Configuration]")]
    [SerializeField] private float timeColdown;

    [Header("[Values]")]
    [SerializeField] private float currentTime;

    private bool canDropBread;


    private void Start()
    {
        currentTime = 0f;
        canDropBread = true;
    }

    private void Update()
    {
        if (!canDropBread)
        {
            currentTime += Time.deltaTime;
            if (currentTime > timeColdown)
            {
                canDropBread = true;
                currentTime = 0;
            }
        }
        
        if(Input.GetKeyDown(KeyCode.E) && StaticData.gameState == GameState.Gameplay)
        {
            if (canDropBread)
            {
                DropBread();
                canDropBread = false;
            }
        }
    }

    private void DropBread()
    {
        Instantiate(breadPrefab, gameObject.transform.position, Quaternion.identity);
    }

}
