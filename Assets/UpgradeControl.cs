using UnityEngine;
using System.Collections;

public class UpgradeControl : MonoBehaviour {
    [Range(0,100)]
    [SerializeField]
    float healthPercentageIncrease = 50;    
    [Range(0, 100)]
    [SerializeField]
    float shootingRatePercentageDecrease = 20;
    [Range(0, 100)]
    [SerializeField]
    float missileCollectionRateIncrease = 50;
    [SerializeField]
    int missileSlotsIncrease = 1;
    [SerializeField]
    int enemyShootThroughIncrease = 1;
    
    

    private static UpgradeControl upgradeControl;

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
                           
                break;
            case UpgradeType.MoreMissilesUpgrade:
                break;
            case UpgradeType.MissileRateUpgrade:
                break;
            case UpgradeType.ShootThroughUpgrade:
                break;
            case UpgradeType.FireRateUpgrade:
                break;
            default:
                break;
        }
        Debug.Log(type);
        UpgradesUI.SetActive(false);
        GameControl.ResumeGame();
    }

    public enum UpgradeType
    {
        MoreLifeUpgrade, MoreMissilesUpgrade, MissileRateUpgrade, ShootThroughUpgrade, FireRateUpgrade
    }
}
