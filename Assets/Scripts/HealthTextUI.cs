using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class HealthTextUI : UIFollowWorldObject {

    Text text;
    bool isGameOver = false;

    static HealthTextUI healthTextUI;

    public static HealthTextUI instance
    {
        get
        {
            if (!healthTextUI)
            {
                healthTextUI = FindObjectOfType<HealthTextUI>();
                if (!healthTextUI)
                {
                    Debug.LogError("There needs to be one active HealthTextUI script on a GameObject in your scene.");
                }
                else
                {
                    healthTextUI.Init();
                }
            }

            return healthTextUI;
        }
    }

    void Init()
    {
        text = GetComponent<Text>();
    }

    void OnEnable()
    {
        EventManager.StartListening(EventManager.EventType.OnGameOver, OnGameOver);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.EventType.OnGameOver, OnGameOver);

    }

    void OnGameOver()
    {
        isGameOver = true;
        Destroy(gameObject);
    }

    public static void SetText(string s)
    {
        instance.text.text = s;
    }

	void Update () {
        if (isGameOver)
            return;
        FollowObject();
    }
}
