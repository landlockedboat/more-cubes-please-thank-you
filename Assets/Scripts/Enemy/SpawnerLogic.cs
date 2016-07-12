using UnityEngine;
using System.Collections;

public class SpawnerLogic : MonoBehaviour {

    float timeToSpawn = .1f;
    float damage;
    float speed;
    Color color;
    bool finishedSpawning = false;
    int enemiesSpawned = 0;
    bool isGamePaused = false;
    [SerializeField]
    GameObject enemyPrefab;

    [Header("Special level stuff")]
    [SerializeField]
    float enemyScaleIncrease = .25f;
    [SerializeField]
    float enemyScaleDecrease = .25f;

    int enemiesToSpawn;
    float timeBetweenSpawns;

    bool bigEnemies;
    bool smallEnemies;


    // Use this for initialization
    public void Init (Color color, float damage, float speed) {
        this.damage = damage;
        this.speed = speed;
        this.color = color;
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = color;
        StartCoroutine("Spawn");

        enemiesToSpawn = SpawnerControl.EnemiesToSpawn;
        timeBetweenSpawns = SpawnerControl.TimeBetweenSpawns;

        bigEnemies = SpawnerControl.BiggerEnemies;
        smallEnemies = SpawnerControl.SmallerEnemies;
}

    IEnumerator Spawn() {
        yield return new WaitForSeconds(timeToSpawn);
        while (!finishedSpawning)
        {
            if (!isGamePaused)
            {
                if (LevelControl.LevelEnemiesLeftToSpawn > 0)
                {
                    --LevelControl.LevelEnemiesLeftToSpawn;
                    GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity) as GameObject;
                    enemy.name = "Enemy #" + LevelControl.LevelEnemiesLeftToSpawn;
                    enemy.GetComponent<EnemyLogic>().Init(color, damage, speed);
                    if (bigEnemies)
                    {
                        enemy.transform.localScale = enemy.transform.localScale + 
                            new Vector3(enemyScaleIncrease, enemyScaleIncrease, enemyScaleIncrease);
                    }
                    else if (smallEnemies)
                    {
                        enemy.transform.localScale = enemy.transform.localScale -
                            new Vector3(enemyScaleDecrease, enemyScaleDecrease, enemyScaleDecrease);
                    }
                    ++enemiesSpawned;
                    finishedSpawning = enemiesSpawned == enemiesToSpawn;
                }
                else
                    finishedSpawning = true;
                yield return new WaitForSeconds(timeBetweenSpawns);
            }
            yield return null;
        }
        Destroy();
    }

    void OnEnable() {
        EventManager.StartListening(EventManager.EventType.OnLevelChanged, Destroy);
        EventManager.StartListening(EventManager.EventType.OnGameOver, Destroy);
        EventManager.StartListening(EventManager.EventType.OnGamePaused, OnGamePaused);
        EventManager.StartListening(EventManager.EventType.OnGameResumed, OnGameResumed);
    }

    void OnDisable() {
        EventManager.StopListening(EventManager.EventType.OnLevelChanged, Destroy);
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

    void Destroy() {
        Destroy(gameObject);
    }

}
