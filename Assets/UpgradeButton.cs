using UnityEngine;
using System.Collections;

public class UpgradeButton : MonoBehaviour {

    public void MoreLifeUpgrade()
    {
        UpgradeControl.UpgradeChosen(UpgradeControl.UpgradeType.MoreLifeUpgrade);
    }

    public void MoreMissilesUpgrade()
    {
        UpgradeControl.UpgradeChosen(UpgradeControl.UpgradeType.MoreMissilesUpgrade);
    }

    public void MissileRateUpgrade()
    {
        UpgradeControl.UpgradeChosen(UpgradeControl.UpgradeType.MissileRateUpgrade);
    }

    public void ShootThroughUpgrade()
    {
        UpgradeControl.UpgradeChosen(UpgradeControl.UpgradeType.ShootThroughUpgrade);
    }

    public void FireRateUpgrade()
    {
        UpgradeControl.UpgradeChosen(UpgradeControl.UpgradeType.FireRateUpgrade);
    }
}
