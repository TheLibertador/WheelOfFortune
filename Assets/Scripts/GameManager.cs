using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instace;

    [SerializeField] private WheelSpinCalculator wheelSpinCalculator;
    [SerializeField] private WheelRotateHandler wheelRotateHandler;


    public enum GameState
    {
        MainMenuActive,
        SpinStarted,
        SpinEnded,
        GameFailed
    }

    public GameState currentState = GameState.MainMenuActive;

    private void Awake()
    {
        if(Instace != this && Instace == null)
        { 
            Instace = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void SpinWheel()
    {
        ChangeGameState(GameState.SpinStarted);
        wheelSpinCalculator.GenerateRandomRewardIndex();
        wheelRotateHandler.RotateWheel(wheelSpinCalculator.GenerateRandomRotationAngle());
    }

    public int GetCurrentReward()
    {
        return wheelSpinCalculator.GetCurrentRewardIndex();
    }

    public void ChangeGameState(GameState state)
    {
        currentState = state;
        switch (state)
        {
            case GameState.MainMenuActive:
                break;
            case GameState.SpinStarted:
                break;
            case GameState.SpinEnded:
                break;
            case GameState.GameFailed:
                break;
        }
    }
}
