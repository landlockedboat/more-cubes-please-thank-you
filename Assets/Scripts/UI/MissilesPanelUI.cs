using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class MissilesPanelUI : MonoBehaviour
{
    [SerializeField]
    Text missilesText;
    [SerializeField]
    RectTransform missileProgressImage;
    ShrinkAndMove sam;
    float progressImageOGWidth;
    int maxMissiles;
    int currentMissiles;
    int currentEnemiesTillNextMissile;
    int enemiesTillNextMissile = 1;

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
        progressImageOGWidth = missileProgressImage.sizeDelta.x;
        sam = GetComponent<ShrinkAndMove>();
    }

    public static int CurrentMissiles
    {
        set
        {
            if (instance.currentMissiles < value)
                instance.sam.Animate();
            instance.currentMissiles = value;
            UpdateMissilesUI();
        }
    }

    public static int MaxMissiles
    {
        set
        {
            instance.maxMissiles = value;
            instance.sam.Animate();
            UpdateMissilesUI();
        }
    }

    public static int CurrentEnemiesTillNextMissile
    {
        set
        {
            instance.currentEnemiesTillNextMissile = value;
            UpdateMissilesUI();
        }
    }

    public static int EnemiesTillNextMissile
    {
        set
        {
            instance.enemiesTillNextMissile = value;
            UpdateMissilesUI();
        }
    }

    static void UpdateMissilesUI()
    {
        instance.missilesText.text = "Missiles: " + instance.currentMissiles + "/" + instance.maxMissiles;
        if (instance.currentMissiles >= instance.maxMissiles)
        {
            instance.missileProgressImage.sizeDelta = new Vector3(instance.progressImageOGWidth, instance.missileProgressImage.sizeDelta.y);
        }
        else
        {
            instance.missileProgressImage.sizeDelta = new Vector2(instance.progressImageOGWidth *
                (1 - ((float)instance.currentEnemiesTillNextMissile / instance.enemiesTillNextMissile)),
                instance.missileProgressImage.sizeDelta.y);
        }
    }

}
