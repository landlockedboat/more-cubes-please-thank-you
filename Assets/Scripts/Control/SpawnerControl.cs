using UnityEngine;
using System.Collections;

public class SpawnerControl : MonoBehaviour {
    [SerializeField]
    float timeBetweenSpawns = .25f;
    [SerializeField]
    float timeToBeginSpawning = 1f;
    [SerializeField]
    float fadeSpeed = 6;
    [SerializeField]
    float enemyGrowingSpeed = 6f;
    int enemiesToSpawn;
    [Header("On level changed stuff")]
    [SerializeField]
    float timeToBeginSpawningDecreasePercentage = .05f;
    [SerializeField]
    float timeBetweenSpawnsDecreasePercentage = .05f;

    bool bigEnemies = false;
    bool smallEnemies = false;

    static SpawnerControl spawnerControl;

    public static SpawnerControl instance
    {
        get
        {
            if (!spawnerControl)
            {
                spawnerControl = FindObjectOfType<SpawnerControl>();
                if (!spawnerControl)
                {
                    Debug.LogError("There needs to be one active SpawnerControl script on a GameObject in your scene.");
                }
                else
                {
                    spawnerControl.Init();
                }
            }

            return spawnerControl;
        }
    }


    void Init()
    {

    }

    void OnEnable()
    {
        EventManager.StartListening(EventManager.EventType.OnLevelChanged, OnLevelChanged);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.EventType.OnLevelChanged, OnLevelChanged);
    }

    void OnLevelChanged()
    {
        timeToBeginSpawning *= 1 - timeToBeginSpawningDecreasePercentage;
        timeBetweenSpawns *= 1 - timeBetweenSpawnsDecreasePercentage;
    }


    public static float TimeBetweenSpawns
    {
        set
        {
            instance.timeBetweenSpawns = value;
        }
        get
        {
            return instance.timeBetweenSpawns;
        }
    }


    public static bool BiggerEnemies
    {
        get
        {
            return instance.bigEnemies;
        }
        set
        {
            instance.bigEnemies = value;
        }
    }

    public static bool SmallerEnemies
    {
        get
        {
            return instance.smallEnemies;
        }
        set
        {
            instance.smallEnemies = value;
        }
    }

    public static float TimeToBeginSpawning
    {
        get
        {
            return instance.timeToBeginSpawning;
        }
    }

    public static float FadeSpeed
    {
        get
        {
            return instance.fadeSpeed;
        }
    }

    public static float EnemyGrowingSpeed
    {
        get
        {
            return instance.enemyGrowingSpeed;
        }
    }

    public static int EnemiesToSpawn
    {
        get
        {
            return instance.enemiesToSpawn;
        }

        set
        {
            instance.enemiesToSpawn = value;
        }
    }
}
