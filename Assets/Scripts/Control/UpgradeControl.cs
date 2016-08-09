using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class UpgradeControl : MonoBehaviour {
    [Header("Health upgrade")]    
    [SerializeField]
    float healthAmmountIncrease = 100f;
    [SerializeField]
    float healingAmmountIncrease = .5f;

    [Header("Bullets upgrade")]
    [SerializeField]
    float shootingRateAmmountDecrease = .075f;
    [SerializeField]
    float shootingSpeedAmmountIncrease = 5f;

    [Header("Missiles upgarde")]    
    [SerializeField]
    int enemiesTillNextMissileAmmountDecrease = 50;
    [SerializeField]
    int missileSlotsIncrease = 1;

    [Header("Shoot through upgrade")]
    [SerializeField]
    int enemyShootThroughIncrease = 1;

    [Header("Multishot upgrade")]
    [SerializeField]
    int nOfExtraBulletsShot = 1;

    Dictionary<UpgradeType, int> upgradeLevels;
    

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
        upgradeLevels = new Dictionary<UpgradeType, int>();
        foreach (UpgradeType ut in Enum.GetValues(typeof(UpgradeType)))
        {
            upgradeLevels.Add(ut, 0);
        }
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
                PlayerHealth.MaxHealth += instance.healthAmmountIncrease;
                PlayerHealth.HealingPerEnemy += instance.healingAmmountIncrease;
                break;
            case UpgradeType.MoreMissilesUpgrade:
                PlayerShooting.MaxMissiles += instance.missileSlotsIncrease;
                PlayerShooting.EnemiesTillNextMissile -= instance.enemiesTillNextMissileAmmountDecrease;
                break;
            case UpgradeType.ShootThroughUpgrade:
                PlayerShooting.ShootThroughEnemies += instance.enemyShootThroughIncrease;
                break;
            case UpgradeType.MoreBulletsUpgrade:
                PlayerShooting.CooldownTime -= instance.shootingRateAmmountDecrease;
                PlayerShooting.Speed += instance.shootingSpeedAmmountIncrease;
                break;
            case UpgradeType.MultishotUpgrade:
                PlayerShooting.CurrentBulletsShot += instance.nOfExtraBulletsShot;
                break;
            default:
                break;
        }
        ++instance.upgradeLevels[type];
        EventManager.TriggerEvent(EventManager.EventType.OnUpgradesHidden);
    }

    public static int GetLevel(UpgradeType ut)
    {
        return instance.upgradeLevels[ut];
    }

    void OnEnable()
    {
        EventManager.StartListening(EventManager.EventType.OnSpawnResumed, OnGameResumed);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.EventType.OnSpawnResumed, OnGameResumed);
    }

    void OnGameResumed() {
        UpgradesUI.SetActive(false);
    }

    public enum UpgradeType
    {
        MoreLifeUpgrade, MoreMissilesUpgrade, ShootThroughUpgrade, MoreBulletsUpgrade, MultishotUpgrade

    }
}
