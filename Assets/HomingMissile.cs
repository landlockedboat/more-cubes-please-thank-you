using UnityEngine;
using System.Collections;
[RequireComponent(typeof(NavMeshAgent))]
public class HomingMissile : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Transform closestEnemy;
    [SerializeField]
    float enemyLostRecalcTime = .2f;
    [SerializeField]
    float timeTillStart = .2f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();        
        StartCoroutine("UpdateNavMeshAgent");
    }

    IEnumerator UpdateNavMeshAgent()
    {
        navMeshAgent.SetDestination(transform.localPosition + transform.rotation * new Vector3(0,0,20));
        yield return new WaitForSeconds(timeTillStart);
        closestEnemy = GetClosestEnemy().transform;
        while (true)
        {
            if(closestEnemy != null)
            {
                navMeshAgent.SetDestination(closestEnemy.position);
            }
            else
            {
                yield return new WaitForSeconds(enemyLostRecalcTime);
                closestEnemy = GetClosestEnemy();
            }
            yield return null;
        }
    }

    Transform GetClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
            }
        }

        return bestTarget;
    }
}
