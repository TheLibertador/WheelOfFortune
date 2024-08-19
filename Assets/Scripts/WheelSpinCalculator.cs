using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSpinCalculator : MonoBehaviour
{
    private int _rewardNum = 8;
    private float _rewardAnglePerReward = 45f;

    private int _currentRewardIndex;
    private float _currentRotationAngle;


    private int _minSpinCount = 3;
    private int _maxSpinCount = 5;
    
    public int GenerateRandomRewardIndex()
    {
        _currentRewardIndex = Random.Range(0, _rewardNum);
        return _currentRewardIndex;
    }

    public int GetCurrentRewardIndex()
    {
        return _currentRewardIndex;
    }

    public float GenerateRandomRotationAngle()
    {
        int randomSpinCount = Random.Range(_minSpinCount, _maxSpinCount + 1);
        float rewardAngle = _currentRewardIndex * _rewardAnglePerReward;
        _currentRotationAngle = (randomSpinCount * 360) + rewardAngle;
        return _currentRotationAngle;
    }

}
