using UnityEngine;
using UnityEngine.UI;

using System;

using System.Collections;

public class UpgradesLevelUI : MonoBehaviour {
    [SerializeField]
    Text upgradesLevelsText;

	void OnEnable()
    {
        EventManager.StartListening(EventManager.EventType.OnUpgradesShown, OnUpgradesShown);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.EventType.OnUpgradesShown, OnUpgradesShown);
    }

    void OnUpgradesShown()
    {
        string text = "";
        foreach(UpgradeControl.UpgradeType ut in Enum.GetValues(typeof(UpgradeControl.UpgradeType)))
        {
            text += EnumParser.ParseAbbreviation(ut) + ": " + UpgradeControl.GetLevel(ut) + "\n";
        }
        upgradesLevelsText.text = text;
    }
}
