using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlayMusicOnStart : MonoBehaviour {
    AudioSource audioSource;

    void Start () {
        audioSource = GetComponent<AudioSource>();
        if (AudioSettingsControl.MusicOn)
            audioSource.Play();
    }

    void OnEnable()
    {
        EventManager.StartListening(EventManager.EventType.OnMusicMuted, OnMusicMuted);
        EventManager.StartListening(EventManager.EventType.OnMusicUnmuted, OnMusicEnabled);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.EventType.OnMusicMuted, OnMusicMuted);
        EventManager.StopListening(EventManager.EventType.OnMusicUnmuted, OnMusicEnabled);
    }

    void OnMusicMuted()
    {
        audioSource.Stop();
    }

    void OnMusicEnabled()
    {
        audioSource.Play();
    }
}
