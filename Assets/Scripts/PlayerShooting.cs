using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {

    private Transform playerGeom;
    private static float cooldownTime = .1f;
    static int maxMissiles;
    static int currentMissiles = 0;
    private float currentTime;
    [SerializeField]
    GameObject bulletPrefab;
    Transform muzzle;

    public static float CooldownTime
    {
        get
        {
            return cooldownTime;
        }

        set
        {
            cooldownTime = value;
        }
    }

    public static int MaxMissiles
    {
        get
        {
            return maxMissiles;
        }

        set
        {
            maxMissiles = value;
        }
    }

    public static int CurrentMissiles
    {
        get
        {
            return currentMissiles;
        }

        set
        {
            currentMissiles = value;
        }
    }

    void Start() {
        playerGeom = transform.GetChild(0);
        muzzle = transform.GetChild(0).transform.GetChild(0);
        currentTime = cooldownTime;
    }

    void Update () {
        Vector3 mouse = Input.mousePosition;
        mouse.z = Camera.main.transform.position.y - playerGeom.position.y;
        playerGeom.LookAt(Camera.main.ScreenToWorldPoint(mouse));
        if (Input.GetMouseButton(0) && currentTime <= 0)
        {
            currentTime = cooldownTime;
            Instantiate(bulletPrefab, muzzle.transform.position, playerGeom.transform.localRotation);
        }
        if (currentTime > 0)
            currentTime -= Time.deltaTime;
    }
}
