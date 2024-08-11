using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    [SerializeField] private SlotManager slotManager;
    private Dictionary<RewardDataSO, float> earnedRewards = new Dictionary<RewardDataSO, float>();

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= HandleGameStateChanged;
    }

    public void SaveCurrentReward(RewardDataSO rewardData)
    {
        if (rewardData.isBomb)
        {
            GameManager.Instace.ChangeGameState(GameManager.GameState.BombExploded);
            return;
        }
        else 
        {
            GameManager.Instace.ChangeGameState(GameManager.GameState.RewardEarned);
            if (earnedRewards.ContainsKey(rewardData))
            {
                earnedRewards[rewardData] += rewardData.amount;
            }
            else
            {
                earnedRewards.Add(rewardData, rewardData.amount);
            }
                
        }
    }

    public Dictionary<RewardDataSO, float> GetEarnedRewards()
    {
        return earnedRewards;
    }

    private void ResetEarnedRewards()
    {
        earnedRewards = new Dictionary<RewardDataSO, float>();
    }

    private void ListRewards()
    {

        if (earnedRewards != null && earnedRewards.Count != 0)
        {
            foreach (var reward in earnedRewards)
            {
                Debug.Log($"Reward: {reward.Key.rewardName}, Amount: {reward.Value}");
            }
        }

    }

    private void HandleGameStateChanged(GameManager.GameState newState)
    {
        switch (newState)
        {
            case GameManager.GameState.MainMenuActive:
                break;
            case GameManager.GameState.SpinStarted:
                break;
            case GameManager.GameState.SpinEnded:
                SaveCurrentReward(GameManager.Instace.GetCurrentRewardData());
                break;
            case GameManager.GameState.RewardsCollected:
                break;
            case GameManager.GameState.RewardEarned:
                ListRewards();
                break;
            case GameManager.GameState.GameFailed:
                ResetEarnedRewards();
                break;
            case GameManager.GameState.GameWon:
                ResetEarnedRewards();
                break;
        }
    }
}
