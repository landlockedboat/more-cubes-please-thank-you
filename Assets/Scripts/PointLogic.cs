using UnityEngine;
using System.Collections;

public class PointLogic : MonoBehaviour
{
    private static float timeToDie = 2f;
    private static float deltaTimeToDie = 1f;
    private float randomisedTimeToDie;    
    private static float shrinkingRate = 1f;

    private new Rigidbody rigidbody;
    private static float explosionForce = 500f;
    private static float explosionRadius = 10f;

    // Use this for initialization
    public void Init(Vector3 explosionPos, bool isMissile)
    {
        randomisedTimeToDie = timeToDie + deltaTimeToDie * Random.Range(-1, 1);
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddExplosionForce(
            (isMissile ? explosionForce * 2: explosionForce), explosionPos, explosionRadius
            );
        StartCoroutine("Die");
    }

    // Update is called once per frame

    IEnumerator Die()
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
        --OptimisationControl.CurrentPointsInScene;
        Destroy(gameObject);
    }
}
