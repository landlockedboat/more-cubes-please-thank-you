using UnityEngine;
using System.Collections;
[RequireComponent(typeof(FadeImageUI))]
[RequireComponent(typeof(AudioSource))]
public class DamageSplatterController : MonoBehaviour {
    
    FadeImageUI fadeImage;
    AudioSource audioSource;

    void Awake () {
        fadeImage = GetComponent<FadeImageUI>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        EventManager.StartListening(EventManager.EventType.OnPlayerHurt, OnPlayerHurt);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.EventType.OnPlayerHurt, OnPlayerHurt);
    }

    void OnPlayerHurt()
    {
        fadeImage.StartAnimation();
        if(AudioSettingsControl.AudioOn)
            audioSource.PlayOneShot(audioSource.clip);
    }
}
