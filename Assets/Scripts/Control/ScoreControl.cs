using UnityEngine;
using System.Collections;

public class ScoreControl : MonoBehaviour
{
    int currentScore = 0;
    int currentMultiplier = 1;
    /// <summary>
    /// The time it takes to the multiplier to go down by one
    /// </summary>
    [SerializeField]
    float multiplierTime = 1;
    float currentMultiplierTime;
    /// <summary>
    /// The ammount of time it is deduced from multiplierTime when the multiplier increases by one.
    /// </summary>
    [SerializeField]
    float deltaMultiplierTime = .01f;
    bool isGamePaused = false;

    static ScoreControl scoreControl;

    public static ScoreControl instance
    {
        get
        {
            if (!scoreControl)
            {
                scoreControl = FindObjectOfType<ScoreControl>();
                if (!scoreControl)
                {
                    Debug.LogError("There needs to be one active ScoreControl script on a GameObject in your scene.");
                }
                else
                {
                    scoreControl.Init();
                }
            }

            return scoreControl;
        }
    }

    void Init()
    {
        StartCoroutine("InitMultiplier");
    }

    public static int CurrentScore
    {
        get
        {
            return instance.currentScore;
        }

        set
        {
            int deltaScore = value - instance.currentScore;
            deltaScore *= CurrentMultiplier;
            instance.currentScore = instance.currentScore + deltaScore;
            EventManager.TriggerEvent(EventManager.EventType.OnScoreChanged);
        }
    }

    void OnEnable()
    {
        EventManager.StartListening(EventManager.EventType.OnEnemyKilled, OnEnemyKilled);
        EventManager.StartListening(EventManager.EventType.OnSpawnPaused, OnGamePaused);
        EventManager.StartListening(EventManager.EventType.OnSpawnResumed, OnGameResumed);
        EventManager.StartListening(EventManager.EventType.OnGamePaused, OnGamePaused);
        EventManager.StartListening(EventManager.EventType.OnGameResumed, OnGameResumed);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.EventType.OnEnemyKilled, OnEnemyKilled);
        EventManager.StopListening(EventManager.EventType.OnSpawnPaused, OnGamePaused);
        EventManager.StopListening(EventManager.EventType.OnSpawnResumed, OnGameResumed);
        EventManager.StopListening(EventManager.EventType.OnGamePaused, OnGamePaused);
        EventManager.StopListening(EventManager.EventType.OnGameResumed, OnGameResumed);
    }

    void OnGamePaused()
    {
        isGamePaused = true;
    }

    void OnGameResumed()
    {
        isGamePaused = false;
    }

    void OnEnemyKilled()
    {
        IncrementMultiplier();
    }

    public static void IncrementMultiplier()
    {
        ++instance.currentMultiplier;
        instance.multiplierTime -= instance.deltaMultiplierTime;
        instance.currentMultiplierTime = instance.multiplierTime;
        if (StatisticsControl.GetStat(StatisticsControl.Stat.MaxMultiplierHad) < instance.currentMultiplier)
            StatisticsControl.SetStat(StatisticsControl.Stat.MaxMultiplierHad, instance.currentMultiplier);
        EventManager.TriggerEvent(EventManager.EventType.OnMultiplierChanged);
    }

    public static void DecrementMultiplier()
    {
        --instance.currentMultiplier;
        instance.multiplierTime += instance.deltaMultiplierTime;
        instance.currentMultiplierTime = instance.multiplierTime;
        EventManager.TriggerEvent(EventManager.EventType.OnMultiplierChanged);
    }

    public static int CurrentMultiplier
    {
        get
        {
            return instance.currentMultiplier;
        }
    }

    IEnumerator InitMultiplier()
    {
        while (true)
        {
            if (currentMultiplier > 0 && !isGamePaused)
            {
                currentMultiplierTime -= deltaMultiplierTime;
                if (currentMultiplierTime <= 0 && currentMultiplier > 1)
                {
                    DecrementMultiplier();
                }
            }
            yield return null;
        }

    }
}
