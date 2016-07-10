using UnityEngine;
using System.Collections;

public class OptimisationControl : MonoBehaviour {
    [SerializeField]
    private float bulletLifespan = 2f;
    private float currentTimeToKillBullets;
    [SerializeField]
    private float maxParticlesInScene = 200;
    private float currentParticlesInscene = 0;

    private static OptimisationControl upgradeControl;

    public static OptimisationControl instance
    {
        get
        {
            if (!upgradeControl)
            {
                upgradeControl = FindObjectOfType<OptimisationControl>();
                if (!upgradeControl)
                {
                    Debug.LogError("There needs to be one active OptimisationControl script on a GameObject in your scene.");
                }
                else
                {
                    upgradeControl.Init();
                }
            }

            return upgradeControl;
        }
    }

    void Init()
    {

    }

    public static float MaxParticlesInScene
    {
        get
        {
            return instance.maxParticlesInScene;
        }
    }

    public static float CurrentParticlesInscene
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
        currentTimeToKillBullets -= Time.deltaTime;
	}
}
