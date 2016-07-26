using UnityEngine;
using System.Collections;

public class EnemyLogic : MonoBehaviour {
    [SerializeField]
    NavMeshAgent navMeshAgent;
    Vector3 prevPlayerPos;
    bool isGameOver = false;
    float damage = 5f;
    [SerializeField]
    GameObject pointPrefab;

    [Header("Sound")]
    [SerializeField]
    GameObject soundPlayerPrefab;
    [SerializeField]
    AudioClip deathSound;
    [Range(0f, 1f)]
    [SerializeField]
    float volume = .25f;

    [Space(10)]
    [SerializeField]
    MeshRenderer meshRenderer;
    bool isKilledBecauseEndOfLevel = false;
    [SerializeField]
    int scoreWorth = 5;
    bool omaeWaMouShindeiru = false;

    public void Init (Color color, float damage, float speed) {
        this.damage = damage;
        meshRenderer.material.color = color;
        navMeshAgent.speed = speed;
        //hardcoded because i can
        navMeshAgent.acceleration = speed / 1.1f;
        prevPlayerPos = PlayerMovement.Pos;
        navMeshAgent.SetDestination(prevPlayerPos);
        StartCoroutine("Grow");
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
            StatisticsControl.AddToStat(StatisticsControl.Stat.EnemiesKilledByBullets, 1);
            if (StatisticsControl.GetStat(StatisticsControl.Stat.BulletsShot) > 0)
            {
                StatisticsControl.SetStat(StatisticsControl.Stat.Accuracy,
                    Mathf.RoundToInt(
                        (
                    (float)(StatisticsControl.GetStat(StatisticsControl.Stat.EnemiesKilledByBullets)) /
                    (float)(StatisticsControl.GetStat(StatisticsControl.Stat.BulletsShot))
                    ) * 100f
                    ));
            }
            Kill(col.transform.position, false);
        }
        if(col.gameObject.tag == "Player")
        {
            PlayerHealth.CurrentHealth -= damage;
            Kill(col.transform.position, false);
        }

    }

    void Kill() {
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
                    if (Random.Range(0f,1f) < OptimisationControl.ParticleSpawnChance())
                    {
                        ++OptimisationControl.CurrentParticlesInscene;
                        GameObject point = 
                            SimplePool.Spawn(pointPrefab,
                            transform.position + new Vector3(i * .5f, j * .5f, k * .5f),
                            transform.localRotation);                        
                        point.GetComponent<EnemyParticleLogic>().StartExplosion(explosionPos, isMissile);
                        point.GetComponent<MeshRenderer>().material.color = thisColor;
                    }

                }
            }
                            
        }
        if (!isMissile && !omaeWaMouShindeiru)        
            PlayDeadSound();
        
        if (!isKilledBecauseEndOfLevel && !omaeWaMouShindeiru)
        {
            omaeWaMouShindeiru = true;
            EventManager.TriggerEvent(EventManager.EventType.OnEnemyKilled);
        }
        if (isMissile)
        {
            StatisticsControl.AddToStat(StatisticsControl.Stat.EnemiesKilledByMissile, 1);
        }

        Destroy(gameObject);
    }

    void PlayDeadSound()
    {
        GameObject soundPlayer =
        Instantiate(soundPlayerPrefab, transform.position, Quaternion.identity) as GameObject;
        SoundPlayerLogic spl = soundPlayer.GetComponent<SoundPlayerLogic>();
        spl.AudioClip = deathSound;
        spl.Volume = volume;        
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

    IEnumerator Grow() {
        float OGScale = transform.localScale.x;
        transform.localScale = new Vector3(.01f, .01f, .01f);
        float growSpeed = SpawnerControl.EnemyGrowingSpeed;
        bool finished = false;
        while (!finished)
        {
            float growDelta = growSpeed * Time.deltaTime;
            transform.localScale += new Vector3(growDelta, growDelta, growDelta);
            if (transform.localScale.x >= OGScale)
                finished = true;
            yield return null;
        }
    }
}
