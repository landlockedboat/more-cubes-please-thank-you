using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Light))]
public class FadeLight : AnimateUI {
    [Range(0,8)]
    [SerializeField]
    float startingIntensity = 1f;
    [Range(0, 8)]
    [SerializeField]
    float endingIntensity = 0f;

    new Light light;

    protected override void ApplyAnimation(float progress)
    {
        light.intensity = Mathf.Lerp(startingIntensity, endingIntensity, progress);
    }

    protected override void FinishAnimation()
    {
        light.intensity = endingIntensity;
    }

    protected override void InitValues()
    {
        light.intensity = startingIntensity;
    }

    void Start () {
        light = GetComponent<Light>();
    }
}
