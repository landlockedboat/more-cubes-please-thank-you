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
    [SerializeField]
    GameObject explosionSoundPrefab;
    [SerializeField]
    AudioClip explosionAudioClip;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            InstantiateMissileParticles();
            GameObject soundPlayer =
            Instantiate(explosionSoundPrefab, transform.position, Quaternion.identity) as GameObject;
            soundPlayer.GetComponent<SoundPlayerLogic>().AudioClip = explosionAudioClip;
            foreach (Collider c in Physics.OverlapSphere(transform.position, explosionRadius))
            {
                if (c.tag == "Enemy")
                    c.GetComponent<EnemyDeath>().Kill(transform.position, true);
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
                    if (Random.Range(0f, 1f) < OptimisationControl.ParticleSpawnChance())
                    {
                        GameObject point =
                        SimplePool.Spawn(missileParticlePrefab,
                        transform.position + new Vector3(i * .5f, j * .5f, k * .5f),
                        transform.localRotation);
                        point.GetComponent<Rigidbody>().AddExplosionForce(particleExplosionForce, transform.position, explosionRadius);
                    }
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
                SimplePool.Spawn(missileParticlePrefab, transform.position, Quaternion.identity);
            particle.GetComponent<Rigidbody>().AddForce(
            transform.localRotation * new Vector3(Random.Range(-particleForce, particleForce), 0));
            yield return new WaitForSeconds(timeBetweenParticles);
        }
    }
}
