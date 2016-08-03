using UnityEngine;
using System.Collections;

public class CursorController : MonoBehaviour {

    [SerializeField]
    GameObject crosshair;
    bool gameOver = false;

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        if (gameOver)
            return;
        crosshair.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition +
            new Vector3(0,0,Camera.main.transform.position.y));
    }

    void OnEnable()
    {
        EventManager.StartListening(EventManager.EventType.OnGameOver, OnGameOver);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.EventType.OnGameOver, OnGameOver);
    }

    void OnGameOver()
    {
        gameOver = true;
        Cursor.visible = true;
        //We send the crosshair to the fucking space
        crosshair.transform.position = new Vector3(-200, 0);
    }
}
