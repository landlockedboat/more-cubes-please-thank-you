using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameOverPanelUI : MonoBehaviour {
    static GameOverPanelUI gameOverPanelUI;

    public static GameOverPanelUI instance
    {
        get
        {
            if (!gameOverPanelUI)
            {
                gameOverPanelUI = FindObjectOfType<GameOverPanelUI>();
                if (!gameOverPanelUI)
                {
                    Debug.LogError("There needs to be one active GameOverPanelUI script on a GameObject in your scene.");
                }
                else
                {
                    gameOverPanelUI.Init();
                }
            }

            return gameOverPanelUI;
        }
    }

    void Init()
    {
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
        PlayerPrefs.SetInt("IS_FIRST_TIME_PLAYING", 0);
        foreach (Transform child in instance.transform)
            child.gameObject.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
    }
}
