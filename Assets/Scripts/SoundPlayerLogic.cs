using UnityEngine;
using System.Collections;

public class SoundPlayerLogic : MonoBehaviour {
    [SerializeField]
    AudioClip audioClip;
    [SerializeField]
    float timeToLive = 1f;
    [Range(0f, 1f)]
    [SerializeField]
    float volume = 1f;

    public AudioClip AudioClip
    {
        set
        {
            audioClip = value;
        }
    }

    public float Volume
    {
        set
        {
            volume = value;
        }
    }

    // Use this for initialization
    void Start () {
        AudioSource aus = GetComponent<AudioSource>();
        aus.clip = audioClip;
        aus.volume = volume;
        aus.Play();
        StartCoroutine("Die");
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(timeToLive);
        Destroy(gameObject);
    }
}
