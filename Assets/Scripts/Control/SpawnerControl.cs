using UnityEngine;
using System.Collections;

public class SpawnerControl : MonoBehaviour {
    [SerializeField]
    int enemiesToSpawn = 20;
    [SerializeField]
    float timeBetweenSpawns = .25f;

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
        get
        {
            return instance.timeBetweenSpawns;
        }

        set
        {
            instance.timeBetweenSpawns = value;
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
}
