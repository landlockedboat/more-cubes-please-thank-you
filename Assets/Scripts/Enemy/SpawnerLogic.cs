using UnityEngine;
using System.Collections;

public class SpawnerLogic : MonoBehaviour {

    float timeToBeginSpawning = .1f;
    float damage;
    float speed;
    float fadeSpeed;
    bool fadingIn = true;

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

        enemiesToSpawn = SpawnerControl.EnemiesToSpawn;
        timeBetweenSpawns = SpawnerControl.TimeBetweenSpawns;
        timeToBeginSpawning = SpawnerControl.TimeToBeginSpawning;
        fadeSpeed = SpawnerControl.FadeSpeed;

        bigEnemies = SpawnerControl.BiggerEnemies;
        smallEnemies = SpawnerControl.SmallerEnemies;
        StartCoroutine("FadeIn");
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn() {
        yield return new WaitForSeconds(timeToBeginSpawning);
        while (!finishedSpawning)
        {
            if (!isGamePaused)
            {
                if (LevelControl.LevelEnemiesLeftToSpawn > 0)
                {
                    --LevelControl.LevelEnemiesLeftToSpawn;
                    GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity) as GameObject;
                    enemy.name = "Enemy #" + LevelControl.LevelEnemiesLeftToSpawn;                    
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
                    enemy.GetComponent<EnemyDeath>().Init(damage);
                    enemy.transform.GetChild(0).GetComponent<EnemyGeomLogic>().Init(color);
                    enemy.GetComponent<EnemySimpleMovement>().Init(speed);
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

    IEnumerator FadeIn() {
        SpriteRenderer mySpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        mySpriteRenderer.color = mySpriteRenderer.color *= new Color(1f, 1f, 1f, 0);
        while (fadingIn)
        {
            mySpriteRenderer.color += new Color(0, 0, 0, fadeSpeed * Time.deltaTime);
            if (mySpriteRenderer.color.a >= 1)
                fadingIn = false;
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        fadingIn = false;
        SpriteRenderer mySpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        bool fadingOut = true;
        while (fadingOut)
        {
            mySpriteRenderer.color -= new Color(0, 0, 0, fadeSpeed * Time.deltaTime);
            if (mySpriteRenderer.color.a <= 0)
                fadingOut = false;
            yield return null;
        }
        Destroy(gameObject);
    }

    void OnEnable() {
        EventManager.StartListening(EventManager.EventType.OnLevelChanged, Destroy);
        EventManager.StartListening(EventManager.EventType.OnGameOver, Destroy);
        EventManager.StartListening(EventManager.EventType.OnSpawnPaused, OnGamePaused);
        EventManager.StartListening(EventManager.EventType.OnSpawnResumed, OnGameResumed);
    }

    void OnDisable() {
        EventManager.StopListening(EventManager.EventType.OnLevelChanged, Destroy);
        EventManager.StopListening(EventManager.EventType.OnSpawnPaused, OnGamePaused);
        EventManager.StopListening(EventManager.EventType.OnSpawnResumed, OnGameResumed);
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
        StartCoroutine("FadeOut");
    }

}
