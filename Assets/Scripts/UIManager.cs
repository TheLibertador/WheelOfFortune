using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameFailPanel;
    [SerializeField] private GameObject _rewardEarnPanel;
    [SerializeField] private GameObject _rewardCollectionPanel;

    [SerializeField] private GameObject _bronzeZone;
    [SerializeField] private GameObject _silverZone;
    [SerializeField] private GameObject _goldZone;

    [SerializeField] private TextMeshProUGUI _spinCount;


    [SerializeField] private TextMeshProUGUI _rewardCardItemNameText;
    [SerializeField] private TextMeshProUGUI _rewardCardItemAmountText;
    [SerializeField] private Image _rewardCardItemIcon;

    [SerializeField] private Transform _rewardCollectCardsHolder;
    [SerializeField] private GameObject _rewardCollectCardPrefab;
    


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
        _gameFailPanel.SetActive(true);
    }

    private void DisableGameFailPanel()
    {
        _gameFailPanel.SetActive(false);
    }

    private void EnableRewardEarnPanel()
    {
        _rewardEarnPanel.SetActive(true);
    }

    private void DisableRewardEarnPanel()
    {
        _rewardEarnPanel.SetActive(false);
    }

    private void EnableRewardCollectionPanel()
    {
        _rewardCollectionPanel.SetActive(true);
    }

    private void DisableRewardCollectionPanel()
    {
        _rewardCollectionPanel.SetActive(false);
    }


    private void UpdateSpinCountText()
    {
        _spinCount.text = GameManager.Instace.GetSpinCount().ToString();
    }

    private void ResetSpinCountText()
    {
        _spinCount.text = "1";
    }

    private void ActivateBronzeZone()
    {
        if(!_bronzeZone.activeInHierarchy)
        {
            _bronzeZone.SetActive(true);
            _silverZone.SetActive(false);
            _goldZone.SetActive(false);
        }
    }

    private void ActivateSilverZone()
    {
        if (!_silverZone.activeInHierarchy)
        {
            _silverZone.SetActive(true);
            _bronzeZone.SetActive(false);
            _goldZone.SetActive(false);
        }
    }

    private void ActivateGoldZone()
    {
        if (!_goldZone.activeInHierarchy)
        {
            _goldZone.SetActive(true);
            _silverZone.SetActive(false);
            _bronzeZone.SetActive(false);
        }
    }

    
    private void FillRewardCardData()
    {
        var currentReward = GameManager.Instace.GetCurrentRewardData();
        _rewardCardItemNameText.text = currentReward.rewardName;
        _rewardCardItemAmountText.text = currentReward.amount.ToString();
        _rewardCardItemIcon.sprite = currentReward.iconSprite;
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
