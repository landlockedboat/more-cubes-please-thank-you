using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
public class FadeImageUI : AnimateUI {

    [Header("Fade")]
    [SerializeField]
    float startingAlpha = 0f;
    [SerializeField]
    float finalAlpha = 1f;
    [SerializeField]
    Color startingImageColor;
    Color finalImageColor;

    Image image = null;


    protected override void ApplyAnimation(float progress)
    {
        image.color = Color.Lerp(startingImageColor, finalImageColor, progress);
    }

    protected override void FinishAnimation()
    {
        image.color = finalImageColor;
    }

    protected override void InitValues()
    {
        if (image == null)
            image = GetComponent<Image>();
        startingImageColor = new Color(startingImageColor.r, startingImageColor.g, startingImageColor.b, startingAlpha);
        image.color = startingImageColor;
        finalImageColor = new Color(startingImageColor.r, startingImageColor.g, startingImageColor.b, finalAlpha);
    }
}
