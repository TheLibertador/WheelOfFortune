using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardCollectUIHandler : MonoBehaviour
{
    private Transform panel;

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= HandleGameStateChanged;
    }

    private void HandleGameStateChanged(GameManager.GameState newState)
    {
        switch (newState)
        {
            case GameManager.GameState.GameWon:
                FillCollectScroll();
                break;
        }
    }


    private void FillCollectScroll()
    {
        

    }

}
