using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShrinkAndMove : MonoBehaviour {
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
        Text text = GetComponent<Text>();
        bool fadingOut = true;
        while (fadingOut)
        {
            text.color -= new Color(0, 0, 0, fadeSpeed * Time.deltaTime);
            if (text.color.a <= 0)
                fadingOut = false;
            yield return null;
        }        
    }


    IEnumerator FadeIn()
    {
        Text text = GetComponent<Text>();
        text.color = text.color *= new Color(1f, 1f, 1f, 0);
        bool fadingIn = true;
        while (fadingIn)
        {
            text.color += new Color(0, 0, 0, fadeSpeed * Time.deltaTime);
            if (text.color.a >= 1)
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
