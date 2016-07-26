using UnityEngine;
using System.Collections;

public class OptimisationControl : MonoBehaviour {
    [SerializeField]
    float bulletLifespan = 2f;
    float currentTimeToKillBullets;
    [SerializeField]
    int maxParticlesInScene = 600;

    int currentParticlesInscene = 0;

    static OptimisationControl optimisationControl;

    public static OptimisationControl instance
    {
        get
        {
            if (!optimisationControl)
            {
                optimisationControl = FindObjectOfType<OptimisationControl>();
                if (!optimisationControl)
                {
                    Debug.LogError("There needs to be one active OptimisationControl script on a GameObject in your scene.");
                }
                else
                {
                    optimisationControl.Init();
                }
            }

            return optimisationControl;
        }
    }

    void Init()
    {
    }

    public static int CurrentParticlesInscene
    {
        get
        {
            return instance.currentParticlesInscene;
        }

        set
        {
            instance.currentParticlesInscene = value;
        }
    }

    public static int MaxParticlesInScene
    {
        get
        {
            return instance.maxParticlesInScene;
        }
    }

    public static float ParticleSpawnChance () {
        float ret = 
        1 - (float)instance.currentParticlesInscene / (float)instance.maxParticlesInScene;
        return ret;
    }

    // Use this for initialization
    void Start () {
        currentTimeToKillBullets = bulletLifespan;
    }
	
	// Update is called once per frame
	void Update () {
	    if(currentTimeToKillBullets <= 0)
        {
            currentTimeToKillBullets = bulletLifespan;
            EventManager.TriggerEvent(EventManager.EventType.OnBulletKill);
        }
        EventManager.TriggerEvent(EventManager.EventType.OnParticleClock);
        currentTimeToKillBullets -= Time.deltaTime;
	}
}
