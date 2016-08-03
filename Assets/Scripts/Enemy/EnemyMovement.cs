using UnityEngine;
using System.Collections;
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Vector3 prevPlayerPos;
    bool isGameOver = false;

    public void Init(float speed)
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
        navMeshAgent.acceleration = speed / 1.1f;
        prevPlayerPos = PlayerMovement.Pos;
        navMeshAgent.SetDestination(prevPlayerPos);
    }

    void Update()
    {
        if (isGameOver)
            return;
        Vector3 playerPos = PlayerMovement.Pos;
        if (playerPos != prevPlayerPos && playerPos != Vector3.down)
        {
            navMeshAgent.SetDestination(playerPos);
            prevPlayerPos = playerPos;
        }
    }
    void OnEnable()
    {
        EventManager.StartListening(EventManager.EventType.OnGameOver, OnGameOver);
    }
    void OnDisable()
    {
        EventManager.StopListening(EventManager.EventType.OnGameOver, OnGameOver);
    }

    void OnGameOver()
    {
        isGameOver = true;
    }
}
