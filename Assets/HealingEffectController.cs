using UnityEngine;
using System.Collections;

public class HealingEffectController : MonoBehaviour {

    ChangeTextColorUI changeTextColor;
    AudioSource audioSource;

    void Awake()
    {
        changeTextColor = GetComponent<ChangeTextColorUI>();
    }

    void OnEnable()
    {
        EventManager.StartListening(EventManager.EventType.OnPlayerHealed, OnPlayerHealed);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.EventType.OnPlayerHealed, OnPlayerHealed);
    }

    void OnPlayerHealed()
    {
        changeTextColor.StartAnimation();
    }
}
