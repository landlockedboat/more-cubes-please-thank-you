using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class DelayAudioSource : MonoBehaviour {
    [SerializeField]
    float delay = 0f;

    AudioSource audioSource;

    void Start () {
        StartCoroutine("Delay");
        audioSource = GetComponent<AudioSource>();
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);
        if(AudioSettingsControl.AudioOn)
            audioSource.Play();
    }
}
