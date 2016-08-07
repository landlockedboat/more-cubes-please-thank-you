using UnityEngine;
using System.Collections;

public class AudioSettingsControl : MonoBehaviour {

    static AudioSettingsControl audioSettingsControl;
    [SerializeField]
    bool audioOn = true;
    [SerializeField]
    bool musicOn = true;

    public static AudioSettingsControl instance
    {
        get
        {
            if (!audioSettingsControl)
            {
                audioSettingsControl = FindObjectOfType<AudioSettingsControl>();
                if (!audioSettingsControl)
                {
                    Debug.LogError("There needs to be one active AudioSettingsControl script on a GameObject in your scene.");
                }
                else
                {
                    audioSettingsControl.Init();
                }
            }

            return audioSettingsControl;
        }
    }

    public static bool AudioOn
    {
        get
        {
            return instance.audioOn;
        }

        set
        {
            instance.audioOn = value;
        }
    }

    public static bool MusicOn
    {
        get
        {
            return instance.musicOn;
        }

        set
        {
            instance.musicOn = value;
        }
    }

    void Init()
    {

    }
}
