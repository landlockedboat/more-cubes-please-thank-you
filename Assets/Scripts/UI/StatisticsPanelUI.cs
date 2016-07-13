using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class StatisticsPanelUI : MonoBehaviour
{
    [SerializeField]
    int charSize = 32;
    [SerializeField]
    float animationSpeed = 6;
    [SerializeField]
    Text statisticsText;
    [SerializeField]
    Text supportText;    
    bool animate = false;
    RectTransform statisticsTextRectTransform;
    RectTransform supportTextRect;

    void Start()
    {
        statisticsTextRectTransform = statisticsText.GetComponent<RectTransform>();
        supportTextRect = supportText.GetComponent<RectTransform>();
        StartCoroutine("Animate");
        ActivateTexts(false);
    }

    void OnEnable()
    {
        EventManager.StartListening(EventManager.EventType.OnUpgradesShown, OnUpgradesShown);
        EventManager.StartListening(EventManager.EventType.OnGameResumed, OnGameResumed);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.EventType.OnUpgradesShown, OnUpgradesShown);
        EventManager.StopListening(EventManager.EventType.OnGameResumed, OnGameResumed);
    }

    void OnGameResumed() {        
        ActivateTexts(false);
    }

    void ActivateTexts(bool value) {
        animate = value;
        statisticsText.gameObject.SetActive(value);
    }

    void OnUpgradesShown()
    {
        ActivateTexts(true);
        string statsText = "";
        foreach (StatisticsControl.Stat stat in Enum.GetValues(typeof(StatisticsControl.Stat)))
        {
            int val =
            StatisticsControl.GetStat(stat);
            statsText += EnumParser.ParseUppercase(stat) + ": " + val.ToString() + " ";
        }
        statsText += " - ";
        statisticsText.text = statsText;
        statisticsTextRectTransform.sizeDelta = new Vector2(statsText.Length * charSize, 100);

        supportText.text = statisticsText.text;
        supportTextRect.sizeDelta = statisticsTextRectTransform.sizeDelta;
    }

    IEnumerator Animate()
    {
        while (true)
        {
            if (animate)
            {
                statisticsTextRectTransform.anchoredPosition = Vector2.MoveTowards(statisticsTextRectTransform.anchoredPosition,
                    new Vector2(-statisticsTextRectTransform.rect.width, statisticsTextRectTransform.anchoredPosition.y),
                    animationSpeed * Time.deltaTime);
                if (statisticsTextRectTransform.anchoredPosition.x <= -statisticsTextRectTransform.rect.width)
                {
                    statisticsTextRectTransform.anchoredPosition = new Vector2(0, statisticsTextRectTransform.anchoredPosition.y);
                }
            }
            yield return null;
        }
    }
}
