using UnityEngine;
using System.Collections;

public class SpawnerControl : MonoBehaviour {
    [SerializeField]
    int enemiesToSpawn = 20;
    [SerializeField]
    float timeBetweenSpawns = .25f;
    [SerializeField]
    float timeToBeginSpawning = 1f;
    [SerializeField]
    float fadeSpeed = 6;
    [SerializeField]
    float enemyGrowingSpeed = 6f;

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

    public static float TimeBetweenSpawns
    {
        set
        {
            instance.timeToBeginSpawning = value;
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
}
