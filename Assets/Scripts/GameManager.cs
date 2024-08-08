using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instace;

    [SerializeField] private WheelSpinCalculator wheelSpinCalculator;
    [SerializeField] private WheelRotateHandler wheelRotateHandler;
    [SerializeField] private SlotManager slotManager;
    [SerializeField] private RewardManager rewardManager;

    private int spinCount = 0;


    public enum GameState
    {
        MainMenuActive,
        SpinStarted,
        SpinEnded,
        RewardsCollected,
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

    private void IncreaseSpinCount()
    {
        spinCount++;
    }

    public int GetSpinCount()
    {
        return spinCount;
    }
    public int GetCurrentReward()
    {
        return wheelSpinCalculator.GetCurrentRewardIndex();
    }

    public RewardDataSO GetRewardData(int rewardIndex)
    {
        return slotManager.GetRewardData(rewardIndex);
    }

    private  void ListRewards()
    {
        Dictionary<RewardDataSO, float> earnedRewards = rewardManager.GetEarnedRewards();
        foreach (var reward in earnedRewards)
        {
            Debug.Log($"Reward: {reward.Key.rewardName}, Amount: {reward.Value}");
        }
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
                rewardManager.SaveCurrentReward(GetRewardData(GetCurrentReward()));
                break;
            case GameState.RewardsCollected:
                ListRewards();
                break;
            case GameState.GameFailed:
                break;
        }
    }
}
