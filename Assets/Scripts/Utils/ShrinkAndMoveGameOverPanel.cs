using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShrinkAndMoveGameOverPanel : MonoBehaviour {
    [Header("Position")]
    [SerializeField]
    Vector2 startingPos;
    [SerializeField]
    Vector2 finalPos;


    [Header("Size")]
    [SerializeField]
    Vector2 startingSize;
    [SerializeField]
    Vector2 finalSize;

    [Header("Fade")]
    [SerializeField]
    float fadeSpeed = 6f;

    [Space(10)]
    [SerializeField]
    float animationStartTime = 1f;
    [SerializeField]
    float animationTime = 1f;
    float currentTime;

    [Header("Ping pong")]
    [SerializeField]
    bool pingPong = false;
    [SerializeField]
    float pingPongTime = 2;
    [SerializeField]
    Text text1;
    [SerializeField]
    Text text2;
    [SerializeField]
    Image image;
    RectTransform rectTransform;
	// Use this for initialization
	void Awake () {
        rectTransform = GetComponent<RectTransform>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Animate()
    {
        StartCoroutine("DoTheThing");
        StartCoroutine("FadeIn");
        if(pingPong)
        {
            StartCoroutine("WaitForPingPong");
        }
    }

    IEnumerator WaitForPingPong()
    {
        yield return new WaitForSeconds(pingPongTime);
        Vector2 aux = startingPos;
        startingPos = finalPos;
        finalPos = aux;
        aux = startingSize;
        startingSize = finalSize;
        finalSize = aux;
        StartCoroutine("DoTheThing");
        StartCoroutine("FadeOut");
    }

    IEnumerator FadeOut()
    {
        
        bool fadingOut = true;
        while (fadingOut)
        {
            Color col = new Color(0, 0, 0, fadeSpeed * Time.deltaTime);
            text1.color -= col;
            text2.color -= col;
            image.color -= col;
            if (text1.color.a <= 0)
                fadingOut = false;
            yield return null;
        }        
    }


    IEnumerator FadeIn()
    {
        Color alphaRemover = new Color(1f, 1f, 1f, 0);
        text1.color *= alphaRemover;
        text2.color *= alphaRemover;
        image.color *= alphaRemover;
        bool fadingIn = true;
        while (fadingIn)
        {
            Color col = new Color(0, 0, 0, fadeSpeed * Time.deltaTime);
            text1.color += col;
            text2.color += col;
            image.color += col;
            if (text1.color.a >= 1)
                fadingIn = false;
            yield return null;
        }
    }

    IEnumerator DoTheThing() {
        rectTransform.anchoredPosition = startingPos;
        transform.localScale = startingSize;
        currentTime = 0;
        bool finished = false;
        yield return new WaitForSeconds(animationStartTime);
        while (!finished)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(startingPos, finalPos, currentTime / animationTime);
            transform.localScale = Vector2.Lerp(startingSize, finalSize, currentTime / animationTime);
            currentTime += Time.deltaTime;
            if (currentTime >= animationTime)
            {
                finished = true;
                rectTransform.anchoredPosition = finalPos;
                transform.localScale = finalSize;
            }
            yield return null;                
        }
    }
}
