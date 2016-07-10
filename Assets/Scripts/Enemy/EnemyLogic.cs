using UnityEngine;
using System.Collections;

public class EnemyLogic : MonoBehaviour {
    [SerializeField]
    private NavMeshAgent navMeshAgent;
    private Vector3 prevPlayerPos;
    private bool isGameOver = false;
    private float damage = 5f;
    [SerializeField]
    GameObject pointPrefab;
    [SerializeField]
    MeshRenderer meshRenderer;
    bool isKilledBecauseEndOfLevel = false;
    private int scoreWorth = 5;
	
	public void Init (Color color, float damage, float speed) {
        this.damage = damage;
        meshRenderer.material.color = color;
        navMeshAgent.speed = speed;
        //hardcoded because i can
        navMeshAgent.acceleration = speed / 1.1f;
        prevPlayerPos = PlayerMovement.Pos;
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
            col.SendMessage("EnemyHit");
            Kill(col.transform.position, false);
        }
        if(col.gameObject.tag == "Player")
        {
            PlayerHealth.Hurt(damage);
            Kill(col.transform.position, false);
        }

    }

    private void Kill() {
        isKilledBecauseEndOfLevel = true;
        Kill(transform.position, false);
    }

    public void Kill(Vector3 explosionPos, bool isMissile) {
        ScoreControl.CurrentScore += scoreWorth;
        Color thisColor = transform.GetChild(0).GetComponent<MeshRenderer>().material.color;
        //hardcoded because i can
        for (int i = -1; i < 2; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = -1; k < 2; k++)
                {
                    if (OptimisationControl.CurrentParticlesInscene < OptimisationControl.MaxParticlesInScene)
                    {
                        ++OptimisationControl.CurrentParticlesInscene;
                        GameObject point =
                    Instantiate(pointPrefab,
                        transform.position + new Vector3(i * .5f, j * .5f, k * .5f),
                        transform.localRotation) as GameObject;
                        point.GetComponent<PointLogic>().Init(explosionPos, isMissile);
                        point.GetComponent<MeshRenderer>().material.color = thisColor;
                    }

                }
            }
                            
        }
        if (!isKilledBecauseEndOfLevel)
        {
            EventManager.TriggerEvent(EventManager.EventType.OnEnemyKilled);
        }
        Destroy(gameObject);
    }

    void OnEnable()
    {
        EventManager.StartListening(EventManager.EventType.OnGameOver, OnGameOver);
        EventManager.StartListening(EventManager.EventType.OnLevelChanged, Kill);
    }

    void OnDisable() {
        EventManager.StopListening(EventManager.EventType.OnGameOver, OnGameOver);
        EventManager.StopListening(EventManager.EventType.OnLevelChanged, Kill);

    }

    void OnGameOver() {
        isGameOver = true;
    }
}
