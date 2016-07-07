using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextLevelControl : MonoBehaviour {
    Text levelText;

    void Start () {
        levelText = GetComponent<Text>();
        UpdateLevelText();
    }

    void OnEnable() {
        EventManager.StartListening(EventManager.EventType.OnLevelChanged, UpdateLevelText);
    }

    void OnDisable() {
        EventManager.StopListening(EventManager.EventType.OnLevelChanged, UpdateLevelText);
    }

    void UpdateLevelText() {
        levelText.text = "Level " + GameControl.CurrentLevel;
    }
}
