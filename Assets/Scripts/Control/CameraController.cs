using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float growSpeed;
    [SerializeField]
    float maxHeight;
    [SerializeField]
    float growDelta;


    Vector3 newHeight;

    void OnEnable()
    {
        EventManager.StartListening(EventManager.EventType.OnLevelChanged, OnLevelChanged);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.EventType.OnLevelChanged, OnLevelChanged);
    }

    void Start()
    {
        newHeight = transform.position;
    }

    void OnLevelChanged()
    {
        if (transform.position.y < maxHeight)
        {
            StopAllCoroutines();
            transform.position = newHeight;
            StartCoroutine("Grow");
        }
    }


    IEnumerator Grow()
    {
        newHeight = new Vector3(0, transform.position.y + growDelta, 0);
        //map.transform.localScale *= magicalScaleConstant;
        bool finished = false;
        while (!finished)
        {
            transform.position = Vector3.MoveTowards(transform.position, newHeight,
                growSpeed * Time.deltaTime);
            if (transform.position.y >= newHeight.y)
                finished = true;
            yield return null;
        }
    }
}
