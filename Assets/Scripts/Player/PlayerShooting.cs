using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    Transform playerGeom;

    [Header("Bullets") ]
    [SerializeField]
    float cooldownTime = .1f;
    [SerializeField]
    float bulletSpeed = 20;
    [SerializeField]
    int shootThroughEnemies = 1;
    [SerializeField]
    int currentBulletsShot = 1;
    [SerializeField]
    MuzzleLogic muzzleLogic;

    [Header("Missiles")]
    [SerializeField]
    int enemiesTillNextMissile;
    int currentEnemiesTillNextMissile;
    [SerializeField]
    int maxMissiles = 1;

    int currentMissiles = 0;
    float currentTime;
    bool canShootMissiles = true;
    bool isPaused = false;

    static PlayerShooting playerShooting;

    public static PlayerShooting instance
    {
        get
        {
            if (!playerShooting)
            {
                playerShooting = FindObjectOfType<PlayerShooting>();
                if (!playerShooting)
                {
                    Debug.LogError("There needs to be one active PlayerShooting script on a GameObject in your scene.");
                }
                else
                {
                    playerShooting.Init();
                }
            }

            return playerShooting;
        }
    }

    void Init()
    {
        playerGeom = transform.GetChild(0);
        currentTime = cooldownTime;
        currentMissiles = maxMissiles;
        currentEnemiesTillNextMissile = enemiesTillNextMissile;        
        UpdateMuzzles();

        //We do this to trigger the UI inisialisation.
        MaxMissiles = maxMissiles;
        CurrentMissiles = currentMissiles;
        EnemiesTillNextMissile = enemiesTillNextMissile;
        CurrentEnemiesTillNextMissile = 0;
    }

    void UpdateMuzzles() {
        int currentMuzzles = muzzleLogic.CurrentMuzzles;
        for (int i = 0; i < currentBulletsShot - currentMuzzles; i++)
        {
            muzzleLogic.AddMuzzle();
        }
    }

    public static float CooldownTime
    {
        get
        {
            return instance.cooldownTime;
        }

        set
        {
            instance.cooldownTime = value;
        }
    }

    public static int MaxMissiles
    {
        get
        {
            return instance.maxMissiles;
        }

        set
        {
            instance.maxMissiles = value;
            MissilesPanelUI.MaxMissiles = instance.maxMissiles;
            CurrentMissiles = instance.maxMissiles;
            instance.currentEnemiesTillNextMissile = EnemiesTillNextMissile;
        }
    }

    public static int CurrentMissiles
    {
        get
        {
            return instance.currentMissiles;
        }

        set
        {
            if (instance.currentMissiles < value)
            {
                instance.currentEnemiesTillNextMissile = instance.enemiesTillNextMissile;
            }

                if (value > instance.maxMissiles)
            {
                instance.currentMissiles = instance.maxMissiles;
            }
            else
            {
                instance.currentMissiles = value;
                MissilesPanelUI.CurrentMissiles = instance.currentMissiles;
            }
        }
    }

    public static int EnemiesTillNextMissile
    {
        get
        {
            return instance.enemiesTillNextMissile;
        }

        set
        {
            instance.enemiesTillNextMissile = value;
            if (instance.currentEnemiesTillNextMissile > instance.enemiesTillNextMissile)
            {
                instance.CurrentEnemiesTillNextMissile = instance.enemiesTillNextMissile;
            }
            MissilesPanelUI.EnemiesTillNextMissile = instance.enemiesTillNextMissile;
        }
    }

    int CurrentEnemiesTillNextMissile
    {
        get
        {
            return currentEnemiesTillNextMissile;
        }
        set
        {
            currentEnemiesTillNextMissile = value;
            MissilesPanelUI.CurrentEnemiesTillNextMissile = currentEnemiesTillNextMissile;
        }
    }

    public static bool CanShootMissiles
    {
        set
        {
            instance.canShootMissiles = value;
        }
    }

    public static float Speed
    {
        get
        {
            return instance.bulletSpeed;
        }

        set
        {
            instance.bulletSpeed = value;
        }
    }

    public static int ShootThroughEnemies
    {
        get
        {
            return instance.shootThroughEnemies;
        }

        set
        {
            instance.shootThroughEnemies = value;
        }
    }

    public static int CurrentBulletsShot
    {
        get
        {
            return instance.currentBulletsShot;
        }

        set
        {
            int diff = value - instance.currentBulletsShot; 
            instance.currentBulletsShot = value;
            for (int i = 0; i < diff; i++)
            {
                instance.muzzleLogic.AddMuzzle();
            }            
        }
    }

    void OnEnable()
    {
        EventManager.StartListening(EventManager.EventType.OnEnemyKilled, OnEnemyKilled);
        EventManager.StartListening(EventManager.EventType.OnUpgradesShown, OnUpgradesShown);
        EventManager.StartListening(EventManager.EventType.OnGamePaused, OnGamePaused);
        EventManager.StartListening(EventManager.EventType.OnGameResumed, OnGameResumed);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.EventType.OnEnemyKilled, OnEnemyKilled);
        EventManager.StopListening(EventManager.EventType.OnUpgradesShown, OnUpgradesShown);
        EventManager.StopListening(EventManager.EventType.OnGamePaused, OnGamePaused);
        EventManager.StopListening(EventManager.EventType.OnGameResumed, OnGameResumed);
    }

    void OnGamePaused()
    {
        instance.isPaused = true;
    }

    void OnGameResumed()
    {
        instance.isPaused = false;
    }

    void OnUpgradesShown()
    {
        CurrentMissiles = MaxMissiles;
    }

    void OnEnemyKilled()
    {
        if (CurrentMissiles < MaxMissiles)
        {
            --CurrentEnemiesTillNextMissile;
            if (currentEnemiesTillNextMissile <= 0)
            {
                ++CurrentMissiles;
                CurrentEnemiesTillNextMissile = EnemiesTillNextMissile;
            }
        }        
    }

    void Update()
    {
        if (isPaused)
            return;
        Vector3 mouse = Input.mousePosition;
        mouse.z = Camera.main.transform.position.y - instance.playerGeom.position.y;
        playerGeom.LookAt(Camera.main.ScreenToWorldPoint(mouse));
        if (Input.GetMouseButton(0) && currentTime <= 0)
        {
            currentTime = cooldownTime;
            muzzleLogic.Shoot();
            StatisticsControl.AddToStat(StatisticsControl.Stat.BulletsShot, 1);
            if (StatisticsControl.GetStat(StatisticsControl.Stat.BulletsShot) > 0)
            {
                StatisticsControl.SetStat(StatisticsControl.Stat.Accuracy,
                    Mathf.RoundToInt(
                        (
                    (float)(StatisticsControl.GetStat(StatisticsControl.Stat.EnemiesKilledByBullets)) /
                    (float)(StatisticsControl.GetStat(StatisticsControl.Stat.BulletsShot))
                    ) * 100f
                    ));
            }
        }
        if (currentTime > 0)
            currentTime -= Time.deltaTime;
        if (Input.GetMouseButtonDown(1) && currentMissiles > 0)
        {
            if (canShootMissiles)
            {
                --CurrentMissiles;
                muzzleLogic.ShootMissile();
                StatisticsControl.AddToStat(StatisticsControl.Stat.MissilesShot, 1);
                CurrentEnemiesTillNextMissile = EnemiesTillNextMissile;
            }
        }
    }
}
