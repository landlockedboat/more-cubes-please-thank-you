using UnityEngine;
using System.Collections;

public class TakeAScreenshot : MonoBehaviour {
    [SerializeField]
    KeyCode keyToPress = KeyCode.K;
    [SerializeField]
    int scaleFactor = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(keyToPress))
        {
            string screenshotName = "Screenshot-" + System.DateTime.Now.ToFileTime() + ".png";
            Debug.Log(Application.persistentDataPath + screenshotName);
            Application.CaptureScreenshot(screenshotName, scaleFactor);
        }
	}
}
