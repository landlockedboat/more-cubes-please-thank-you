using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class FadeAudioSource : MonoBehaviour {
    [SerializeField]
    float fadeTime = 3f;
    [Range(0,1)]
    [SerializeField]
    float startingVol = 0;
    [Range(0, 1)]
    [SerializeField]
    float finishingVol = 1;

    AudioSource audioSource;

    [SerializeField]
    bool fadeOnStart = true;
    [SerializeField]
    bool fadeOnEnable = false;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    public void FadeSound()
    {
        StartCoroutine("Fade");
    }

    void Start()
    {
        if (fadeOnStart)
            FadeSound();
    }

    public void OnEnable()
    {
        if(fadeOnEnable)
        {
            StopAllCoroutines();
            FadeSound();
        }
    }

    public void FadeSound(float startingVol, float finishingVol, float fadeTime)
    {
        this.startingVol = startingVol;
        this.finishingVol = finishingVol;
        this.fadeTime = fadeTime;        
        StartCoroutine("Fade");
    }

    IEnumerator Fade()
    {
        float currentTime = 0;
        while (currentTime / fadeTime < 1)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startingVol, finishingVol, currentTime / fadeTime);
            yield return null;
        }
    }
}
