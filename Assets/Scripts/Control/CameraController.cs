using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    [SerializeField]
    float growSpeed;
    [SerializeField]
    float maxHeight;
    [SerializeField]
    float growDelta;
    [SerializeField]
    float aspectRatio;
    [SerializeField]
    GameObject map;
    [SerializeField]
    float magicalScaleConstant = .05f;
    Transform mainCamera;
    static bool isGrowing = false;

    static public bool IsGrowing
    {
        get
        {
            return isGrowing;
        }

        set
        {
            isGrowing = value;
        }
    }

    // Use this for initialization
    void Start () {
        mainCamera = Camera.main.transform;
        StartCoroutine("Grow");
	}


    IEnumerator Grow() {
        while (true)
        {            
            while (!isGrowing)
            {
                yield return new WaitForSeconds(.5f);
            }            
            Vector3 newHeight = new Vector3(0, mainCamera.position.y + growDelta, 0);
            if (newHeight.y < maxHeight)
            {
                map.transform.localScale += new Vector3(growDelta * magicalScaleConstant, 0, growDelta * magicalScaleConstant);                
                bool finishedLerping = false;
                while (!finishedLerping)
                {
                    mainCamera.position = Vector3.MoveTowards(mainCamera.position, newHeight, growSpeed * Time.deltaTime);
                    if (mainCamera.position.y == newHeight.y)
                        finishedLerping = true;
                    yield return null;
                }
            }
            isGrowing = false;
        }
    }
}
