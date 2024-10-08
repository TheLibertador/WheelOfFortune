using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instace;

    [SerializeField] private WheelSpinCalculator _wheelSpinCalculator;
    [SerializeField] private RewardManager _rewardManager;

    [SerializeField] SpecialZoneIndexDataSO zoneIndex;
  

    private int _spinIndex = 1;
    private int _currentRewardIndex; 
    private float _currentSpinRotation;

    [HideInInspector] public RewardDataSO rewardData;



    public enum GameState
    {
        MainMenuActive,
        SpinStarted,
        SpinEnded,
        RewardsChecked,
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
        return _currentSpinRotation;
    }

    public void SpinWheel()
    {
        _currentRewardIndex = _wheelSpinCalculator.GenerateRandomRewardIndex();
        _currentSpinRotation = _wheelSpinCalculator.GenerateRandomRotationAngle();
        ChangeGameState(GameState.SpinStarted);
    }

    private void IncreaseSpinCount()
    {
        _spinIndex++;
    }

    private void ResetSpinCount()
    {
        _spinIndex = 1;
    }

    public int GetSpinCount()
    {
        return _spinIndex;
    }


    public int GetCurrentReward()
    {
        return _currentRewardIndex;
    }

    public RewardDataSO GetCurrentRewardData()
    {
        return rewardData;
    }

    public  List<Reward> GetAllRewardData()
    {
        return _rewardManager.GetEarnedRewards();
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
                    ChangeZone(Zone.Bronze);
                    break;
                case GameState.GameWon:
                    ResetSpinCount();
                    ChangeZone(Zone.Bronze);
                    break;
            }
        }
    }

    private void CheckSpecialZone()
    {
        if (_spinIndex % zoneIndex._goldZoneIndex == 0 && _spinIndex >= 30)
        {
            ChangeZone(Zone.Gold);
        }
        else if (_spinIndex % zoneIndex._silverZoneIndex == 0)
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
