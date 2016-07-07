using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {

    private float cameraHeight;
    private Transform playerGeom;
    [SerializeField]
    GameObject bulletPrefab;
    Transform muzzle;

    void Start() {
        playerGeom = transform.GetChild(0);
        cameraHeight = Camera.main.transform.position.y - playerGeom.position.y;
        muzzle = transform.GetChild(0).transform.GetChild(0);
    }

    void Update () {
        Vector3 mouse = Input.mousePosition;
        mouse.z = cameraHeight;
        playerGeom.LookAt(Camera.main.ScreenToWorldPoint(mouse));
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletPrefab, muzzle.transform.position, playerGeom.transform.localRotation);
        }
    }
}
