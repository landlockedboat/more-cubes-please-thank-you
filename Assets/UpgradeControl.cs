using UnityEngine;
using System.Collections;

public class UpgradeControl : MonoBehaviour {

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
        //UpgradesUI.ShowUpgrades();
    }

    public static void UpgradeChosen(UpgradeType ut)
    {
        switch (ut)
        {
            case UpgradeType.MoreLife:
                
                break;
            case UpgradeType.RateOfFire:
                PlayerShooting.CooldownTime -= PlayerShooting.CooldownTime * .5f;
                break;
            case UpgradeType.MoreMissiles:

                break;
            case UpgradeType.MissileRate:

                break;
            case UpgradeType.ShootThrough:

                break;
            default:
                break;
        }
        //GameController.ResumeGame();
    }

    public enum UpgradeType
    {
        MoreLife, RateOfFire, MoreMissiles, MissileRate, ShootThrough
    }
}
