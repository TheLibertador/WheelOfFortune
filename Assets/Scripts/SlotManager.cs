using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    private Image[] slots;
    [SerializeField] private RewardDataSO[] rewardDatas = new RewardDataSO[8];
    
    void Start()
    {
        InitializeSlots();
        MatchSlotData();
    }
 
    private void MatchSlotData()
    {
        for(int index = 0; index < slots.Length; index++)
        {
            slots[index].sprite = rewardDatas[index].image;
        }
    }

    private void InitializeSlots()
    {
        slots = transform.GetComponentsInChildren<Image>();
    }

    public RewardDataSO GetRewardData(int rewardIndex)
    {
        return rewardDatas[rewardIndex];
    }
}
