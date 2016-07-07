using UnityEngine;
using System.Collections;

public class EnemyLogic : MonoBehaviour {
    private NavMeshAgent navMeshAgent;
    private Vector3 prevPlayerPos;
	// Use this for initialization
	void Start () {
        prevPlayerPos = PlayerMovement.Pos;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(prevPlayerPos);
    }

    // Update is called once per frame
    void Update () {
        Vector3 playerPos = PlayerMovement.Pos;
        if (playerPos != prevPlayerPos)
        {
            navMeshAgent.SetDestination(playerPos);
            prevPlayerPos = playerPos;
        }
	}


    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Bullet")
        {
            Destroy(col.gameObject);
            Destroy(gameObject);            
        }
    }
}
