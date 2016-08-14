using UnityEngine;
using System.Collections;

public class UpgradeButton : MonoBehaviour {

    bool alreadyChosen = false;

    void OnEnable()
    {
        alreadyChosen = false;
    }

    public void MoreLifeUpgrade()
    {
        if (!alreadyChosen)
        {
            UpgradeControl.UpgradeChosen(UpgradeControl.UpgradeType.MoreLifeUpgrade);
            alreadyChosen = true;
        }
    }

    public void MoreMissilesUpgrade()
    {
        if (!alreadyChosen)
        {
            UpgradeControl.UpgradeChosen(UpgradeControl.UpgradeType.MoreMissilesUpgrade);
            alreadyChosen = true;
        }
    }

    public void ShootThroughUpgrade()
    {
        if (!alreadyChosen)
        {
            UpgradeControl.UpgradeChosen(UpgradeControl.UpgradeType.ShootThroughUpgrade);
            alreadyChosen = true;
        }
    }

    public void MoreBulletsUpgrade()
    {
        if (!alreadyChosen)
        {
            UpgradeControl.UpgradeChosen(UpgradeControl.UpgradeType.MoreBulletsUpgrade);
            alreadyChosen = true;
        }
    }

    public void MultishotUpgrade()
    {
        if (!alreadyChosen)
        {
            UpgradeControl.UpgradeChosen(UpgradeControl.UpgradeType.MultishotUpgrade);
            alreadyChosen = true;
        }
    }
}
