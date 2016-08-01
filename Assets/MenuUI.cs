using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuUI : MonoBehaviour {
    [SerializeField]
    FadeImageUI fadeImage;
    [SerializeField]
    float timeToFade;
    bool changing = false;

    public void Play()
    {
        if (!changing)
        {
            changing = true;
            StartCoroutine("FadeAndChange", "tutorial");
        }
    }

    public void SkipTutorial()
    {
        if (!changing)
        {
            changing = true;
            StartCoroutine("FadeAndChange", "game");
        }
    }

    IEnumerator FadeAndChange(string scene)
    {
        fadeImage.StartAnimation();
        yield return new WaitForSeconds(timeToFade);
        SceneManager.LoadScene(scene);
    }

}
