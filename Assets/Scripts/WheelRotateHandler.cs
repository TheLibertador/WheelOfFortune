using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WheelRotateHandler : MonoBehaviour
{
    RectTransform rectTransform;
   

    [SerializeField] private float duration = 5f;
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
        Debug.Log(GameManager.Instace.currentState);
        rectTransform.DORotate(new Vector3(0, 0, rotationValue), duration, RotateMode.FastBeyond360)
            .SetEase(Ease.OutQuad)
            .OnComplete(HandleOnComplete);
    }

    private void HandleOnComplete()
    {
        Debug.Log(GameManager.Instace.GetCurrentReward());
        GameManager.Instace.ChangeGameState(GameManager.GameState.SpinEnded);
        Debug.Log(GameManager.Instace.currentState);
    }

    
}
