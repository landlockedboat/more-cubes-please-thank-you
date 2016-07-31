using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MuzzleLogic : MonoBehaviour {

    [Header("Shooting")]
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    GameObject missilePrefab;
    [SerializeField]
    GameObject muzzlePrefab;

    [Header("Multishot")]
    [SerializeField]
    float angleBetweenMuzzles = 20f;
    [SerializeField]
    float muzzleRadius = 2f;

    Dictionary<Transform, Quaternion> muzzles;
    int currentMuzzles = 0;

    [Header("Bullet Sound")]
    [SerializeField]
    float minBulletVol = .5f;
    [SerializeField]
    float maxBulletVol = 1f;
    [SerializeField]
    float minBulletPitch = .75f;
    [SerializeField]
    float maxBulletPitch = 1.5f;
    [SerializeField]
    AudioClip bulletShootingSound;

    [Header("Missile Sound")]
    [SerializeField]
    float minMissileVol = .5f;
    [SerializeField]
    float maxMissileVol = 1f;
    [SerializeField]
    float minMissilePitch = .75f;
    [SerializeField]
    float maxMissilePitch = 1.5f;
    [SerializeField]
    AudioClip missileShootingSound;

    AudioSource audioSource;

    public int CurrentMuzzles
    {
        get
        {
            return currentMuzzles;
        }
    }

    void Awake()
    {
        muzzles = new Dictionary<Transform, Quaternion>();
        audioSource = GetComponent<AudioSource>();
    }

    public void AddMuzzle() {
        ++currentMuzzles;
        float angle = angleBetweenMuzzles * (currentMuzzles / 2);
        if (currentMuzzles % 2 == 0)
        {
            angle *= -1;
            transform.localRotation = Quaternion.Euler(0, angleBetweenMuzzles / 2, 0); 
        }
        else
        {
            transform.localRotation = Quaternion.identity;
        }
        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        GameObject newMuzzle = 
        Instantiate(muzzlePrefab,
            rotation * new Vector3(0, 0, muzzleRadius),
            Quaternion.identity) as GameObject;
        newMuzzle.transform.SetParent(transform, false);
        muzzles.Add(newMuzzle.transform, rotation);
    }  

    public void Shoot()
    {
        foreach (KeyValuePair<Transform, Quaternion> muzzleTrans in muzzles)
        {
            Instantiate(bulletPrefab, muzzleTrans.Key.position, muzzleTrans.Value * muzzleTrans.Key.parent.localRotation * muzzleTrans.Key.parent.parent.localRotation);
        }
        float vol = Random.Range(minBulletVol, maxBulletVol);
        float pitch = Random.Range(minBulletPitch, maxBulletPitch);
        audioSource.pitch = pitch;
        audioSource.PlayOneShot(bulletShootingSound, vol);
    }

    public void ShootMissile()
    {
        Instantiate(missilePrefab, transform.position, transform.parent.localRotation);
        float vol = Random.Range(minMissileVol, maxMissileVol);
        float pitch = Random.Range(minMissilePitch, maxMissilePitch);
        audioSource.pitch = pitch;
        audioSource.PlayOneShot(missileShootingSound, vol);
    }


}
