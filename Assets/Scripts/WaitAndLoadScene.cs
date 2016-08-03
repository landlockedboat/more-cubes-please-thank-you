using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WaitAndLoadScene : MonoBehaviour {
    [SerializeField]
    float waitTime = 0f;
	// Use this for initialization
	void Start () {
        StartCoroutine("WaitAndLoad");
	}
	
    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("menu");
    }
}
