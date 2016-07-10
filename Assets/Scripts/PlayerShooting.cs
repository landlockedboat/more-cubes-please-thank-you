using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {
    Transform playerGeom;
    [SerializeField]
    float cooldownTime = .1f;
    [SerializeField]
    int enemiesTillNextMissile;
    [SerializeField]
    int maxMissiles = 1;
    int currentMissiles = 0;
    float currentTime;
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    GameObject missilePrefab;
    Transform muzzle;

    private static PlayerShooting playerShooting;

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
        muzzle = transform.GetChild(0).transform.GetChild(0);
        currentTime = cooldownTime;
        currentMissiles = maxMissiles;
        //We do this to trigger the UI inisialisation.
        MaxMissiles = maxMissiles;
        CurrentMissiles = currentMissiles;
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
            instance.currentMissiles = value;
            MissilesPanelUI.CurrentMissiles = instance.currentMissiles;
        }
    } 

    void Update () {
        Vector3 mouse = Input.mousePosition;
        mouse.z = Camera.main.transform.position.y - instance.playerGeom.position.y;
        playerGeom.LookAt(Camera.main.ScreenToWorldPoint(mouse));
        if (Input.GetMouseButton(0) && currentTime <= 0)
        {
            currentTime = cooldownTime;
            Instantiate(instance.bulletPrefab, instance.muzzle.transform.position, instance.playerGeom.transform.localRotation);
        }
        if (currentTime > 0)
            currentTime -= Time.deltaTime;
        if(Input.GetMouseButtonDown(1) && currentMissiles > 0)
        {
            --CurrentMissiles;
            Instantiate(instance.missilePrefab, instance.muzzle.transform.position, instance.playerGeom.transform.localRotation);
        }
    }
}
