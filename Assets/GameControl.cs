using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {
    private static int enemiesSpawnedThisLevel  = 0;

    public static int EnemiesSpawnedThisLevel
    {
        get
        {
            return enemiesSpawnedThisLevel;
        }

        set
        {
            enemiesSpawnedThisLevel = value;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
