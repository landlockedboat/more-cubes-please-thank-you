using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[RequireComponent(typeof(FadeTextUI))]
public class TutorialText : MonoBehaviour {
    [SerializeField]
    string moveTutText;
    [SerializeField]
    string bulletsTutText;
    [SerializeField]
    string missilesTutText;
    [SerializeField]
    Text tutorialText;

    TutState tutState = TutState.MoveTut;
    FadeTextUI fadeText;

    enum TutState {
        MoveTut, BulletsTut, MissilesTut
    }

    void UpdateText()
    {
        switch (tutState)
        {
            case TutState.MoveTut:
                tutorialText.text = moveTutText;
                break;
            case TutState.BulletsTut:
                tutorialText.text = bulletsTutText;
                break;
            case TutState.MissilesTut:
                tutorialText.text = missilesTutText;
                break;
            default:
                break;
        }
        fadeText.StartAnimation();
    }

    void Start()
    {
        fadeText = GetComponent<FadeTextUI>();
        UpdateText();
    }

    void Update()
    {
        switch (tutState)
        {
            case TutState.MoveTut:
                if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
                {
                    tutState = TutState.BulletsTut;
                    UpdateText();
                }
                break;
            case TutState.BulletsTut:
                break;
            case TutState.MissilesTut:
                break;
            default:
                break;
        }
    }

}
