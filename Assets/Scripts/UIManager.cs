using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameFailPanel;
    
    [SerializeField] private TextMeshProUGUI _spinCount;

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= HandleGameStateChanged;
    }

    private void EnableGameFailPanel()
    {
        _gameFailPanel.SetActive(true);
    }

    private void DisableGameFailPanel()
    {
        _gameFailPanel.SetActive(false);
    }

    private void UpdateSpinCountText()
    {
        _spinCount.text = GameManager.Instace.GetSpinCount().ToString();
    }

    private void ResetSpinCountText()
    {
        _spinCount.text = "1";
    }
   
    private void HandleGameStateChanged(GameManager.GameState newState)
    {
        switch (newState)
        {
            case GameManager.GameState.MainMenuActive:
                DisableGameFailPanel();
                break;
            case GameManager.GameState.SpinEnded:
                UpdateSpinCountText();
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
                break;
        }
    }

}
