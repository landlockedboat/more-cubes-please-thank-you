using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextLevelControl : MonoBehaviour{
    Text levelText;
    [SerializeField]
    AnimateUI moveUI;
    [SerializeField]
    AnimateUI scaleUI;
    [SerializeField]
    AudioSource audioSource;


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
        levelText.text = "Level " + LevelControl.CurrentLevel;
        moveUI.StartAnimation();
        scaleUI.StartAnimation();
        if(AudioSettingsControl.AudioOn)
            audioSource.Play();
    }
}
