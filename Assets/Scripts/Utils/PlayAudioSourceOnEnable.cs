using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class PlayAudioSourceOnEnable : MonoBehaviour {

    AudioSource audioSource;

    void OnEnable()
    {
        if(!audioSource)
            audioSource = GetComponent<AudioSource>();
        if(AudioSettingsControl.AudioOn)
            audioSource.Play();
    }


}
