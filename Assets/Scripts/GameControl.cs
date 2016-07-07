using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour
{

    [SerializeField]
    int mapWidth;
    [SerializeField]
    int mapHeight;

    [SerializeField]
    //Color[] levelColors;
    static float levelMaxDeltaHue = .1f;
    static float currentHue = .5f;
    [SerializeField]
    static int levelsPerColor = 1;
    //Look @ Start() for reference
    static Color currentColor = Color.green;

    private static int currentScore = 0;

    private static int levelEnemiesLeftToSpawn;
    private static int levelEnemiesToSpawn;
    private static int levelEnemiesKilled;
    private static int levelEnemyMinimumDamage;
    private static int levelEnemyMinimumSpeed;
    private static int levelEnemyMaximumDamage;
    private static int levelEnemyMaximumSpeed;

    static bool finishedSpawning = false;
    [SerializeField]
    private GameObject spawnerPrefab;
    [SerializeField]
    private float timeBetweenSpawners = 1f;

    private static int currentLevel = 1;



    public static int CurrentLevel
    {
        get
        {
            return currentLevel;
        }

        set
        {
            currentLevel = value;
            LevelChanged();
            EventManager.TriggerEvent(EventManager.EventType.OnLevelChanged);

        }
    }

    public static int LevelEnemiesKilled
    {
        get
        {
            return levelEnemiesKilled;
        }

        set
        {
            levelEnemiesKilled = value;
            if (levelEnemiesKilled >= levelEnemiesToSpawn)
                ++CurrentLevel;
        }
    }

    public static int LevelEnemiesLeftToSpawn
    {
        get
        {
            return levelEnemiesLeftToSpawn;
        }

        set
        {
            levelEnemiesLeftToSpawn = value;
        }
    }

    public static int CurrentScore
    {
        get
        {
            return currentScore;
        }

        set
        {
            currentScore = value;
            EventManager.TriggerEvent(EventManager.EventType.OnScoreChanged);
        }
    }

    static void LevelChanged()
    {
        levelEnemiesToSpawn = CalculateEnemiesToSpawn();
        levelEnemiesLeftToSpawn = levelEnemiesToSpawn;
        SpawnerLogic.EnemiesToSpawn = CalculateSpawnerEnemies();
        if (currentLevel % levelsPerColor == 0)
        {
            currentHue += levelMaxDeltaHue;
            currentColor = Color.HSVToRGB(currentHue, 1, 1);
        }
        levelEnemiesKilled = 0;
        CalculateDamages();
        finishedSpawning = false;
    }

    static void CalculateDamages()
    {
        levelEnemyMinimumSpeed = 5 + currentLevel/2;
        levelEnemyMaximumSpeed = 10 + currentLevel / 2;

        levelEnemyMinimumDamage = currentLevel;
        levelEnemyMaximumDamage = 5 + currentLevel;
    }

    static int CalculateSpawnerEnemies()
    {
        return currentLevel * 5;
    }

    static int CalculateEnemiesToSpawn()
    {
        return 10 + currentLevel * 5;
    }

    // Use this for initialization
    void Start()
    {
        LevelChanged();
        StartCoroutine("InitSpawners");

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator InitSpawners()
    {
        while (true)
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
                    Color.Lerp(currentColor, Color.HSVToRGB(currentHue + levelMaxDeltaHue, 1, 1), enemyPower),
                    levelEnemyMinimumDamage + levelEnemyMaximumDamage * enemyPower,
                    levelEnemyMinimumSpeed + levelEnemyMaximumSpeed * enemyPower);
                potentialEnemies -= SpawnerLogic.EnemiesToSpawn;
                if (potentialEnemies <= 0)
                {
                    finishedSpawning = true;
                }
                   
                yield return new WaitForSeconds(timeBetweenSpawners);
            }
            yield return null;
        }

    }

    public static void PlayerKilled()
    {
        EventManager.TriggerEvent(EventManager.EventType.OnGameOver);
    }
}
