using UnityEngine;
using System.Collections;

public class ParticleLogic : MonoBehaviour
{

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

    void InitVars()
    {
        shrinking = false;
        randomisedTicksToDie = ticksToDie + Random.Range(-deltaTicksToDie / 2, deltaTicksToDie);
        transform.localScale = startingScale;
    }

    void OnEnable()
    {
        InitVars();
        EventManager.StartListening(EventManager.EventType.OnParticleClock, ShrinkABit);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.EventType.OnParticleClock, ShrinkABit);
    }

    void ShrinkABit()
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
                --OptimisationControl.CurrentParticlesInscene;
                SimplePool.Despawn(gameObject);
            }
        }
    }
}
