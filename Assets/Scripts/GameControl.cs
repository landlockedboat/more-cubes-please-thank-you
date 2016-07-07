using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {
    [SerializeField]
    int mapWidth;
    [SerializeField]
    int mapHeight;
    private static int currentScore = 0;
    private static int levelEnemiesLeftToSpawn;
    private static int levelEnemiesToSpawn;
    private static int enemiesKilledThisLevel;
    private static int currentLevel = 1;
    static bool finishedSpawning = false;
    [SerializeField]
    private GameObject spawnerPrefab;
    [SerializeField]
    private float timeBetweenSpawners = 1f;

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

    public static int EnemiesKilledThisLevel
    {
        get
        {
            return enemiesKilledThisLevel;
        }

        set
        {
            enemiesKilledThisLevel = value;
            if (enemiesKilledThisLevel >= levelEnemiesToSpawn)
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
        enemiesKilledThisLevel = 0;
        finishedSpawning = false;
    }

    static int CalculateSpawnerEnemies()
    {
        return 10 + currentLevel * 5;
    }

    static int CalculateEnemiesToSpawn()
    {
        return currentLevel * 20;
    }

    // Use this for initialization
    void Start () {
        StartCoroutine("InitSpawners");
        LevelChanged();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator InitSpawners()
    {
        while (true)
        {
            int potentialEnemies = levelEnemiesToSpawn;
            while (!finishedSpawning)
            {
                Instantiate(
                    spawnerPrefab,
                    new Vector3(
                    Random.Range(-mapWidth / 2, mapWidth / 2),
                    0,
                    Random.Range(-mapHeight / 2, mapHeight / 2)
                    ),
                    Quaternion.identity
                    );
                potentialEnemies -= SpawnerLogic.EnemiesToSpawn;
                if (potentialEnemies <= 0)
                    finishedSpawning = true;
                yield return new WaitForSeconds(timeBetweenSpawners);
            }
            yield return null;
        }
        
    }

    public static void PlayerKilled() {
        EventManager.TriggerEvent(EventManager.EventType.OnGameOver);
    }
}
