using UnityEngine;
using System.Collections;

public class MapBoundariesControl : MonoBehaviour {
    static MapBoundariesControl mapBoundariesControl;
    [SerializeField]
    Transform rightBoundary;
    [SerializeField]
    Transform topBoundary;

    public static MapBoundariesControl instance
    {
        get
        {
            if (!mapBoundariesControl)
            {
                mapBoundariesControl = FindObjectOfType<MapBoundariesControl>();
                if (!mapBoundariesControl)
                {
                    Debug.LogError("There needs to be one active MapBoundariesControl script on a GameObject in your scene.");
                }
                else
                {
                    mapBoundariesControl.Init();
                }
            }

            return mapBoundariesControl;
        }
    }

    void Init()
    {

    }

    public static float Width {
        get
        {
            return instance.rightBoundary.position.x * 2;
        }
    }

    public static float Height
    {
        get
        {
            return instance.topBoundary.position.z * 2;
        }
    }
}
