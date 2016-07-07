using UnityEngine;
using System.Collections;

public class EnemyLogic : MonoBehaviour {
    private NavMeshAgent navMeshAgent;
    private Vector3 prevPlayerPos;
    private bool isGameOver = false;
    private float damage = 5f;
	// Use this for initialization
	void Start () {
        prevPlayerPos = PlayerMovement.Pos;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(prevPlayerPos);
    }

    // Update is called once per frame
    void Update () {
        if (isGameOver)
            return;
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
            Kill();
        }
        if(col.gameObject.tag == "Player")
        {
            col.GetComponent<PlayerHealth>().Hurt(damage);
            Kill();
        }

    }

    void Kill() {
        ++GameControl.EnemiesKilledThisLevel;
        Destroy(gameObject);
    }

    void OnEnable()
    {
        EventManager.StartListening(EventManager.EventType.OnGameOver, OnGameOver);
    }

    void OnDisable() {
        EventManager.StopListening(EventManager.EventType.OnGameOver, OnGameOver);
    }

    void OnGameOver() {
        isGameOver = true;
    }
}
