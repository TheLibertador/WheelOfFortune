using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    private Dictionary<RewardDataSO, float> earnedRewards = new Dictionary<RewardDataSO, float>();
    public void SaveCurrentReward(RewardDataSO rewardData)
    {
        if (rewardData.isBomb)
        {
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
}
