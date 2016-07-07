using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {

    private float cameraHeight;
    private Transform playerGeom;
    private static float cooldownTime = .1f;
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

    void Start() {
        playerGeom = transform.GetChild(0);
        cameraHeight = Camera.main.transform.position.y - playerGeom.position.y;
        muzzle = transform.GetChild(0).transform.GetChild(0);
        currentTime = cooldownTime;
    }

    void Update () {
        Vector3 mouse = Input.mousePosition;
        mouse.z = cameraHeight;
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
