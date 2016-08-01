using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MarketplacePanelUI : MonoBehaviour
{

    [SerializeField]
    float statisticsPanelAnimationSpeed;
    [SerializeField]
    float upgradesPanelAnimationSpeed;
    [SerializeField]
    float upgradesLevelsPanelAnimationSpeed;
    bool finishedAppearing = false;
    bool finishedDissappearing = false;

    [Space(10)]
    [SerializeField]
    RectTransform upgradesPanel;
    [SerializeField]
    RectTransform upgradesLevelsPanel;

    [Space(10)]
    [SerializeField]
    Vector3 upgradesPanelFinalPos;
    [SerializeField]
    Vector3 upgradesLevelsPanelFinalPos;

    Vector3 upgradesLevelsPanelStartingPos;
    Vector3 upgradesPanelStartingPos;

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
        upgradesLevelsPanelStartingPos = upgradesLevelsPanel.anchoredPosition;
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
            upgradesLevelsPanel.anchoredPosition = Vector3.MoveTowards(
                upgradesLevelsPanel.anchoredPosition,
                upgradesLevelsPanelFinalPos,
                upgradesLevelsPanelAnimationSpeed * Time.deltaTime);
            if (upgradesPanel.anchoredPosition.y == upgradesPanelFinalPos.y)
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
            upgradesLevelsPanel.anchoredPosition = Vector3.MoveTowards(
                upgradesLevelsPanel.anchoredPosition,
                upgradesLevelsPanelStartingPos,
                upgradesLevelsPanelAnimationSpeed * Time.deltaTime);
            if (upgradesPanel.anchoredPosition.y == upgradesPanelStartingPos.y)
            {
                finishedDissappearing = true;
                EventManager.TriggerEvent(EventManager.EventType.OnGameResumed);
            }
            yield return null;
        }
    }
}
