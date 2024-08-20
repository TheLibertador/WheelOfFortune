using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsManager : MonoBehaviour
{
    [SerializeField] private Button _spinButton;
    [SerializeField] private Button _silverSpinButton;
    [SerializeField] private Button _goldSpinButton;
    [SerializeField] private Button _claimButton;
    [SerializeField] private Button _reviveButton;
    [SerializeField] private Button _loseButton;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _rewardEarnedClaimButton;
    [SerializeField] private Button _playAgainButton;

    private List<Button> _buttonsOnScene = new List<Button>();


    private void OnValidate()
    {
        GetButtonsOnScene();
        if (_spinButton == null)
        {
            _spinButton = FindButtonByName("SpinButton");
        }

        if (_silverSpinButton == null)
        {
            _silverSpinButton = FindButtonByName("SilverSpinButton");
        }

        if (_goldSpinButton == null)
        {
            _goldSpinButton = FindButtonByName("GoldSpinButton");
        }

        if (_claimButton == null)
        {
            _claimButton = FindButtonByName("ClaimButton");
        }

        if (_reviveButton == null)
        {
            _reviveButton = FindButtonByName("ReviveButton");
        }

        if (_loseButton == null)
        {
            _loseButton = FindButtonByName("LoseButton");
        }

        if (_continueButton == null)
        {
            _continueButton = FindButtonByName("ContinueButton");
        }

        if (_rewardEarnedClaimButton == null)
        {
            _rewardEarnedClaimButton = FindButtonByName("RewardEarnedClaimButton");
        }

        if (_playAgainButton == null)
        {
            _playAgainButton = FindButtonByName("PlayAgainButton");
        }

    }

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= HandleGameStateChanged;

        if (_spinButton != null)
        {
            _spinButton.onClick.RemoveListener(OnSpinButtonClicked);
        }
        if (_silverSpinButton != null)
        {
            _silverSpinButton.onClick.RemoveListener(OnSpinButtonClicked);
        }
        if (_goldSpinButton != null)
        {
            _goldSpinButton.onClick.RemoveListener(OnSpinButtonClicked);
        }
        if (_claimButton != null)
        {
            _claimButton.onClick.RemoveListener(OnClaimButtonClicked);
        }
        if (_reviveButton != null)
        {
            _reviveButton.onClick.RemoveListener(OnReviveButtonClicked);
        }
        if (_loseButton != null)
        {
            _loseButton.onClick.RemoveListener(OnLoseButtonClicked);
        }
        if (_continueButton != null)
        {
            _continueButton.onClick.RemoveListener(OnContinueButtonClicked);
        }
        if (_rewardEarnedClaimButton != null)
        {
            _rewardEarnedClaimButton.onClick.RemoveListener(OnRewardEarnedClaimButtonClicked);
        }
        if (_playAgainButton != null)
        {
            _playAgainButton.onClick.RemoveListener(OnPlayAgainButtonClicked);
        }
    }

    private void Start()
    {
        if (_spinButton != null)
        {
            _spinButton.onClick.AddListener(OnSpinButtonClicked);
        }
        if (_silverSpinButton != null)
        {
            _silverSpinButton.onClick.AddListener(OnSpinButtonClicked);
        }
        if (_goldSpinButton != null)
        {
            _goldSpinButton.onClick.AddListener(OnSpinButtonClicked);
        }
        if (_claimButton != null)
        {
            _claimButton.onClick.AddListener(OnClaimButtonClicked);
        }
        if (_reviveButton != null)
        {
            _reviveButton.onClick.AddListener(OnReviveButtonClicked);
        }
        if (_loseButton != null)
        {
            _loseButton.onClick.AddListener(OnLoseButtonClicked);
        }
        if (_continueButton != null)
        {
            _continueButton.onClick.AddListener(OnContinueButtonClicked);
        }
        if (_rewardEarnedClaimButton != null)
        {
            _rewardEarnedClaimButton.onClick.AddListener(OnRewardEarnedClaimButtonClicked);
        }
        if (_playAgainButton != null)
        {
            _playAgainButton.onClick.AddListener(OnPlayAgainButtonClicked);
        }
    }

    
    private void GetButtonsOnScene()
    {
        Button[] buttons = FindObjectsOfType<Button>();
        foreach (Button button in buttons)
        {
            _buttonsOnScene.Add(button);
        }

    }
    private Button FindButtonByName(string buttonName)
    {
        foreach (Button button in _buttonsOnScene)
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
    }

    private void OnClaimButtonClicked()
    {
        GameManager.Instace.ChangeGameState(GameManager.GameState.RewardsCollected);
    }

    private void OnReviveButtonClicked()
    {
        GameManager.Instace.ChangeGameState(GameManager.GameState.MainMenuActive);
    }

    private void OnLoseButtonClicked()
    {
        GameManager.Instace.ChangeGameState(GameManager.GameState.GameFailed);
    }

    private void OnContinueButtonClicked()
    {
        GameManager.Instace.ChangeGameState(GameManager.GameState.MainMenuActive);
    }

    private void OnRewardEarnedClaimButtonClicked()
    {
        GameManager.Instace.ChangeGameState(GameManager.GameState.RewardsCollected);
    }

    private void OnPlayAgainButtonClicked()
    {
        GameManager.Instace.ChangeGameState(GameManager.GameState.GameWon);
    }

    private void DisableSpinButton()
    {
        _spinButton.interactable = false;
        _silverSpinButton.interactable = false;
        _goldSpinButton.interactable = false;
    }

    private void EnableSpinButton()
    {
        _spinButton.interactable = true;
        _silverSpinButton.interactable = true;
        _goldSpinButton.interactable = true;
    }

    private void HandleGameStateChanged(GameManager.GameState newState)
    {
        switch (newState)
        {
            case GameManager.GameState.MainMenuActive:
                EnableSpinButton();
                break;
            case GameManager.GameState.SpinStarted:
                DisableSpinButton();
                break;
            case GameManager.GameState.GameWon:
                EnableSpinButton();
                break;
            case GameManager.GameState.GameFailed:
                EnableSpinButton();
                break;
        }
    }
}
