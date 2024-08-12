using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SlotManager : MonoBehaviour
{
    private Transform[] slots = new Transform[8];
    [SerializeField] private RewardDataSO[] rewardDatas = new RewardDataSO[8];

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= HandleGameStateChanged;
    }

    void Start()
    {
        InitializeSlots();

        if(slots != null)
        {
            MatchSlotData();
        }
       
    }
 
    private void MatchSlotData()
    {
        for(int index = 0; index < slots.Length; index++)
        {
            slots[index].GetComponent<Image>().sprite = rewardDatas[index].iconSprite;
            if (!rewardDatas[index].isBomb)
            {
                slots[index].GetComponentInChildren<TextMeshProUGUI>().text = rewardDatas[index].amount.ToString();
            }
            
        }
    }

    private void InitializeSlots()
    {
        var allChildren = transform.GetComponentsInChildren<Transform>();
        int slotIndex = 0;
        foreach (Transform child in allChildren)
        {
            if (child.GetComponent<Image>() && slotIndex < slots.Length)
            {
                slots[slotIndex] = child;
                slotIndex++;
            }
        }
    }

    public void SetRewardData(int rewardIndex)
    {
        GameManager.Instace.rewardData = rewardDatas[rewardIndex];
    }

    private void HandleGameStateChanged(GameManager.GameState newState)
    {
        switch (newState)
        {
            case GameManager.GameState.SpinStarted:
                SetRewardData(GameManager.Instace.GetCurrentReward());
                break;
        }
    }
}
