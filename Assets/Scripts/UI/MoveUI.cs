using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(RectTransform))]
public class MoveUI : AnimateUI {

    [Header("Move")]
    [SerializeField]
    Vector2 startingPos;
    [SerializeField]
    Vector2 finalPos;

    RectTransform rectTransform;

    protected override void ApplyAnimation(float progress)
    {
        rectTransform.anchoredPosition = Vector2.Lerp(startingPos, finalPos, progress);
    }

    protected override void FinishAnimation()
    {
        rectTransform.anchoredPosition = finalPos;
    }

    protected override void InitValues()
    {
        if (rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
        }
        rectTransform.anchoredPosition = startingPos;

    }
    
}
