using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MarketplacePanelUI : MonoBehaviour
{

    [SerializeField]
    float statisticsPanelAnimationSpeed;
    [SerializeField]
    float upgradesPanelAnimationSpeed;
    bool finishedAppearing = false;
    bool finishedDissappearing = false;

    [Space(10)]
    [SerializeField]
    RectTransform upgradesPanel;
    [SerializeField]
    RectTransform statisticsPanel;

    [Space(10)]
    [SerializeField]
    Vector3 upgradesPanelFinalPos;
    [SerializeField]
    Vector3 statisticsPanelFinalPos;

    Vector3 upgradesPanelStartingPos;
    Vector3 statisticsPanelStartingPos;

    void OnEnable()
    {
        EventManager.StartListening(EventManager.EventType.OnUpgradesShown, OnUpgradesShown);
        EventManager.StartListening(EventManager.EventType.OnUpgradesHidden, OnUpgradesHidden);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.EventType.OnUpgradesShown, OnUpgradesShown);
        EventManager.StopListening(EventManager.EventType.OnUpgradesHidden, OnUpgradesHidden);
    }

    void OnUpgradesShown()
    {
        upgradesPanelStartingPos = upgradesPanel.anchoredPosition;
        statisticsPanelStartingPos = statisticsPanel.anchoredPosition;
        finishedAppearing = false;
        StartCoroutine("AnimateMarketplace");
    }

    void OnUpgradesHidden()
    {
        finishedAppearing = true;
        finishedDissappearing = false;
        StartCoroutine("DeanimateMarketplace");
    }

    IEnumerator AnimateMarketplace()
    {
        while (!finishedAppearing)
        {
            upgradesPanel.anchoredPosition = Vector3.MoveTowards(
                upgradesPanel.anchoredPosition,
                upgradesPanelFinalPos,
                upgradesPanelAnimationSpeed * Time.deltaTime);
            statisticsPanel.anchoredPosition = Vector3.MoveTowards(
                statisticsPanel.anchoredPosition,
                statisticsPanelFinalPos,
                statisticsPanelAnimationSpeed * Time.deltaTime);
            if(upgradesPanel.anchoredPosition.y == upgradesPanelFinalPos.y &&
                 statisticsPanel.anchoredPosition.y == statisticsPanelFinalPos.y)
            {
                finishedAppearing = true;
            }
            yield return null;
        }
    }

    IEnumerator DeanimateMarketplace()
    {
        while (!finishedDissappearing)
        {
            upgradesPanel.anchoredPosition = Vector3.MoveTowards(
                upgradesPanel.anchoredPosition,
                upgradesPanelStartingPos,
                upgradesPanelAnimationSpeed * Time.deltaTime);
            statisticsPanel.anchoredPosition = Vector3.MoveTowards(
                statisticsPanel.anchoredPosition,
                statisticsPanelStartingPos,
                statisticsPanelAnimationSpeed * Time.deltaTime);
            if (upgradesPanel.anchoredPosition.y == upgradesPanelStartingPos.y &&
                 statisticsPanel.anchoredPosition.y == statisticsPanelStartingPos.y)
            {
                finishedDissappearing = true;
                EventManager.TriggerEvent(EventManager.EventType.OnGameResumed);
            }
            yield return null;
        }
    }
}
