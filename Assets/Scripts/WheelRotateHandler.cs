using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WheelRotateHandler : MonoBehaviour
{
    RectTransform rectTransform;
    [SerializeField] private float duration = 5f;

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
        GetRectTransform();
    }

    private void GetRectTransform()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
    }
    public void RotateWheel(float rotationValue)
    {
        Debug.Log("Wheel should be rotating, rotation value: " + rotationValue);
        rectTransform.DORotate(new Vector3(0, 0, rotationValue), duration, RotateMode.FastBeyond360)
            .SetEase(Ease.OutQuad)
            .OnComplete(HandleOnComplete);
    }

    private void HandleOnComplete()
    {
        GameManager.Instace.ChangeGameState(GameManager.GameState.SpinEnded);
    }


    private void HandleGameStateChanged(GameManager.GameState newState)
    {
        switch (newState)
        {
            case GameManager.GameState.MainMenuActive:
                break;
            case GameManager.GameState.SpinStarted:
                RotateWheel(GameManager.Instace.GetWheelRotation());
                break;
            case GameManager.GameState.SpinEnded:
                break;
            case GameManager.GameState.RewardCollected:
                break;
            case GameManager.GameState.AllRewardsCollected:
                break;
            case GameManager.GameState.GameFailed:
                break;
        }
    }


}
