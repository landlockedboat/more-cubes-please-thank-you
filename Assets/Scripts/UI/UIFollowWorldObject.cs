using UnityEngine;
using System.Collections;

public class UIFollowWorldObject : MonoBehaviour {
    [SerializeField]
    Transform objectToFollow;
    [SerializeField]
    bool moveOnUpdate = true;

    RectTransform rectTransform;

    void Start () {
        rectTransform = GetComponent<RectTransform>();
    }

    public void FollowObject()
    {
        Vector3 pos = objectToFollow.position;
        Vector3 viewportPoint = Camera.main.WorldToViewportPoint(pos);
        rectTransform.anchorMin = viewportPoint;
        rectTransform.anchorMax = viewportPoint;
    }
    
    void Update () {
        if (moveOnUpdate)
            FollowObject();
    }
}
