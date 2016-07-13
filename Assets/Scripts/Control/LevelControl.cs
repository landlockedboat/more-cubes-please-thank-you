using UnityEngine;
using System.Collections;

public class LevelControl : MonoBehaviour {

    [SerializeField]
    int currentLevel = 1;

    [SerializeField]
    float levelDeltaHue = .01f;
    float currentHue = .5f;
    Color currentColor;

    [Header("Enemies spawned every level")]
    [SerializeField]
    int enemiesToSpawnTimesTheLevel = 5;
    [SerializeField]
    int baseEnemiesToSpawn = 10;

    [Header("Enemies spawned per spawner")]
    [SerializeField]
    int spawnerEnemiesToSpawnTimesTheLevel = 5;
    [SerializeField]
    int baseSpawnerEnemiesToSpawn = 10;    

    [Space(10)]
    [SerializeField]
    float baseEnemyDamage = 5;
    [SerializeField]
    float baseEnemySpeed = 6;
    [Range(0, 1)]
    [SerializeField]
    float enemyDamageIncrease = .01f;
    [Range(0, 1)]
    [SerializeField]
    float enemySpeedIncrease = .01f;

    [Header("Special level things")]
    [SerializeField]
    float speedIncreasePercentage = .6f;
    [SerializeField]
    float speedDecreasePercentage = .6f;
    bool fasterEnemies = false;
    bool slowerEnemies = false;

    [Range(0f,1f)]
    [SerializeField]
    float maxEnemyDamageIncrease = .1f;
    [Range(0f, 1f)]
    [SerializeField]
    float maxEnemySpeedIncrease = .1f;

    int levelEnemiesLeftToSpawn;
    int levelEnemiesToSpawn;
    int levelEnemiesKilled;
    float levelEnemyMinimumDamage;
    float levelEnemyMinimumSpeed;
    float levelEnemyMaximumDamage;
    float levelEnemyMaximumSpeed;

    [Space(10)]
    [SerializeField]
    float spawnMapWidth;
    [SerializeField]
    float spawnMapHeight;

    bool finishedSpawning = false;
    [SerializeField]
    GameObject spawnerPrefab;
    [SerializeField]
    float timeBetweenSpawners = 1f;

    [SerializeField]
    int levelsUntilUpgrade = 5;

    bool isGamePaused = false;


    static LevelControl levelControl;

    public static LevelControl instance
    {
        get
        {
            if (!levelControl)
            {
                levelControl = FindObjectOfType<LevelControl>();
                if (!levelControl)
                {
                    Debug.LogError("There needs to be one active LevelControl script on a GameObject in your scene.");
                }
                else
                {
                    levelControl.Init();
                }
            }

            return levelControl;
        }
    }

    public static int CurrentLevel
    {
        get
        {
            return instance.currentLevel;
        }

        set
        {
            instance.currentLevel = value;
            instance.LevelChanged();
            EventManager.TriggerEvent(EventManager.EventType.OnLevelChanged);
        }
    }

    public static int LevelEnemiesKilled
    {
        get
        {
            return instance.levelEnemiesKilled;
        }

        set
        {
            instance.levelEnemiesKilled = value;
            if (instance.levelEnemiesKilled >= instance.levelEnemiesToSpawn)
                ++CurrentLevel;
        }
    }

    public static int LevelEnemiesLeftToSpawn 
    {
        get
        {
            return instance.levelEnemiesLeftToSpawn;
        }

        set
        {
            instance.levelEnemiesLeftToSpawn = value;
        }
    }

    public static float SpawnMapHeight
    {
        get
        {
            return instance.spawnMapHeight;
        }

        set
        {
            instance.spawnMapHeight = value;
        }
    }

    public static float SpawnMapWidth
    {
        get
        {
            return instance.spawnMapWidth;
        }

        set
        {
            instance.spawnMapWidth = value;
        }
    }

    public static bool FasterEnemies
    {
        set
        {
            instance.fasterEnemies = value;
        }
    }

    public static bool SlowerEnemies
    {
        set
        {
            instance.slowerEnemies = value;
        }
    }

    void OnEnable()
    {
        EventManager.StartListening(EventManager.EventType.OnEnemyKilled, OnEnemyKilled);
        EventManager.StartListening(EventManager.EventType.OnGamePaused, OnGamePaused);
        EventManager.StartListening(EventManager.EventType.OnGameResumed, OnGameResumed);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.EventType.OnEnemyKilled, OnEnemyKilled);
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
        StatisticsControl.AddToStat(StatisticsControl.Stat.TotalEnemiesKilled, 1);        
        ++LevelEnemiesKilled;
    }

    void Init()
    {
        instance.levelEnemyMinimumSpeed = baseEnemySpeed;
        instance.levelEnemyMinimumDamage = baseEnemyDamage;
    
        LevelChanged();
        StartCoroutine("InitSpawners");
    }

    IEnumerator InitSpawners()
    {
        while (true)
        {
            if (!isGamePaused)
            {
                int potentialEnemies = levelEnemiesToSpawn;
                while (!finishedSpawning)
                {
                    yield return new WaitForSeconds(timeBetweenSpawners);
                    GameObject spawner =
                    Instantiate(
                        spawnerPrefab,
                        new Vector3(
                        Random.Range(-spawnMapWidth / 2, spawnMapWidth / 2),
                        0,
                        Random.Range(-spawnMapHeight / 2, spawnMapHeight / 2)
                        ),
                        Quaternion.identity
                        ) as GameObject;

                    float enemyPower = Random.value;
                    Color enemyColor = Color.Lerp(currentColor, Color.HSVToRGB(currentHue + levelDeltaHue, 1, 1), enemyPower);
                    float enemyDamage = Mathf.Max(levelEnemyMinimumDamage, levelEnemyMaximumDamage * enemyPower);
                    float enemySpeed = Mathf.Max(levelEnemyMinimumSpeed, levelEnemyMaximumSpeed * enemyPower);
                    if (fasterEnemies)
                        enemySpeed *= speedIncreasePercentage;
                    else if (slowerEnemies)
                        enemySpeed *= speedDecreasePercentage;
                    spawner.GetComponent<SpawnerLogic>().Init(
                        enemyColor,
                        enemyDamage,
                        enemySpeed
                        );
                    potentialEnemies -= SpawnerControl.EnemiesToSpawn;
                    if (potentialEnemies <= 0)
                    {
                        finishedSpawning = true;
                    }
                }
            }
            yield return null;
        }

    }

    void LevelChanged()
    {
        instance.levelEnemiesToSpawn = CalculateEnemiesToSpawn();
        instance.levelEnemiesLeftToSpawn = instance.levelEnemiesToSpawn;
        SpawnerControl.EnemiesToSpawn = CalculateSpawnerEnemies();
        currentHue += levelDeltaHue;
        if (currentHue >= 1)
            currentHue = 0;
        currentColor = Color.HSVToRGB(currentHue, 1, 1);
        CameraController.IsGrowing = true;
        instance.levelEnemiesKilled = 0;
        CalculateDamages();
        finishedSpawning = false;

        SpawnerControl.TimeBetweenSpawns *= .95f;

        if (currentLevel % levelsUntilUpgrade == 0)
        {            
            UpgradeControl.ShowUpgrades();
            EventManager.TriggerEvent(EventManager.EventType.OnGamePaused);
            EventManager.TriggerEvent(EventManager.EventType.OnUpgradesShown);
        }
    }

    void CalculateDamages()
    {
        instance.levelEnemyMinimumSpeed *= 1 + enemySpeedIncrease;
        instance.levelEnemyMaximumSpeed = instance.levelEnemyMinimumSpeed * (1 + maxEnemySpeedIncrease);

        instance.levelEnemyMinimumDamage *= 1 + enemyDamageIncrease;
        instance.levelEnemyMaximumDamage = instance.levelEnemyMinimumDamage * (1 + maxEnemyDamageIncrease);
    }

    int CalculateSpawnerEnemies()
    {
        return baseSpawnerEnemiesToSpawn + currentLevel * spawnerEnemiesToSpawnTimesTheLevel;
    }

    int CalculateEnemiesToSpawn()
    {
        return baseEnemiesToSpawn + currentLevel * enemiesToSpawnTimesTheLevel; ;
    }

}
