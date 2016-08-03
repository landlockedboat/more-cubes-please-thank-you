using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinalScoreUI : MonoBehaviour {

    Text scoreText;

	void Start () {
        scoreText = GetComponent<Text>();
        scoreText.text = "Score: " + ScoreControl.CurrentScore.ToString("n0") + " Points";

    }

}
