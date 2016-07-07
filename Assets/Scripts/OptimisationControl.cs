using UnityEngine;
using System.Collections;

public class OptimisationControl : MonoBehaviour {
    [SerializeField]
    private float timeToKillBullets = 2f;
    private float currentTimeToKillBullets;
	// Use this for initialization
	void Start () {
        currentTimeToKillBullets = timeToKillBullets;
    }
	
	// Update is called once per frame
	void Update () {
	    if(currentTimeToKillBullets <= 0)
        {
            currentTimeToKillBullets = timeToKillBullets;
            EventManager.TriggerEvent(EventManager.EventType.OnBulletKill);
        }
        currentTimeToKillBullets -= Time.deltaTime;
	}
}
