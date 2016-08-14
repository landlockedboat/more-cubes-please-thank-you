using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ChangeTextColorUI : AnimateUI {
    [Header("Change text color")]
    [SerializeField]
    Color startingColor;
    [SerializeField]
    Color endingColor;

    Text text;


    protected override void ApplyAnimation(float progress)
    {
        text.color = Color.Lerp(startingColor, endingColor, progress);
    }

    protected override void FinishAnimation()
    {
        text.color = endingColor;
    }

    protected override void InitValues()
    {
        text.color = startingColor;
    }

    void Awake () {
        text = GetComponent<Text>();
	}
}
