using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HideCrosshairOnPause : MonoBehaviour {

    void OnEnable()
    {
        EventManager.StartListening(EventManager.EventType.OnGamePaused, OnGamePaused);
        EventManager.StartListening(EventManager.EventType.OnGameResumed, OnGameResumed);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.EventType.OnGamePaused, OnGamePaused);
        EventManager.StopListening(EventManager.EventType.OnGameResumed, OnGameResumed);
    }

    void OnGamePaused()
    {
        Cursor.visible = true;
        GetComponent<Image>().enabled = false;
    }

    void OnGameResumed()
    {
        Cursor.visible = false;
        GetComponent<Image>().enabled = true;
    }
}

