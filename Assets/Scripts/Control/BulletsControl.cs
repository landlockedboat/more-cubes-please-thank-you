using UnityEngine;
using System.Collections;

public class BulletsControl : MonoBehaviour {
    [SerializeField]
    float speed = 20;
    [SerializeField]
    int shootThroughEnemies = 1;

    static BulletsControl bulletsControl;

    public static BulletsControl instance
    {
        get
        {
            if (!bulletsControl)
            {
                bulletsControl = FindObjectOfType<BulletsControl>();
                if (!bulletsControl)
                {
                    Debug.LogError("There needs to be one active BulletsControl script on a GameObject in your scene.");
                }
                else
                {
                    bulletsControl.Init();
                }
            }

            return bulletsControl;
        }
    }

    public static float Speed
    {
        get
        {
            return instance.speed;
        }

        set
        {
            instance.speed = value;
        }
    }

    public static int ShootThroughEnemies
    {
        get
        {
            return instance.shootThroughEnemies;
        }

        set
        {
            instance.shootThroughEnemies = value;
        }
    }

    void Init()
    {

    }




}
