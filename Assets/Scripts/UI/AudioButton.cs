using UnityEngine;
using System.Collections;

public class AudioButton : MonoBehaviour {

    GameObject audioOn;
    GameObject audioOff;

    void Start()
    {
        audioOn = transform.FindChild("AudioOn").gameObject;
        audioOff = transform.FindChild("AudioOff").gameObject;
        if (AudioSettingsControl.AudioOn)
        {
            audioOn.SetActive(true);
            audioOff.SetActive(false);
        }
        else
        {
            audioOn.SetActive(false);
            audioOff.SetActive(true);
        }
    }

    public void AudioOnPressed()
    {
        audioOn.SetActive(false);
        audioOff.SetActive(true);
        AudioSettingsControl.AudioOn = false;
    }

    public void AudioOffPressed()
    {
        audioOn.SetActive(true);
        audioOff.SetActive(false);
        AudioSettingsControl.AudioOn = true;
    }
}
