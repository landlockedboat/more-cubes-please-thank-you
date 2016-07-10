using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour
{
    [SerializeField]
    float mapWidth;
    [SerializeField]
    float mapHeight;
    [SerializeField]
    float levelDeltaHue = .01f;    
    float currentHue = .5f;
    //Look @ Start() for reference
    Color currentColor = Color.green;

    private int currentScore = 0;
    private int currentMultiplier = 0;
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

    int levelEnemiesLeftToSpawn;
    int levelEnemiesToSpawn;
    int levelEnemiesKilled;
    int levelEnemyMinimumDamage;
    int levelEnemyMinimumSpeed;
    int levelEnemyMaximumDamage;
    int levelEnemyMaximumSpeed;

    [SerializeField]
    int levelsUntilUpgrade = 5;

    bool gamePaused = false;

    bool finishedSpawning = false;
    [SerializeField]
    private GameObject spawnerPrefab;
    [SerializeField]
    private float timeBetweenSpawners = 1f;
    [SerializeField]
    private int currentLevel = 1;

    private static GameControl gameControl;

    public static GameControl instance
    {
        get
        {
            if (!gameControl)
            {
                gameControl = FindObjectOfType<GameControl>();
                if (!gameControl)
                {
                    Debug.LogError("There needs to be one active GameControl script on a GameObject in your scene.");
                }
                else
                {
                    gameControl.Init();
                }
            }

            return gameControl;
        }
    }

    void Init()
    {
        LevelChanged();
        StartCoroutine("InitSpawners");
        StartCoroutine("InitMultiplier");
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
    }

    void OnDisable()
    {
        EventManager.StartListening(EventManager.EventType.OnEnemyKilled, OnEnemyKilled);
    }

    private void OnEnemyKilled()
    {
        ++LevelEnemiesKilled;
        IncrementMultiplier();
    }

    public static void IncrementMultiplier() {
        ++instance.currentMultiplier;
        instance.multiplierTime -= instance.deltaMultiplierTime;
        instance.currentMultiplierTime = instance.multiplierTime;
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


    public static float MapWidth
    {
        get
        {
            return instance.mapWidth;
        }

        set
        {
            instance.mapWidth = value;
        }
    }

    public static float MapHeight
    {
        get
        {
            return instance.mapHeight;
        }

        set
        {
            instance.mapHeight = value;
        }
    }

    public static bool GamePaused
    {
        get
        {
            return instance.gamePaused;
        }

        set
        {
            instance.gamePaused = value;
        }
    }

    void LevelChanged()
    {
        instance.levelEnemiesToSpawn = CalculateEnemiesToSpawn();
        instance.levelEnemiesLeftToSpawn = instance.levelEnemiesToSpawn;
        SpawnerLogic.EnemiesToSpawn = CalculateSpawnerEnemies();
        currentHue += levelDeltaHue;
        if (currentHue >= 1)
            currentHue = 0;
        currentColor = Color.HSVToRGB(currentHue, 1, 1);
        CameraController.IsGrowing = true;
        instance.levelEnemiesKilled = 0;
        CalculateDamages();
        finishedSpawning = false;

        SpawnerLogic.TimeBetweenSpawns *= .95f;

        if(currentLevel % levelsUntilUpgrade == 0)
        {
            GamePaused = true;
            UpgradeControl.ShowUpgrades();
            EventManager.TriggerEvent(EventManager.EventType.OnUpgradesShown);
        }
    }

    void CalculateDamages()
    {
        instance.levelEnemyMinimumSpeed = 5 + currentLevel/2;
        instance.levelEnemyMaximumSpeed = 10 + currentLevel / 2;

        instance.levelEnemyMinimumDamage = currentLevel;
        instance.levelEnemyMaximumDamage = 5 + currentLevel;
    }

    public static void ResumeGame()
    {
        GamePaused = false;
    }

    int CalculateSpawnerEnemies()
    {
        return currentLevel * 5;
    }

    int CalculateEnemiesToSpawn()
    {
        return currentLevel * 1;
    }

    IEnumerator InitSpawners()
    {
        while (true)
        {
            if (!GamePaused)
            {
                int potentialEnemies = levelEnemiesToSpawn;
                while (!finishedSpawning)
                {
                    GameObject spawner =
                    Instantiate(
                        spawnerPrefab,
                        new Vector3(
                        Random.Range(-mapWidth / 2, mapWidth / 2),
                        0,
                        Random.Range(-mapHeight / 2, mapHeight / 2)
                        ),
                        Quaternion.identity
                        ) as GameObject;
                    float enemyPower = Random.value;
                    spawner.GetComponent<SpawnerLogic>().Init(
                        Color.Lerp(currentColor, Color.HSVToRGB(currentHue + levelDeltaHue, 1, 1), enemyPower),
                        levelEnemyMinimumDamage + levelEnemyMaximumDamage * enemyPower,
                        levelEnemyMinimumSpeed + levelEnemyMaximumSpeed * enemyPower);
                    potentialEnemies -= SpawnerLogic.EnemiesToSpawn;
                    if (potentialEnemies <= 0)
                    {
                        finishedSpawning = true;
                    }
                    yield return new WaitForSeconds(timeBetweenSpawners);
                }
            }            
            yield return null;
        }

    }

    IEnumerator InitMultiplier() {
        while (true)
        {
            if(currentMultiplier > 0)
            {
                currentMultiplierTime -= deltaMultiplierTime;
                if (currentMultiplierTime <= 0)
                {
                    DecrementMultiplier();
                }
            }
            yield return null;
        }

    }

    public static void PlayerKilled()
    {
        EventManager.TriggerEvent(EventManager.EventType.OnGameOver);
    }
}
