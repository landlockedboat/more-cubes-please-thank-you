using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(FadeTextUI))]
public class TutorialControl: MonoBehaviour {
    [SerializeField]
    GameObject playerInTutorial;
    [SerializeField]
    string moveTutText;
    [SerializeField]
    string bulletsTutText;
    [SerializeField]
    string missilesTutText;
    [SerializeField]
    Text tutorialText;

    static Quaternion tutPlayRot;
    static Vector3 tutPlayPos = Vector3.zero;

    TutState tutState = TutState.MoveTut;
    FadeTextUI fadeText;

    [Header("Final countdown")]
    [SerializeField]
    float timeBetweenNumbers = 1f;
    int currentNumber = 3;

    [Header("Fading Image")]
    [SerializeField]
    GameObject fadeImage;
    [SerializeField]
    float fadeImageDuration = 1f;

    public static Quaternion TutPlayRot
    {
        get
        {
            return tutPlayRot;
        }
    }

    public static Vector3 TutPlayPos
    {
        get
        {
            return tutPlayPos;
        }
    }

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
                if (Input.GetMouseButtonDown(0))
                {
                    tutState = TutState.MissilesTut;
                    UpdateText();
                }
                break;
            case TutState.MissilesTut:
                if (Input.GetMouseButtonDown(1))
                {
                    StartCoroutine("Countdown");
                }
                break;
            default:
                break;
        }
    }

    IEnumerator Countdown()
    {
        while(currentNumber > 0)
        {
            tutorialText.text = currentNumber.ToString();
            fadeText.StartAnimation();
            yield return new WaitForSeconds(timeBetweenNumbers);
            --currentNumber;
        }
        tutorialText.text = "GO!";
        fadeText.StartAnimation();
        yield return new WaitForSeconds(timeBetweenNumbers / 2);
        fadeImage.GetComponent<FadeImageUI>().StartAnimation();
        yield return new WaitForSeconds(fadeImageDuration);
        tutPlayRot = playerInTutorial.transform.localRotation;
        tutPlayPos = playerInTutorial.transform.position;
        Destroy(playerInTutorial);
        SceneManager.LoadScene(1);
    }

}
