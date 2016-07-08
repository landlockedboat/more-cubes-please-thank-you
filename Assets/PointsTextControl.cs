using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PointsTextControl : MonoBehaviour
{
    Text scoreText;

    void Start()
    {
        scoreText = GetComponent<Text>();
        UpdateScoreText();
    }

    void OnEnable()
    {
        EventManager.StartListening(EventManager.EventType.OnScoreChanged, UpdateScoreText);
        EventManager.StartListening(EventManager.EventType.OnMultiplierChanged, UpdateScoreText);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.EventType.OnScoreChanged, UpdateScoreText);
        EventManager.StopListening(EventManager.EventType.OnMultiplierChanged, UpdateScoreText);
    }

    void UpdateScoreText()
    {
        scoreText.text = GameControl.CurrentScore.ToString() + " x" + GameControl.CurrentMultiplier;
    }
}
