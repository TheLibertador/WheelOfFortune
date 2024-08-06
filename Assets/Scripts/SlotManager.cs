using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    [SerializeField] private List<RewardDataSO> rewardDatas;

    private GameObject[] slots;
   
    void Start()
    {
        InitializeSlots();
    }
 
    private void MatchSlotData()
    {
        
    }

    private void InitializeSlots()
    {
        slots = transform.GetComponentsInChildren<GameObject>();
    }
}
