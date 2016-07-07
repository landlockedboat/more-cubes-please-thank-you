using UnityEngine;
using System.Collections;

public class SpawnerLogic : MonoBehaviour {
    private static int enemiesToSpawn = 20;
    private static float timeToSpawn = .25f;
    private float damage;
    private float speed;
    private Color color;
    private bool finishedSpawning = false;
    private int enemiesSpawned = 0;
    [SerializeField]
    GameObject enemyPrefab;
    public static int EnemiesToSpawn
    {
        get
        {
            return enemiesToSpawn;
        }

        set
        {
            enemiesToSpawn = value;
        }
    }

    public static float TimeToSpawn
    {
        get
        {
            return timeToSpawn;
        }

        set
        {
            timeToSpawn = value;
        }
    }

    // Use this for initialization
    public void Init (Color color, float damage, float speed) {
        this.damage = damage;
        this.speed = speed;
        this.color = color;
        StartCoroutine("Spawn");
	}

    IEnumerator Spawn() {
        while(!finishedSpawning)
        {
            if (GameControl.LevelEnemiesLeftToSpawn > 0)
            {
                --GameControl.LevelEnemiesLeftToSpawn;
                GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity) as GameObject;
                enemy.name = "Enemy #" + GameControl.LevelEnemiesLeftToSpawn;
                enemy.GetComponent<EnemyLogic>().Init(color, damage, speed);
                ++enemiesSpawned;
                finishedSpawning = enemiesSpawned == enemiesToSpawn;
            }
            else
                finishedSpawning = true;
            yield return new WaitForSeconds(timeToSpawn);
        }
        Destroy();
    }

    void OnEnable() {
        EventManager.StartListening(EventManager.EventType.OnLevelChanged, Destroy);
        EventManager.StartListening(EventManager.EventType.OnGameOver, Destroy);
    }

    void OnDisable() {
        EventManager.StopListening(EventManager.EventType.OnLevelChanged, Destroy);
        EventManager.StopListening(EventManager.EventType.OnGameOver, Destroy);
    }

    void Destroy() {
        Destroy(gameObject);
    }

}
