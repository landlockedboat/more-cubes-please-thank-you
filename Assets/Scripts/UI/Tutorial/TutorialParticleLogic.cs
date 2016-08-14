using UnityEngine;
using System.Collections;

public class TutorialParticleLogic : MonoBehaviour {

    [Header("Shrinking stuff")]
    [SerializeField]
    int ticksToDie = 200;
    [SerializeField]
    int deltaTicksToDie = 100;
    [SerializeField]
    float shrinkingRate = 1f;
    [SerializeField]
    Vector3 startingScale;

    int randomisedTicksToDie;

    bool shrinking = false;

    void Start()
    {
        shrinking = false;
        randomisedTicksToDie = ticksToDie + Random.Range(-deltaTicksToDie / 2, deltaTicksToDie);
        transform.localScale = startingScale;
        StartCoroutine("Shrink");
    }



    IEnumerator Shrink()
    {
        while (true)
        {
            if (!shrinking)
            {
                --randomisedTicksToDie;
                if (randomisedTicksToDie <= 0)
                    shrinking = true;
            }
            else
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, shrinkingRate * Time.deltaTime);
                if (transform.localScale.z <= 0)
                {
                    Destroy(gameObject);
                }
            }
            yield return new WaitForEndOfFrame();
        }
        
    }
}
