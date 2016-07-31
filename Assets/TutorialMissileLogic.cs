using UnityEngine;
using System.Collections;

public class TutorialMissileLogic : TutorialBulletLogic {

    [Header("Particles")]
    [SerializeField]
    float timeBetweenParticles = .15f;
    [SerializeField]
    float particleForce = 50f;
    [SerializeField]
    GameObject missileParticlePrefab;

    new void Start()
    {
        base.Start();
        StartCoroutine("SpawnParticles");
    }

    IEnumerator SpawnParticles()
    {
        while (true)
        {
            GameObject particle =
                SimplePool.Spawn(missileParticlePrefab, transform.position, Quaternion.identity);
            particle.GetComponent<Rigidbody>().AddForce(
            transform.localRotation * new Vector3(Random.Range(-particleForce, particleForce), 0));
            yield return new WaitForSeconds(timeBetweenParticles);
        }
    }
}
