using UnityEngine;
using System.Collections;
using System;

public class ScaleUI : AnimateUI {

    [Header("Size")]
    [SerializeField]
    Vector2 startingSize;
    [SerializeField]
    Vector2 finalSize;

    protected override void ApplyAnimation(float progress)
    {
        transform.localScale = Vector2.Lerp(startingSize, finalSize, progress);
    }

    protected override void FinishAnimation()
    {
        transform.localScale = finalSize;
    }

    protected override void InitValues()
    {
        transform.localScale = startingSize;
    }
}
