using UnityEngine;
using System.Collections;

public class InstantiateControl : MonoBehaviour {

	void Awake () {
        if (!FindObjectOfType<AudioSettingsControl>())
        {
            GameObject go = new GameObject();
            go.name = "AudioSettingsControl";
            go.AddComponent<AudioSettingsControl>();
        }
        if (!FindObjectOfType<StatisticsControl>())
        {
            GameObject go = new GameObject();
            go.name = "StatisticsControl";
            go.AddComponent<StatisticsControl>();
        }
	}
}
