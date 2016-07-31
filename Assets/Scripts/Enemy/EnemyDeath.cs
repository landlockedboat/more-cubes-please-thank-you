using UnityEngine;
using System.Collections;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField]
    int scoreWorth = 5;
    [SerializeField]
    GameObject enemyFragmentPrefab;

    [Header("Dying sound")]
    [SerializeField]
    GameObject soundPlayerPrefab;
    [SerializeField]
    AudioClip deathSound;
    [Range(0f, 1f)]
    [SerializeField]
    float volume = .25f;

    bool isKilledBecauseEndOfLevel = false;
    float damage = 5f;
    bool alreadyDead = false;

    public void Init(float damage)
    {
        this.damage = damage;
    }

    void OnTriggerEnter(Collider col)
    {
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
        if (col.gameObject.tag == "Player")
        {
            PlayerHealth.CurrentHealth -= damage;
            Kill(col.transform.position, false);
        }
    }

    void Kill()
    {
        isKilledBecauseEndOfLevel = true;
        Kill(transform.position, false);
    }

    public void Kill(Vector3 explosionPos, bool isMissile)
    {
        ScoreControl.CurrentScore += scoreWorth;
        Color thisColor = transform.GetChild(0).GetComponent<MeshRenderer>().material.color;
        //hardcoded because i can
        for (int i = -1; i < 2; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = -1; k < 2; k++)
                {
                    if (Random.Range(0f, 1f) < OptimisationControl.ParticleSpawnChance())
                    {
                        ++OptimisationControl.CurrentParticlesInscene;
                        GameObject point =
                            SimplePool.Spawn(enemyFragmentPrefab,
                            transform.position + new Vector3(i * .5f, j * .5f, k * .5f),
                            transform.localRotation);
                        point.GetComponent<EnemyParticleLogic>().StartExplosion(explosionPos, isMissile);
                        point.GetComponent<MeshRenderer>().material.color = thisColor;
                    }

                }
            }

        }
        if (!isMissile && !alreadyDead)
            PlayDeadSound();

        if (!isKilledBecauseEndOfLevel && !alreadyDead)
        {
            alreadyDead = true;
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
        EventManager.StartListening(EventManager.EventType.OnLevelChanged, Kill);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.EventType.OnLevelChanged, Kill);
    }
}
