using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardEarnPanelManager : MonoBehaviour
{
    [SerializeField] private GameObject _rewardEarnPanel;

    [SerializeField] private TextMeshProUGUI _rewardCardItemNameText;
    [SerializeField] private TextMeshProUGUI _rewardCardItemAmountText;
    [SerializeField] private Image _rewardCardItemIcon;

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= HandleGameStateChanged;
    }


    private void EnableRewardEarnPanel()
    {
        _rewardEarnPanel.SetActive(true);
    }

    private void DisableRewardEarnPanel()
    {
        _rewardEarnPanel.SetActive(false);
    }


    public void FillRewardCardData()
    {
        var currentReward = GameManager.Instace.GetCurrentRewardData();
        _rewardCardItemNameText.text = currentReward.rewardName;
        _rewardCardItemAmountText.text = currentReward.amount.ToString();
        _rewardCardItemIcon.sprite = currentReward.iconSprite;
    }

    private void HandleGameStateChanged(GameManager.GameState newState)
    {
        switch (newState)
        {
            case GameManager.GameState.MainMenuActive:
                DisableRewardEarnPanel();
                break;
            case GameManager.GameState.RewardEarned:
                FillRewardCardData();
                EnableRewardEarnPanel();
                break;
            case GameManager.GameState.GameWon:
                DisableRewardEarnPanel();
                break;
        }
    }

}
