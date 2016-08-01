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
    string congratsText;
    [SerializeField]
    Text tutorialText;
    [SerializeField]
    Text goText;

    static Quaternion tutPlayRot;
    static Vector3 tutPlayPos = Vector3.zero;

    TutState tutState = TutState.MoveTut;
    [SerializeField]
    FadeTextUI tutorialTextFadeText;
    [SerializeField]
    FadeTextUI goTextFadeText;

    [Header("Final countdown")]
    [SerializeField]
    float congratsTimer = 1f;
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
        MoveTut, BulletsTut, MissilesTut, Congrats
    }

    void UpdateText(string text)
    {
        tutorialText.text = text;
        tutorialTextFadeText.StartAnimation();
    }

    void Start()
    {
        tutorialTextFadeText = GetComponent<FadeTextUI>();
        UpdateText(moveTutText);
    }

    void Update()
    {
        switch (tutState)
        {
            case TutState.MoveTut:
                if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
                {
                    StartCoroutine("UpdateState", TutState.BulletsTut);
                    tutState = TutState.BulletsTut;
                    UpdateText(bulletsTutText);
                }
                break;
            case TutState.BulletsTut:
                if (Input.GetMouseButtonDown(0))
                {
                    tutState = TutState.MissilesTut;
                    UpdateText(missilesTutText);
                }
                break;
            case TutState.MissilesTut:
                if (Input.GetMouseButtonDown(1))
                {
                    UpdateText(congratsText);
                    StartCoroutine("Countdown");
                }
                break;
            default:
                break;
        }
    }

    IEnumerator UpdateState()
    {
        return null;
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(congratsTimer);
        tutorialText.text = "";
        while(currentNumber > 0)
        {
            goText.text = currentNumber.ToString();
            goTextFadeText.StartAnimation();
            yield return new WaitForSeconds(timeBetweenNumbers);
            --currentNumber;
        }
        goText.text = "GO!";
        goTextFadeText.StartAnimation();
        yield return new WaitForSeconds(timeBetweenNumbers / 2);
        fadeImage.GetComponent<FadeImageUI>().StartAnimation();
        yield return new WaitForSeconds(fadeImageDuration);
        tutPlayRot = playerInTutorial.transform.localRotation;
        tutPlayPos = playerInTutorial.transform.position;
        Destroy(playerInTutorial);
        SceneManager.LoadScene("game");
    }

}
