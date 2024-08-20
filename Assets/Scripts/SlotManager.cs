using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SlotManager : MonoBehaviour
{
    private List<Transform> slots = new List<Transform>();
    [SerializeField] private RewardDataSO[] rewardDatas = new RewardDataSO[8];

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnValidate()
    {
        InitializeSlots();
        if (slots.Count != rewardDatas.Length)
        {
            Array.Resize(ref rewardDatas, slots.Count);
        }

        for (int i = 0; i < rewardDatas.Length; i++)
        {
            if (rewardDatas[i] == null)
            {
                Debug.LogError($"Reward data at index {i} is not assigned in the SlotManager attached to {gameObject.name}.");
            }
        }
        if (slots != null)
        {
            MatchSlotData();
        }
        else
        {
            Debug.LogError("Slots are null cannot initialize the rewards");
        }
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= HandleGameStateChanged;
    }

    void Start()
    {
       
       
    }
 
    private void MatchSlotData()
    {
        for(int index = 0; index < slots.Count; index++)
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
        slots.Clear();
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Image>())
            {
                slots.Add(child);
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
