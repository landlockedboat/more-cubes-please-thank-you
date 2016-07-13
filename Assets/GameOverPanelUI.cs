using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverPanelUI : MonoBehaviour {
    ShrinkAndMoveGameOverPanel sam;

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
        instance.transform.GetChild(0).gameObject.SetActive(true);
        instance.sam = instance.gameObject.GetComponent<ShrinkAndMoveGameOverPanel>();
        instance.sam.Animate();
    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
    }
}
