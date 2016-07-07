using UnityEngine;
using System.Collections;

public class BulletLogic : MonoBehaviour {
    private static float speed = 20;
    private bool killFlag = false;

    public static float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    void OnEnable() {
        EventManager.StartListening(EventManager.EventType.OnBulletKill, Destroy);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.EventType.OnBulletKill, Destroy);
    }

    void Update () {
        transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
	}

    private void Destroy() {
        if (killFlag)
            Destroy(gameObject);
        else
            killFlag = true;
    }
}
