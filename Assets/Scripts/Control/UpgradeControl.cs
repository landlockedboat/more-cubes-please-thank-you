using UnityEngine;
using System.Collections;

public class UpgradeControl : MonoBehaviour {
    [Header("Health upgrade")]
    [Range(0f, 1f)]
    [SerializeField]
    float healthPercentageIncrease = .2f;
    [Range(0f, 1f)]
    [SerializeField]
    float healingPercentageIncrease = .2f;

    [Header("Bullets upgrade")]
    [Range(0f, 1f)]
    [SerializeField]
    float shootingRatePercentageDecrease = .2f;
    [Range(0f, 1f)]
    [SerializeField]
    float shootingSpeedPercentageIncrease = .15f;

    [Header("Missiles upgarde")]
    [Range(0f, 1f)]
    [SerializeField]
    float missileCollectionRateIncrease = .5f;
    [SerializeField]
    int missileSlotsIncrease = 1;

    [Header("Shoot through upgrade")]
    [SerializeField]
    int enemyShootThroughIncrease = 1;

    [Header("Multishot upgrade")]
    int nOfExtraBulletsShot = 1;    
    

    static UpgradeControl upgradeControl;

    public static UpgradeControl instance
    {
        get
        {
            if (!upgradeControl)
            {
                upgradeControl = FindObjectOfType<UpgradeControl>();
                if (!upgradeControl)
                {
                    Debug.LogError("There needs to be one active UpgradeControl script on a GameObject in your scene.");
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

    public static void ShowUpgrades()
    {
        UpgradesUI.SetActive(true);
        UpgradesUI.ShowUpgrades();
    }


    public static void UpgradeChosen(UpgradeType type)
    {
        switch (type)
        {
            case UpgradeType.MoreLifeUpgrade:
                PlayerHealth.MaxHealth *= 1 + (instance.healthPercentageIncrease);
                break;
            case UpgradeType.MoreMissilesUpgrade:
                PlayerShooting.MaxMissiles += instance.missileSlotsIncrease;
                break;
            case UpgradeType.MissileRateUpgrade:
                PlayerShooting.EnemiesTillNextMissile *= 100 + (int)(instance.missileCollectionRateIncrease * 100);
                PlayerShooting.EnemiesTillNextMissile /= 100;
               break;
            case UpgradeType.ShootThroughUpgrade:
                BulletLogic.ShootThroughEnemies += instance.enemyShootThroughIncrease;
                break;
            case UpgradeType.FireRateUpgrade:
                PlayerShooting.CooldownTime *= 1 - (instance.shootingRatePercentageDecrease);
                break;
            default:
                break;
        }
        Debug.Log(type);
        EventManager.TriggerEvent(EventManager.EventType.OnUpgradesHidden);
    }

    void OnEnable()
    {
        EventManager.StartListening(EventManager.EventType.OnGameResumed, OnGameResumed);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.EventType.OnGameResumed, OnGameResumed);
    }

    void OnGameResumed() {
        UpgradesUI.SetActive(false);
    }

    public enum UpgradeType
    {
        MoreLifeUpgrade, MoreMissilesUpgrade, MissileRateUpgrade, ShootThroughUpgrade, FireRateUpgrade
    }
}
