using UnityEngine;
using System.Collections;

public class BulletLogic : MonoBehaviour {
    private static float speed = 20;
    private static int shootThroughEnemies = 1;
    private int currentShootThroughEnemies;
    private bool killFlag = false;

    public static float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    public static int ShootThroughEnemies
    {
        get
        {
            return shootThroughEnemies;
        }

        set
        {
            shootThroughEnemies = value;
        }
    }

    void Start()
    {
        currentShootThroughEnemies = shootThroughEnemies;
    }

    void OnEnable() {
        EventManager.StartListening(EventManager.EventType.OnBulletKill, Destroy);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.EventType.OnBulletKill, Destroy);
    }

    void Update () {
        transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
	}

    public void EnemyHit()
    {        
        if(--currentShootThroughEnemies <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Destroy() {
        if (killFlag)
            Destroy(gameObject);
        else
            killFlag = true;
    }
}
