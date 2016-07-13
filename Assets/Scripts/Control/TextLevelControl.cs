using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextLevelControl : MonoBehaviour{
    Text levelText;
    ShrinkAndMove shrinkandMove;

    void Start () {
        levelText = GetComponent<Text>();
        shrinkandMove = GetComponent<ShrinkAndMove>();
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
        shrinkandMove.Animate();
    }
}
