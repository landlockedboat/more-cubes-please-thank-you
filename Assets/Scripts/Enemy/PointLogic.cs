using UnityEngine;
using System.Collections;

public class PointLogic : MonoBehaviour
{
    static float timeToDie = 2f;
    static float deltaTimeToDie = 1f;
    float randomisedTimeToDie;    
    static float shrinkingRate = 1f;

    new Rigidbody rigidbody;
    static float explosionForce = 500f;
    static float explosionRadius = 10f;

    // Use this for initialization
    public void Init(Vector3 explosionPos, bool isMissile)
    {
        randomisedTimeToDie = timeToDie + deltaTimeToDie * Random.Range(-1, 1);
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddExplosionForce(
            (isMissile ? explosionForce * 3: explosionForce), explosionPos, explosionRadius
            );
        StartCoroutine("Die");
    }

    // Update is called once per frame

    protected IEnumerator Die()
    {
        yield return new WaitForSeconds(randomisedTimeToDie);
        bool ded = false;
        while (!ded)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, shrinkingRate * Time.deltaTime);
            if (transform.localScale.z <= 0)
                ded = true;
            yield return null;
        }
        Destroy();
    }   

    void Destroy()
    {
        --OptimisationControl.CurrentParticlesInscene;
        Destroy(gameObject);
    }
}
