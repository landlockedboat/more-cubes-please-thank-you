using UnityEngine;
using System.Collections;

public class EnemyParticleLogic : ParticleLogic
{
    [Header("Normal Explosion")]
    [SerializeField]
    float normalExplosionForce = 500f;
    [SerializeField]
    float normalExplosionRadius = 10f;

    [Header("Missile Explosion")]
    [SerializeField]
    float missileExplosionForce = 1000f;
    [SerializeField]
    float missileExplosionRadius = 20f;

    new Rigidbody rigidbody;

    protected void InitVars()
    {
        if (rigidbody == null)
            rigidbody = GetComponent<Rigidbody>();
    }

    public void StartExplosion(Vector3 explosionPos, bool isMissile)
    {
        InitVars();

        if (isMissile)
        {
            rigidbody.AddExplosionForce(missileExplosionForce, explosionPos, missileExplosionRadius);
        }
        else
        {
            rigidbody.AddExplosionForce(normalExplosionForce, explosionPos, normalExplosionRadius);
        }
    }    
}
