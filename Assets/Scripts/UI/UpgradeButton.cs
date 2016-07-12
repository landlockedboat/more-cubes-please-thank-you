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

    public void ShootThroughUpgrade()
    {
        UpgradeControl.UpgradeChosen(UpgradeControl.UpgradeType.ShootThroughUpgrade);
    }

    public void MoreBulletsUpgrade()
    {
        UpgradeControl.UpgradeChosen(UpgradeControl.UpgradeType.MoreBulletsUpgrade);
    }

    public void MultishotUpgrade() {
        UpgradeControl.UpgradeChosen(UpgradeControl.UpgradeType.MultishotUpgrade);
    }
}
