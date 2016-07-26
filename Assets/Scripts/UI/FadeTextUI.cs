using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
[RequireComponent(typeof(Text))]
/// <summary>
/// This class uses a Text and / or an Image component to create the fade animation.
/// </summary>
public class FadeTextUI : AnimateUI
{

    [Header("Fade")]
    [SerializeField]
    float startingAlpha = 0f;
    [SerializeField]
    float finalAlpha = 1f;
    [SerializeField]
    Color startingTextColor;
    Color finalTextColor;

    Text text = null;


    protected override void ApplyAnimation(float progress)
    {
        text.color = Color.Lerp(startingTextColor, finalTextColor, progress);
    }

    protected override void FinishAnimation()
    {
        text.color = finalTextColor;
    }

    protected override void InitValues()
    {
        if (text == null)
            text = GetComponent<Text>();
        startingTextColor = new Color(startingTextColor.r, startingTextColor.g, startingTextColor.b, startingAlpha);
        text.color = startingTextColor;
        finalTextColor = new Color(startingTextColor.r, startingTextColor.g, startingTextColor.b, finalAlpha);
    }
}
