using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class SpecialLevelControl : MonoBehaviour {
    [SerializeField]
    int minLevelsTillSpecial = 5;
    [SerializeField]
    int maxLevelsTilSpecial = 10;
    [SerializeField]
    Text specialLevelText;
    string specialLevelString;

    int levelsFromLastSpecial = 0;
    bool isOnSpecialLevel = false;
    bool upgradesShowing = false;
    SpecialLevel lastSpecialLevel;

    float OGHealing;

    static SpecialLevelControl specialLevelControl;

    public static SpecialLevelControl instance
    {
        get
        {
            if (!specialLevelControl)
            {
                specialLevelControl = FindObjectOfType<SpecialLevelControl>();
                if (!specialLevelControl)
                {
                    Debug.LogError("There needs to be one active SpecialLevelControl script on a GameObject in your scene.");
                }
                else
                {
                    specialLevelControl.Init();
                }
            }

            return specialLevelControl;
        }
    }

    void Init()
    {
    }

    void TryBeginSpecialLevel()
    {
        bool ret = false;
        if (instance.levelsFromLastSpecial > instance.minLevelsTillSpecial)
        {
            ret = UnityEngine.Random.Range(0f, 1f) < (float)instance.levelsFromLastSpecial / (float)instance.maxLevelsTilSpecial;
            if (ret)
            {
                instance.levelsFromLastSpecial = 0;
                BeginSpecialLevel();
            }
        }        
    }

    void OnEnable()
    {
        EventManager.StartListening(EventManager.EventType.OnLevelChanged, OnLevelChanged);
        EventManager.StartListening(EventManager.EventType.OnUpgradesShown, OnUpgradesShown);
        EventManager.StartListening(EventManager.EventType.OnUpgradesHidden, OnUpgradesHidden);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.EventType.OnLevelChanged, OnLevelChanged);
        EventManager.StopListening(EventManager.EventType.OnUpgradesShown, OnUpgradesShown);
        EventManager.StopListening(EventManager.EventType.OnUpgradesHidden, OnUpgradesHidden);
    }

    void OnUpgradesShown()
    {
        upgradesShowing = true;
        specialLevelText.gameObject.SetActive(false);
    }

    void OnUpgradesHidden()
    {
        upgradesShowing = false;
        if (isOnSpecialLevel)
        {
            specialLevelText.gameObject.SetActive(true);
            specialLevelText.text = specialLevelString;
        }
    }

    void OnLevelChanged()
    {
        if (instance.isOnSpecialLevel)
            EndSpecialLevel();
        ++instance.levelsFromLastSpecial;
        TryBeginSpecialLevel();
    }

    void BeginSpecialLevel()
    {
        SpecialLevel[] values = (SpecialLevel[])Enum.GetValues(typeof(SpecialLevel));
        SpecialLevel sl = values[UnityEngine.Random.Range(0, values.Length)];        
        switch (sl)
        {
            case SpecialLevel.NoHealing:
                instance.OGHealing = PlayerHealth.HealingPerEnemy;
                PlayerHealth.HealingPerEnemy = 0;
                break;
            case SpecialLevel.DoubleHealing:
                instance.OGHealing = PlayerHealth.HealingPerEnemy;
                PlayerHealth.HealingPerEnemy *= 2;
                break;
            case SpecialLevel.BiggerEnemies:
                SpawnerControl.BiggerEnemies = true;
                break;
            case SpecialLevel.SmallerEnemies:
                SpawnerControl.SmallerEnemies = true;
                break;
            case SpecialLevel.FasterEnemies:
                LevelControl.FasterEnemies = true;
                break;
            case SpecialLevel.SlowerEnemies:
                LevelControl.SlowerEnemies = true;
                break;
            case SpecialLevel.NoMissiles:
                PlayerShooting.CanShootMissiles = false;
                break;
            default:
                break;
        }
        instance.lastSpecialLevel = sl;
        instance.isOnSpecialLevel = true;
        specialLevelString = "SPECIAL LEVEL! " + sl.ToString();
        if (!upgradesShowing)
        {
            specialLevelText.gameObject.SetActive(true);
            specialLevelText.text = specialLevelString;
        }
    }

    void EndSpecialLevel()
    {
        switch (lastSpecialLevel)
        {
            case SpecialLevel.NoHealing:
                PlayerHealth.HealingPerEnemy = instance.OGHealing;
                break;
            case SpecialLevel.DoubleHealing:
                PlayerHealth.HealingPerEnemy = instance.OGHealing;
                break;
            case SpecialLevel.BiggerEnemies:
                SpawnerControl.BiggerEnemies = false;
                break;
            case SpecialLevel.SmallerEnemies:
                SpawnerControl.SmallerEnemies = false;
                break;
            case SpecialLevel.FasterEnemies:
                LevelControl.FasterEnemies = false;
                break;
            case SpecialLevel.SlowerEnemies:
                LevelControl.SlowerEnemies = false;
                break;
            case SpecialLevel.NoMissiles:
                PlayerShooting.CanShootMissiles = true;
                break;
            default:
                break;
        }
        instance.isOnSpecialLevel = false;
        specialLevelText.gameObject.SetActive(false);
    }
        
    private enum SpecialLevel
    {
        NoHealing, DoubleHealing, BiggerEnemies, SmallerEnemies, FasterEnemies, SlowerEnemies, NoMissiles
    }
}
