using UnityEngine;
using System.Collections;

public class SpawnerLogic : MonoBehaviour {
    private static int enemiesToSpawn = 20;
    private static float timeToSpawn = .25f;
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
    void Start () {
        StartCoroutine("Spawn");
	}

    IEnumerator Spawn() {
        while(enemiesSpawned != enemiesToSpawn)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity) as GameObject;
            ++GameControl.EnemiesSpawnedThisLevel;
            enemy.name = "Enemy #" + GameControl.EnemiesSpawnedThisLevel;
            ++enemiesSpawned;
            yield return new WaitForSeconds(timeToSpawn);
        }
    }
	
	// Update is called once per frame
	void Update () {
	    
	}
}
