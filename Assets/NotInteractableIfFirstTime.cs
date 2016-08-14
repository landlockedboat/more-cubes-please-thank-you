using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class NotInteractableIfFirstTime : MonoBehaviour {
    void OnEnable()
    {
        if(!PlayerPrefs.HasKey("IS_FIRST_TIME_PLAYING") ||
            PlayerPrefs.GetInt("IS_FIRST_TIME_PLAYING") == 1)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
