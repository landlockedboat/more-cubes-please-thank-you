using UnityEngine;
using System.Collections;
using System;

public class LevitateUI : LoopAnimationUI {

    [Header("Levitate")]
    [SerializeField]
    Vector2 startingPos;
    [SerializeField]
    Vector2 finalPos;

    RectTransform rectTransform;

    protected override void Animate(float progress)
    {
        rectTransform.anchoredPosition = Vector2.Lerp(startingPos, finalPos, progress);
    }

    protected override void Deanimate(float progress)
    {
        rectTransform.anchoredPosition = Vector2.Lerp(finalPos, startingPos, progress);
    }

    protected override void ResetValues()
    {
        rectTransform.anchoredPosition = startingPos;
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
