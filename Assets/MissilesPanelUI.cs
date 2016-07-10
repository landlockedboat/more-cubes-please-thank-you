using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class MissilesPanelUI : MonoBehaviour {
    int maxMissiles;
    int currentMissiles;
    private List<Image> missilesIconsUI;
    private RectTransform thisRectTransform;
    [SerializeField]
    private GameObject missileUIPrefab;

    private static MissilesPanelUI missilesPanelUI;

    public static MissilesPanelUI instance
    {
        get
        {
            if (!missilesPanelUI)
            {
                missilesPanelUI = FindObjectOfType<MissilesPanelUI>();
                if (!missilesPanelUI)
                {
                    Debug.LogError("There needs to be one active MissilesPanelUI script on a GameObject in your scene.");
                }
                else
                {
                    missilesPanelUI.Init();
                }
            }

            return missilesPanelUI;
        }
    }

    void Init()
    {
        thisRectTransform = gameObject.GetComponent<RectTransform>();
        missilesIconsUI = new List<Image>();
    }

    public static int CurrentMissiles
    {
        set
        {
            if(instance.currentMissiles <= value)
            {
                //We have more available missiles
                if(instance.currentMissiles < instance.missilesIconsUI.Count - 1)
                {
                    Image image =
                    instance.missilesIconsUI[instance.currentMissiles];
                    image.color = new Color(0, 0, 0, 1);
                }
            }
            else
            {
                if (instance.currentMissiles >= 1)
                {
                    Image image =
                    instance.missilesIconsUI[instance.currentMissiles - 1];
                    image.color = new Color(0, 0, 0, .1f);
                }
            }
            instance.currentMissiles = value;
        }
    }

    public static int MaxMissiles
    {
        set
        {
            instance.maxMissiles = value;
            bool isEven = instance.maxMissiles % 2 == 0;
            GameObject missileUI =
               Instantiate(instance.missileUIPrefab,
               new Vector3(instance.maxMissiles / 2 * 
               (isEven ? 50 : -50)
               , 0, 0)
               , Quaternion.identity) as GameObject;
            if (isEven)
            {
                instance.thisRectTransform.anchoredPosition = new Vector3(-50, 0, 0);
            }
            else
            {
                instance.thisRectTransform.anchoredPosition = Vector3.zero;
            }
            missileUI.transform.SetParent(instance.transform, false);
            instance.missilesIconsUI.Add(missileUI.GetComponent<Image>());
            for (int i = 0; i < instance.maxMissiles; i++)
            {
                CurrentMissiles = i;
            }
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
