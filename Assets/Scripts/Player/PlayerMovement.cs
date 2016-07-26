using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    float speed = 5;
    [Range(0f,1f)]
    [SerializeField]
    float speedPercentageIncrease = .05f;
    new Rigidbody rigidbody;
    static Vector3 pos;
    float deltaSpeed;

    public static Vector3 Pos
    {
        get
        {
            return pos;
        }
    }

    void OnEnable()
    {
        EventManager.StartListening(EventManager.EventType.OnLevelChanged, OnLevelChanged);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.EventType.OnLevelChanged, OnLevelChanged);
    }

    void OnLevelChanged()
    {
        speed *= 1 + speedPercentageIncrease;
    }

    // Use this for initialization
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        pos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        deltaSpeed = speed * Time.fixedDeltaTime;
        rigidbody.velocity = Vector3.zero;
        rigidbody.MovePosition(
            transform.position +
            new Vector3(
                deltaSpeed * Input.GetAxis("Horizontal"),
                0,
                deltaSpeed * Input.GetAxis("Vertical")
                )
            );
        pos = transform.position;
    }
}
