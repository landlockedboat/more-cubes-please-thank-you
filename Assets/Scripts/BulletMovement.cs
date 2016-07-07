using UnityEngine;
using System.Collections;

public class BulletMovement : MonoBehaviour {
    [SerializeField]
    private static float speed = 20;

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
	
	void Update () {
        transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
	}
}
