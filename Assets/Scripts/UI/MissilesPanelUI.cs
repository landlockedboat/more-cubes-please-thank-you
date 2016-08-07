using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class MissilesPanelUI : MonoBehaviour
{
    [SerializeField]
    Text missilesText;
    [SerializeField]
    RectTransform missileProgressImage;
    float progressImageOGWidth;
    int maxMissiles;
    int currentMissiles;
    int currentEnemiesTillNextMissile;
    int enemiesTillNextMissile = 1;
    [SerializeField]
    AudioSource audioSource;

    AnimateUI[] animateUI;

    string missilesPanelText;

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
        animateUI = GetComponents<AnimateUI>();
    }

    void StartAnimation()
    {
        foreach (AnimateUI aUI in animateUI)
        {
            aUI.StartAnimation();
        }
    }

    public static int CurrentMissiles
    {
        set
        {
            int prevCurrentMissiles = instance.currentMissiles;                
            instance.currentMissiles = value;
            UpdateMissilesUI();
            //We animate later to prevent errors
            if (prevCurrentMissiles < instance.currentMissiles)
            {
                instance.StartAnimation();
                if(AudioSettingsControl.AudioOn)
                    instance.audioSource.Play();
            }
        }
    }

    public static int MaxMissiles
    {
        set
        {
            instance.maxMissiles = value;
            UpdateMissilesUI();
            instance.StartAnimation();
            if(AudioSettingsControl.AudioOn)
                instance.audioSource.Play();
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
        instance.missilesPanelText = "Missiles: " + instance.currentMissiles + "/" + instance.maxMissiles;
        instance.missilesText.text = instance.missilesPanelText;
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

    void OnEnable()
    {
        instance.missilesText.text = instance.missilesPanelText;
    }

}
