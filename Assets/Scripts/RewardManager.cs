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
            GameManager.Instace.ChangeGameState(GameManager.GameState.GameFailed);
            return;
        }
        else if (earnedRewards.ContainsKey(rewardData))
        {
            earnedRewards[rewardData] += rewardData.amount;
        }
        else
        {
            earnedRewards.Add(rewardData, rewardData.amount);
            Debug.Log("Reward saved");
        }

        
    }

    public Dictionary<RewardDataSO, float> GetEarnedRewards()
    {
        return earnedRewards;
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
            case GameManager.GameState.RewardCollected:
            case GameManager.GameState.AllRewardsCollected:
                ListRewards();
                break;
            case GameManager.GameState.GameFailed:
                break;
        }
    }
}
