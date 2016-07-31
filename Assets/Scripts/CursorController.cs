using UnityEngine;
using System.Collections;

public class CursorController : MonoBehaviour {

    [SerializeField]
    GameObject crosshair;

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        crosshair.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition +
            new Vector3(0,0,Camera.main.transform.position.y));
    }

    public void OnPointerEnter()
    {
        Cursor.visible = false;
    }

    public void OnPointerExit()
    {
        Cursor.visible = true;
    }
}
