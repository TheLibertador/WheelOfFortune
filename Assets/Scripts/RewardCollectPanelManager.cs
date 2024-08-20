using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RewardCollectPanelManager : MonoBehaviour
{
    [SerializeField] private GameObject _rewardCollectionPanel;
    [SerializeField] private Transform _rewardCollectCardsHolder;
    [SerializeField] private GameObject _rewardCollectCardPrefab;

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= HandleGameStateChanged;
    }

    private void EnableRewardCollectionPanel()
    {
        _rewardCollectionPanel.SetActive(true);
    }

    private void DisableRewardCollectionPanel()
    {
        _rewardCollectionPanel.SetActive(false);
    }

    private void FillRewardCollectScroll()
    {
        if (GameManager.Instace.GetAllRewardData() != null)
        {
            var rewardDataDictionary = GameManager.Instace.GetAllRewardData();

            foreach (var reward in rewardDataDictionary)
            {
                GameObject card = Instantiate(_rewardCollectCardPrefab, _rewardCollectCardsHolder);
                Transform rewardNameText = card.transform.Find("RewardNameText");
                rewardNameText.GetComponent<TextMeshProUGUI>().text = reward.rewardName;
                Transform rewardAmountText = card.transform.Find("RewardAmountText");
                rewardAmountText.GetComponent<TextMeshProUGUI>().text = reward.amount.ToString();
                Transform rewardIcon = card.transform.Find("RewardIcon");
                rewardIcon.GetComponent<Image>().sprite = reward.icon;
            }

        }

    }

    private void HandleGameStateChanged(GameManager.GameState newState)
    {
        switch (newState)
        {
            case GameManager.GameState.MainMenuActive:
                DisableRewardCollectionPanel();
                break;
            case GameManager.GameState.SpinEnded:
                break;
            case GameManager.GameState.RewardsCollected:
                FillRewardCollectScroll();
                EnableRewardCollectionPanel();
                break;
            case GameManager.GameState.BombExploded:
                break;
            case GameManager.GameState.GameWon:
                DisableRewardCollectionPanel();
                break;
        }
    }
}
