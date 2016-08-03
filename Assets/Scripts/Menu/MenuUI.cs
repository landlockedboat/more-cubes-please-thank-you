using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuUI : MonoBehaviour {
    [SerializeField]
    FadeImageUI fadeImage;
    [SerializeField]
    FadeAudioSource fadeAudioSource;
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
        fadeAudioSource.FadeSound(1f, 0, 1);
        yield return new WaitForSeconds(timeToFade);
        SceneManager.LoadScene(scene);
    }

}
