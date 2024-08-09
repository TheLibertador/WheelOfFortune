using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button spinButton;
    [SerializeField] private Button silverSpinButton;
    [SerializeField] private Button goldSpinButton;
    [SerializeField] private Button claimButton;
    [SerializeField] private Button reviveButton;
    [SerializeField] private Button loseButton;

    [SerializeField] private GameObject gameFailPanel;
    [SerializeField] private GameObject rewardCollectionPanel;

    [SerializeField] private TextMeshProUGUI spinCount;


    [SerializeField] private GameObject bronzeZone;
    [SerializeField] private GameObject silverZone;
    [SerializeField] private GameObject goldZone;



    private void OnValidate()
    {
        if(spinButton == null)
        {
            spinButton = FindButtonByName("SpinButton");
        }

        if(silverSpinButton == null)
        {
            silverSpinButton = FindButtonByName("SilverSpinButton");
        }

        if(goldSpinButton == null)
        {
            goldSpinButton = FindButtonByName("GoldSpinButton");
        }

        if(claimButton == null)
        {
            claimButton = FindButtonByName("ClaimButton");
        }

        if(reviveButton == null)
        {
            reviveButton = FindButtonByName("ReviveButton");
        }

        if(loseButton == null)
        {
            loseButton = FindButtonByName("LoseButton");
        }
    }

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

    private void Start()
    {
        if(spinButton != null)
        {
            spinButton.onClick.AddListener(OnSpinButtonClicked);
        }
        if (silverSpinButton != null)
        {
            silverSpinButton.onClick.AddListener(OnSpinButtonClicked);
        }
        if (goldSpinButton != null)
        {
            goldSpinButton.onClick.AddListener(OnSpinButtonClicked);
        }
        if (claimButton != null)
        {
            claimButton.onClick.AddListener(OnClaimButtonClicked);
        }
        if(reviveButton != null)
        {
            reviveButton.onClick.AddListener(OnReviveButtonClicked);
        }
        if (loseButton != null)
        {
            loseButton.onClick.AddListener(OnLoseButtonClicked);
        }
    }

    private Button FindButtonByName(string buttonName)
    {
        Button[] buttons = FindObjectsOfType<Button>();
        foreach (Button button in buttons)
        {
            if (button.gameObject.name == buttonName)
            {
                return button;
            }
        }
        return null;
    }

    private void OnSpinButtonClicked()
    {
        GameManager.Instace.SpinWheel();
        UpdateSpinCountText();
    }

    private void OnClaimButtonClicked()
    {
        GameManager.Instace.ChangeGameState(GameManager.GameState.AllRewardsCollected);
    }

    private void OnReviveButtonClicked()
    {
        GameManager.Instace.ChangeGameState(GameManager.GameState.MainMenuActive);
    }

    private void OnLoseButtonClicked()
    {
        GameManager.Instace.ChangeGameState(GameManager.GameState.GameFailed);
    }

    public void EnableGameFailPanel()
    {
        gameFailPanel.SetActive(true);
    }

    public void DisableGameFailPanel()
    {
        gameFailPanel.SetActive(false);
    }

    public void EnableRewardCollectionPanel()
    {
        rewardCollectionPanel.SetActive(true);
    }
    
    public void DisableRewardCollectionPanel()
    {
        rewardCollectionPanel.SetActive(false);
    }

    private void UpdateSpinCountText()
    {
        spinCount.text = GameManager.Instace.GetSpinCount().ToString();
    }

    public void ResetSpinCount()
    {
        spinCount.text = "0";
    }

    public void ActivateBronzeZone()
    {
        if(!bronzeZone.activeInHierarchy)
        {
            bronzeZone.SetActive(true);
            silverZone.SetActive(false);
            goldZone.SetActive(false);
        }
    }

    public void ActivateSilverZone()
    {
        if (!silverZone.activeInHierarchy)
        {
            silverZone.SetActive(true);
            bronzeZone.SetActive(false);
            goldZone.SetActive(true);
        }
    }

    public void ActivateGoldZone()
    {
        if (!goldZone.activeInHierarchy)
        {
            goldZone.SetActive(true);
            silverZone.SetActive(false);
            bronzeZone.SetActive(false);
        }
    }

    public void DisableSpinButton()
    {
        spinButton.interactable = false;
        silverSpinButton.interactable = false;
        goldSpinButton.interactable = false;
    }

    public void EnableSpinButton()
    {
        spinButton.interactable = true;
        silverSpinButton.interactable = true;
        goldSpinButton.interactable = true;
    }


    private void HandleGameStateChanged(GameManager.GameState newState)
    {
        switch (newState)
        {
            case GameManager.GameState.MainMenuActive:
                ResetSpinCount();
                DisableGameFailPanel();
                DisableRewardCollectionPanel();
                break;
            case GameManager.GameState.SpinStarted:
                DisableSpinButton();
                break;
            case GameManager.GameState.SpinEnded:
                EnableSpinButton();
                break;
            case GameManager.GameState.RewardCollected:
            case GameManager.GameState.AllRewardsCollected:
                EnableRewardCollectionPanel();
                break;
            case GameManager.GameState.GameFailed:
                EnableGameFailPanel();
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
