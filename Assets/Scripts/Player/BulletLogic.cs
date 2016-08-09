using UnityEngine;
using System.Collections;

public class BulletLogic : MonoBehaviour {

    int currentShootThroughEnemies;
    int shootThroughEnemies;
    float speed;

    void Start()
    {
        shootThroughEnemies = PlayerShooting.ShootThroughEnemies;
        currentShootThroughEnemies = shootThroughEnemies;
        Init();
    }

    protected void Init()
    {
        speed = PlayerShooting.Speed;
    }


    void Update () {
        Move();
	}

    protected virtual void Move()
    {
        transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
    }

    public void EnemyHit()
    {        
        if(--currentShootThroughEnemies <= 0)
        {
            Destroy(gameObject);
        }
    }
}
