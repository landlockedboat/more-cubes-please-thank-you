using UnityEngine;
using System.Collections;

public class OptimisationControl : MonoBehaviour {

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
    	
	void Update () {
        EventManager.TriggerEvent(EventManager.EventType.OnParticleClock);
	}
}
