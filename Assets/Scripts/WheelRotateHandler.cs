using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WheelRotateHandler : MonoBehaviour
{
    RectTransform rectTransform;
    private float rotationValue;

    [SerializeField] private float duration = 5f;
    [SerializeField] private float minRotationRange = 1800f;
    [SerializeField] private float maxRotationRange = 3600f;


    private void Start()
    {
        GetRectTransform();
    }

    private void GetRectTransform()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
    }
    public void RotateWheel()
    {
        CalculateRandomRotation();
        rectTransform.DORotate(new Vector3(0, 0, rotationValue), duration, RotateMode.FastBeyond360)
            .SetEase(Ease.OutQuad)
            .OnComplete(HandleOnComplete);
    }

    private void CalculateRandomRotation()
    {
        float randomRotation = Random.Range(minRotationRange, maxRotationRange);
        rotationValue = randomRotation;
    }

    private void HandleOnComplete()
    {
        Debug.Log("Completed");
    }

    
}
