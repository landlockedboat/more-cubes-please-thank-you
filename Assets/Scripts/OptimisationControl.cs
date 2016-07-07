using UnityEngine;
using System.Collections;

public class OptimisationControl : MonoBehaviour {
    [SerializeField]
    private float timeToKillBullets = 2f;
    private float currentTimeToKillBullets;
    private static float maxPointsInScene = 200;
    private static float currentPointsInScene = 0;

    public static float MaxPointsInScene
    {
        get
        {
            return maxPointsInScene;
        }
    }

    public static float CurrentPointsInScene
    {
        get
        {
            return currentPointsInScene;
        }

        set
        {
            currentPointsInScene = value;
        }
    }

    // Use this for initialization
    void Start () {
        currentTimeToKillBullets = timeToKillBullets;
    }
	
	// Update is called once per frame
	void Update () {
	    if(currentTimeToKillBullets <= 0)
        {
            currentTimeToKillBullets = timeToKillBullets;
            EventManager.TriggerEvent(EventManager.EventType.OnBulletKill);
        }
        currentTimeToKillBullets -= Time.deltaTime;
	}
}
