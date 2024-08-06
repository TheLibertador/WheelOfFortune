using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSpinCalculator : MonoBehaviour
{
    private int rewardNum = 8;
    private float rewardAnglePerReward = 45f;

    private int currentRewardIndex;
    private float currentRotationAngle;


    private int minSpinCount = 3;
    private int maxSpinCount = 5;
    
    public void GenerateRandomRewardIndex()
    {
        currentRewardIndex = Random.Range(0, rewardNum);
    }

    public int GetCurrentRewardIndex()
    {
        return currentRewardIndex;
    }

    public float GenerateRandomRotationAngle()
    {
        int randomSpinCount = Random.Range(minSpinCount, maxSpinCount + 1);
        float rewardAngle = currentRewardIndex * rewardAnglePerReward;
        currentRotationAngle = (randomSpinCount * 360) + rewardAngle;
        return currentRotationAngle;
    }

}
