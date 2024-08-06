using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "ScriptableObjects/RewardDataScriptableObject", order = 1)]
public class RewardDataSO : ScriptableObject
{
    public bool isBomb;
    public int amount;
    public string rewardName;
    public Sprite image;
}
