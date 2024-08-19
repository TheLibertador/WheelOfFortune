using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WheelRotateHandler : MonoBehaviour
{
    RectTransform _rectTransform;
    [SerializeField] private float _duration = 5f;

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
        _rectTransform = gameObject.GetComponent<RectTransform>();
    }
    public void RotateWheel(float rotationValue)
    {
        _rectTransform.DORotate(new Vector3(0, 0, rotationValue), _duration, RotateMode.FastBeyond360)
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
            case GameManager.GameState.SpinStarted:
                RotateWheel(GameManager.Instace.GetWheelRotation());
                break;
        }
    }


}
