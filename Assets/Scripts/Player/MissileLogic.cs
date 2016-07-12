using UnityEngine;
using System.Collections;

public class MissileLogic : BulletLogic
{
    [SerializeField]
    float explosionRadius;
    [Header("Particles")]
    [SerializeField]
    float timeBetweenParticles = .15f;
    [SerializeField]
    float particleForce = 50f;
    [SerializeField]
    float particleExplosionForce = 200f;
    [SerializeField]
    GameObject missileParticlePrefab;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            InstantiateMissileParticles();
            foreach (Collider c in Physics.OverlapSphere(transform.position, explosionRadius))
            {
                if (c.tag == "Enemy")
                    c.GetComponent<EnemyLogic>().Kill(transform.position, true);
            }
            Destroy(gameObject);
        }
    }

    void InstantiateMissileParticles()
    {
        for (int i = -1; i < 2; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = -1; k < 2; k++)
                {
                    GameObject point =
                Instantiate(missileParticlePrefab,
                    transform.position + new Vector3(i * .5f, j * .5f, k * .5f),
                    transform.localRotation) as GameObject;
                    point.GetComponent<Rigidbody>().AddExplosionForce(particleExplosionForce, transform.position, explosionRadius);

                }
            }

        }
    }

    void Start()
    {
        Init();
        StartCoroutine("SpawnParticles");
    }

    IEnumerator SpawnParticles()
    {
        while (true)
        {
            GameObject particle =
            Instantiate(missileParticlePrefab, transform.position, Quaternion.identity) as GameObject;
            particle.GetComponent<Rigidbody>().AddForce(
                transform.localRotation * new Vector3(Random.Range(-particleForce, particleForce), 0));
            yield return new WaitForSeconds(timeBetweenParticles);
        }
    }
}
