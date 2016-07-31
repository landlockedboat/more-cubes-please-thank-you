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
    float deltaSpeed;

    static PlayerMovement playerMovement;

    public static PlayerMovement instance
    {
        get
        {
            if (!playerMovement)
            {
                playerMovement = FindObjectOfType<PlayerMovement>();
                if (!playerMovement)
                {
                    Debug.LogError("There needs to be one active PlayerMovement script on a GameObject in your scene.");
                }
                else
                {
                    playerMovement.Init();
                }
            }

            return playerMovement;
        }
    }

    void Init()
    {

    }

    public static Vector3 Pos
    {
        get
        {
            return instance.transform.position;
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

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

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
    }
}
