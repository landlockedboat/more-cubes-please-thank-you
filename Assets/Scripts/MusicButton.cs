using UnityEngine;
using System.Collections;

public class MusicButton : MonoBehaviour {
    GameObject musicOn;
    GameObject musicOff;

    void Start()
    {
        musicOn = transform.FindChild("MusicOn").gameObject;
        musicOff = transform.FindChild("MusicOff").gameObject;
        if (AudioSettingsControl.MusicOn)
        {
            musicOn.SetActive(true);
            musicOff.SetActive(false);
        }
        else
        {
            musicOn.SetActive(false);
            musicOff.SetActive(true);
        }
    }

    public void MusicOnPressed()
    {
        musicOn.SetActive(false);
        musicOff.SetActive(true);
        AudioSettingsControl.MusicOn = false;       
    }

    public void MusicOffPressed()
    {
        musicOn.SetActive(true);
        musicOff.SetActive(false);
        AudioSettingsControl.MusicOn = true;        
    }
}
