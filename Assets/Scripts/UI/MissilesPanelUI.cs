using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class MissilesPanelUI : MonoBehaviour
{
    int maxMissiles;
    int currentMissiles;
    int currentEnemiesTillNextMissile;
    int enemiesTillNextMissile;
    List<Image> missilesIconsUI;
    RectTransform thisRectTransform;
    [SerializeField]
    GameObject missileUIPrefab;

    static MissilesPanelUI missilesPanelUI;

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
            if (instance.currentMissiles <= value)
            {
                //We have more available missiles
                if (instance.currentMissiles < instance.missilesIconsUI.Count)
                {
                    Image image =
                    instance.missilesIconsUI[instance.currentMissiles];
                    image.fillAmount = 1;
                }
            }
            else
            {
                if (instance.currentMissiles >= 1)
                {
                    float prevFill = 0;
                    if (instance.currentMissiles < instance.maxMissiles)
                    {
                        Image image =
                        instance.missilesIconsUI[instance.currentMissiles];
                        prevFill = image.fillAmount;
                        image.fillAmount = 0;
                    }
                    instance.missilesIconsUI[instance.currentMissiles - 1].fillAmount = prevFill;
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
                instance.thisRectTransform.anchoredPosition = new Vector3(-25, 0, 0);
            }
            else
            {
                instance.thisRectTransform.anchoredPosition = Vector3.zero;
            }
            missileUI.transform.SetParent(instance.transform, false);
            instance.missilesIconsUI.Add(missileUI.transform.GetChild(1).GetComponent<Image>());
            for (int i = 0; i < instance.maxMissiles; i++)
            {
                CurrentMissiles = i;
            }
        }
    }

    public static int CurrentEnemiesTillNextMissile
    {
        set
        {
            instance.currentEnemiesTillNextMissile = value;
            if (instance.currentMissiles < instance.maxMissiles)
            {
                Image image =
                instance.missilesIconsUI[instance.currentMissiles];
                image.fillAmount = 1 - ((float)instance.currentEnemiesTillNextMissile / instance.enemiesTillNextMissile);
            }
        }
    }

    public static int EnemiesTillNextMissile
    {
        set
        {
            instance.enemiesTillNextMissile = value;
        }
    }
}
