using UnityEngine;
using System.Collections;

public class AudioSettingsControl : MonoBehaviour
{

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
                    GameObject go = new GameObject();
                    go.name = "AudioSettingsControl";
                    go.AddComponent<AudioSettingsControl>();
                    audioSettingsControl = FindObjectOfType<AudioSettingsControl>();
                    Debug.Log("AudioSettingsControl script created on a GameObject in your scene.");

                }
                audioSettingsControl.Init();
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
            if (instance.musicOn)
            {
                EventManager.TriggerEvent(EventManager.EventType.OnMusicUnmuted);
            }
            else
            {
                EventManager.TriggerEvent(EventManager.EventType.OnMusicMuted);
            }
        }
    }

    void Init()
    {
        DontDestroyOnLoad(gameObject);
    }
}
