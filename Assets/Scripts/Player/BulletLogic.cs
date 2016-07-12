using UnityEngine;
using System.Collections;

public class BulletLogic : MonoBehaviour {

    int currentShootThroughEnemies;
    int shootThroughEnemies;
    float speed;
    bool killFlag = false;  

    void Start()
    {
        shootThroughEnemies = PlayerShooting.ShootThroughEnemies;
        currentShootThroughEnemies = shootThroughEnemies;

        speed = PlayerShooting.Speed;
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

    void Destroy() {
        if (killFlag)
            Destroy(gameObject);
        else
            killFlag = true;
    }
}
