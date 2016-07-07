using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PointsTextControl : MonoBehaviour
{
    Text pointsText;

    void Start()
    {
        pointsText = GetComponent<Text>();
        UpdatePointsText();
    }

    void OnEnable()
    {
        EventManager.StartListening(EventManager.EventType.OnScoreChanged, UpdatePointsText);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.EventType.OnScoreChanged, UpdatePointsText);
    }

    void UpdatePointsText()
    {
        pointsText.text = GameControl.CurrentScore.ToString();
    }
}
