using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward
{
    public float amount;
    public string rewardName;
    public Sprite icon;

    public Reward(float amount, string rewardName, Sprite icon)
    {
        this.amount = amount;
        this.rewardName = rewardName;
        this.icon = icon;
    }
}
public class RewardManager : MonoBehaviour
{
    private List<Reward> rewards = new List<Reward>();

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
        int rewardIndex = 0;
        bool rewardFound = false;
        foreach(var reward in rewards)
        {
            if(reward.rewardName == rewardData.rewardName)
            {
                rewardFound = true;
                break;
            }
            rewardIndex++;
        }
        if (rewardFound)
        {
            rewards[rewardIndex].amount += rewardData.amount;
        }
        else
        {
           var rewardStruct = new Reward(rewardData.amount, rewardData.rewardName, rewardData.iconSprite);
           rewards.Add(rewardStruct);
        }
        GameManager.Instace.ChangeGameState(GameManager.GameState.RewardEarned);
    }

    public List<Reward> GetEarnedRewards()
    {
        return rewards;
    }

    private void ResetEarnedRewards()
    {
        rewards = new List<Reward>();
    }


    private void HandleGameStateChanged(GameManager.GameState newState)
    {
        switch (newState)
        {
            case GameManager.GameState.RewardsChecked:
                SaveCurrentReward(GameManager.Instace.GetCurrentRewardData());
                break;
            case GameManager.GameState.GameFailed:
                ResetEarnedRewards();
                break;
            case GameManager.GameState.GameWon:
                Debug.Log($"Game won list size is {rewards.Count}");
                ResetEarnedRewards();
                Debug.Log($"Game resetted list size is {rewards.Count}");
                break;
        }
    }
}
