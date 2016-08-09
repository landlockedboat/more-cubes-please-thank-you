using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeSceneButton : MonoBehaviour {
    [SerializeField]
    string sceneToLoad;
    
    public void ButtonPressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneToLoad);
    }
}
