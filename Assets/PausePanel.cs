using UnityEngine;
using System.Collections;

public class PausePanel : MonoBehaviour {
    [SerializeField]
    bool isPaused = false;
	// Use this for initialization
	void Start () {
        SetChildrenActive(false);
    }

    void SetChildrenActive(bool active)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(active);
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                Time.timeScale = 0;
                EventManager.TriggerEvent(EventManager.EventType.OnGamePaused);
            }

            else
            {
                Time.timeScale = 1;
                EventManager.TriggerEvent(EventManager.EventType.OnGameResumed);
            }
                
            SetChildrenActive(isPaused);
        }
	}
}
