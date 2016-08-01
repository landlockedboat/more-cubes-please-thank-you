using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class BlinkTextUI : MonoBehaviour {
    [Range(0,10)]
    [SerializeField]
    float blinkSpeed = 6f;
    [SerializeField]
    Color primaryColor;
    [SerializeField]
    Color secondaryColor;

    Text text;
    bool blinkingToSecondary = true;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        Color nextColor = Color.black;
        if (blinkingToSecondary) {
            nextColor = Color.Lerp(primaryColor, secondaryColor, Mathf.PingPong(Time.time * blinkSpeed, 1));
            if (Color.Equals(nextColor, secondaryColor))
                blinkingToSecondary = false;
        }
        else
        {
            nextColor = Color.Lerp(secondaryColor, primaryColor, Mathf.PingPong(Time.time * blinkSpeed, 1));
            if (Color.Equals(nextColor, primaryColor))
                blinkingToSecondary = true;
        }
        text.color = nextColor;
    }
}
