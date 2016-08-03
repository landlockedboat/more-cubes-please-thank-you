using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

[RequireComponent(typeof(Text))]
public class BlinkTextUI : LoopAnimationUI {

    [SerializeField]
    Color primaryColor;
    [SerializeField]
    Color secondaryColor;

    Text text;

    protected override void Animate(float progress)
    {
        text.color = Color.Lerp(primaryColor, secondaryColor, progress);        
    }

    protected override void Deanimate(float progress)
    {
        text.color = Color.Lerp(secondaryColor, primaryColor, progress);        
    }

    protected override void ResetValues()
    {
        text.color = primaryColor;
    }

    protected override void InitValues()
    {
        text = GetComponent<Text>();
    }
}
