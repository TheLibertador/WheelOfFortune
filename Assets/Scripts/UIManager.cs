using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameFailPanel;
    [SerializeField] private GameObject rewardEarnPanel;
    [SerializeField] private GameObject rewardCollectionPanel;

    [SerializeField] private GameObject bronzeZone;
    [SerializeField] private GameObject silverZone;
    [SerializeField] private GameObject goldZone;

    [SerializeField] private TextMeshProUGUI spinCount;


    [SerializeField] private TextMeshProUGUI rewardCardItemNameText;
    [SerializeField] private TextMeshProUGUI rewardCardItemAmountText;
    [SerializeField] private Image rewardCardItemIcon;

    [SerializeField] private Transform rewardCollectCardsHolder;
    [SerializeField] private GameObject rewardCollectCardPrefab;
    


    private void OnEnable()
    {
        GameManager.OnGameStateChanged += HandleGameStateChanged;
        GameManager.OnZoneChanged += HandleZoneChanged;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= HandleGameStateChanged;
        GameManager.OnZoneChanged -= HandleZoneChanged;
    }

    private void EnableGameFailPanel()
    {
        gameFailPanel.SetActive(true);
    }

    private void DisableGameFailPanel()
    {
        gameFailPanel.SetActive(false);
    }

    private void EnableRewardEarnPanel()
    {
        rewardEarnPanel.SetActive(true);
    }

    private void DisableRewardEarnPanel()
    {
        rewardEarnPanel.SetActive(false);
    }

    private void EnableRewardCollectionPanel()
    {
        rewardCollectionPanel.SetActive(true);
    }

    private void DisableRewardCollectionPanel()
    {
        rewardCollectionPanel.SetActive(false);
    }


    private void UpdateSpinCountText()
    {
        spinCount.text = GameManager.Instace.GetSpinCount().ToString();
    }

    private void ResetSpinCountText()
    {
        spinCount.text = "1";
    }

    private void ActivateBronzeZone()
    {
        if(!bronzeZone.activeInHierarchy)
        {
            bronzeZone.SetActive(true);
            silverZone.SetActive(false);
            goldZone.SetActive(false);
        }
    }

    private void ActivateSilverZone()
    {
        if (!silverZone.activeInHierarchy)
        {
            silverZone.SetActive(true);
            bronzeZone.SetActive(false);
            goldZone.SetActive(false);
        }
    }

    private void ActivateGoldZone()
    {
        if (!goldZone.activeInHierarchy)
        {
            goldZone.SetActive(true);
            silverZone.SetActive(false);
            bronzeZone.SetActive(false);
        }
    }

    
    private void FillRewardCardData()
    {
        var currentReward = GameManager.Instace.GetCurrentRewardData();
        rewardCardItemNameText.text = currentReward.rewardName;
        rewardCardItemAmountText.text = currentReward.amount.ToString();
        rewardCardItemIcon.sprite = currentReward.iconSprite;
    }

    private void FillRewardCollectScroll()
    {
        if (GameManager.Instace.GetAllRewardData() != null)
        {
            var rewardDataDictionary = GameManager.Instace.GetAllRewardData();
        
            foreach (var reward in rewardDataDictionary)
            {
                GameObject card = Instantiate(rewardCollectCardPrefab, rewardCollectCardsHolder);
                Transform rewardNameText = card.transform.Find("RewardNameText");
                rewardNameText.GetComponent<TextMeshProUGUI>().text = reward.Key.rewardName;
                Transform rewardAmountText = card.transform.Find("RewardAmountText");
                rewardAmountText.GetComponent<TextMeshProUGUI>().text = reward.Value.ToString();
                Transform rewardIcon = card.transform.Find("RewardIcon");
                rewardIcon.GetComponent<Image>().sprite = reward.Key.iconSprite;
            }

        }

    }
    private void HandleGameStateChanged(GameManager.GameState newState)
    {
        switch (newState)
        {
            case GameManager.GameState.MainMenuActive:
                DisableGameFailPanel();
                DisableRewardCollectionPanel();
                DisableRewardEarnPanel();
                break;
            case GameManager.GameState.SpinEnded:
                UpdateSpinCountText();
                break;
            case GameManager.GameState.RewardsCollected:
                FillRewardCollectScroll();
                EnableRewardCollectionPanel();
                break;
            case GameManager.GameState.RewardEarned:
                FillRewardCardData();
                EnableRewardEarnPanel();
                break;
            case GameManager.GameState.BombExploded:
                EnableGameFailPanel();
                break;
            case GameManager.GameState.GameFailed:
                ResetSpinCountText();
                DisableGameFailPanel();
                break;
            case GameManager.GameState.GameWon:
                ResetSpinCountText();
                DisableRewardCollectionPanel();
                DisableRewardEarnPanel();
                break;
        }
    }

    private void HandleZoneChanged(GameManager.Zone newZone)
    {
        switch (newZone)
        {
            case GameManager.Zone.Bronze:
                ActivateBronzeZone();
                break;
            case GameManager.Zone.Silver:
                ActivateSilverZone();
                break;
            case GameManager.Zone.Gold:
                ActivateGoldZone();
                break;
        }
    }
}
