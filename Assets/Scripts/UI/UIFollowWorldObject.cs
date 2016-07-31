using UnityEngine;
using System.Collections;

public class UIFollowWorldObject : MonoBehaviour
{
    [SerializeField]
    string objectToFollowName;
    Transform objectToFollow;
    [SerializeField]
    bool moveOnUpdate = true;

    RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void FollowObject()
    {
        Vector3 pos = Vector3.zero;
        if (objectToFollow != null)
            pos = objectToFollow.position;
        else
            objectToFollow = GameObject.Find(objectToFollowName).transform;
        Vector3 viewportPoint = Camera.main.WorldToViewportPoint(pos);
        rectTransform.anchorMin = viewportPoint;
        rectTransform.anchorMax = viewportPoint;
    }

    void Update()
    {
        if (moveOnUpdate)
            FollowObject();
    }
}
