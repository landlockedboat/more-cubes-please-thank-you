using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MuzzleLogic : MonoBehaviour {

    Dictionary<Transform, Quaternion> muzzles;
    int currentMuzzles = 0;
    [SerializeField]
    float angleBetweenMuzzles = 20f;
    [SerializeField]
    float muzzleRadius = 2f;
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    GameObject muzzlePrefab;

    public int CurrentMuzzles
    {
        get
        {
            return currentMuzzles;
        }
    }

    void Start()
    {
        muzzles = new Dictionary<Transform, Quaternion>();
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
        foreach (KeyValuePair<Transform, Quaternion> mt in muzzles)
        {
            Instantiate(bulletPrefab, mt.Key.position, mt.Value * mt.Key.parent.localRotation * mt.Key.parent.parent.localRotation);
        }
    }
}
