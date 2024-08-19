using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinResultHandler : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= HandleGameStateChanged;
    }

    private void CheckIfRewardIsBomb()
    {
        RewardDataSO currentRewardData = GameManager.Instace.GetCurrentRewardData();
        if (currentRewardData.isBomb)
        {
            GameManager.Instace.ChangeGameState(GameManager.GameState.BombExploded);
        }
        else
        {
            GameManager.Instace.ChangeGameState(GameManager.GameState.RewardsChecked);
        }
    }

    private void HandleGameStateChanged(GameManager.GameState newState)
    {
        switch (newState)
        {
            case GameManager.GameState.SpinEnded:
                CheckIfRewardIsBomb();
                break;
        }
    }
}
