using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instace;

    [SerializeField] private WheelSpinCalculator wheelSpinCalculator;
    [SerializeField] private RewardManager rewardManager;
  

    private int spinCount = 1;
    private int currentRewardIndex; 
    private float currentSpinRotation;

    public RewardDataSO rewardData;


    public enum GameState
    {
        MainMenuActive,
        SpinStarted,
        SpinEnded,
        RewardsCollected,
        RewardEarned,
        BombExploded,
        GameFailed,
        GameWon,
    }
    public GameState currentState = GameState.MainMenuActive;

    public delegate void GameStateChanged(GameState newState);

    public static event GameStateChanged OnGameStateChanged;

    public enum Zone
    {
        Bronze,
        Gold,
        Silver
    }
    public Zone currentZone = Zone.Bronze;

    public delegate void ZoneChanged(Zone newZone);

    public static event ZoneChanged OnZoneChanged;

    

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

    public float GetWheelRotation()
    {
        return currentSpinRotation;
    }

    public void SpinWheel()
    {
        currentRewardIndex = wheelSpinCalculator.GenerateRandomRewardIndex();
        currentSpinRotation = wheelSpinCalculator.GenerateRandomRotationAngle();
        ChangeGameState(GameState.SpinStarted);
        Debug.Log("Current reward index= " + currentRewardIndex);
    }

    private void IncreaseSpinCount()
    {
        spinCount++;
    }

    private void ResetSpinCount()
    {
        spinCount = 1;
    }

    public int GetSpinCount()
    {
        return spinCount;
    }


    public int GetCurrentReward()
    {
        return currentRewardIndex;
    }

    public RewardDataSO GetCurrentRewardData()
    {
        return rewardData;
    }

    public  Dictionary<RewardDataSO,float> GetAllRewardData()
    {
        return rewardManager.GetEarnedRewards();
    }

    
    public void ChangeGameState(GameState state)
    {
        if (currentState != state)
        {
            currentState = state;
            OnGameStateChanged?.Invoke(state); 

            switch (state)
            {
                case GameState.MainMenuActive:
                    break;
                case GameState.SpinStarted:
                    IncreaseSpinCount();
                    break;
                case GameState.SpinEnded:
                    break;
                case GameState.RewardEarned:
                    CheckSpecialZone();
                    break;
                case GameState.GameFailed:
                    ResetSpinCount();
                    break;
                case GameState.GameWon:
                    ResetSpinCount();
                    break;
            }
        }
    }

    private void CheckSpecialZone()
    {
        if (spinCount % 30 == 0 && spinCount >= 30)
        {
            ChangeZone(Zone.Gold);
        }
        else if (spinCount % 5 == 0)
        {
            ChangeZone(Zone.Silver);
        }
        else
        {
            ChangeZone(Zone.Bronze);
        }
    }

    private void ChangeZone(Zone zone)
    {
        if (currentZone != zone)
        {
            currentZone = zone;
            OnZoneChanged?.Invoke(zone);
        }

    }
}
