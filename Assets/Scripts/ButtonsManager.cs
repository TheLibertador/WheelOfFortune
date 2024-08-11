using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsManager : MonoBehaviour
{
    [SerializeField] private Button spinButton;
    [SerializeField] private Button silverSpinButton;
    [SerializeField] private Button goldSpinButton;
    [SerializeField] private Button claimButton;
    [SerializeField] private Button reviveButton;
    [SerializeField] private Button loseButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button rewardEarnedClaimButton;
    [SerializeField] private Button playAgainButton;


    private void OnValidate()
    {
        if (spinButton == null)
        {
            spinButton = FindButtonByName("SpinButton");
        }

        if (silverSpinButton == null)
        {
            silverSpinButton = FindButtonByName("SilverSpinButton");
        }

        if (goldSpinButton == null)
        {
            goldSpinButton = FindButtonByName("GoldSpinButton");
        }

        if (claimButton == null)
        {
            claimButton = FindButtonByName("ClaimButton");
        }

        if (reviveButton == null)
        {
            reviveButton = FindButtonByName("ReviveButton");
        }

        if (loseButton == null)
        {
            loseButton = FindButtonByName("LoseButton");
        }

        if (continueButton == null)
        {
            continueButton = FindButtonByName("ContinueButton");
        }

        if (rewardEarnedClaimButton == null)
        {
            rewardEarnedClaimButton = FindButtonByName("RewardEarnedClaimButton");
        }

        if (playAgainButton == null)
        {
            playAgainButton = FindButtonByName("PlayAgainButton");
        }

    }

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= HandleGameStateChanged;
    }

    private void Start()
    {
        if (spinButton != null)
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
        if (reviveButton != null)
        {
            reviveButton.onClick.AddListener(OnReviveButtonClicked);
        }
        if (loseButton != null)
        {
            loseButton.onClick.AddListener(OnLoseButtonClicked);
        }
        if (continueButton != null)
        {
            continueButton.onClick.AddListener(OnContinueButtonClicked);
        }
        if (rewardEarnedClaimButton != null)
        {
            rewardEarnedClaimButton.onClick.AddListener(OnRewardEarnedClaimButtonClicked);
        }
        if (playAgainButton != null)
        {
            playAgainButton.onClick.AddListener(OnPlayAgainButtonClicked);
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
        spinButton.interactable = false;
        silverSpinButton.interactable = false;
        goldSpinButton.interactable = false;
    }

    private void EnableSpinButton()
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
                EnableSpinButton();
                break;
            case GameManager.GameState.SpinStarted:
                DisableSpinButton();
                break;
            case GameManager.GameState.GameWon:
                EnableSpinButton();
                break;
        }
    }
}
