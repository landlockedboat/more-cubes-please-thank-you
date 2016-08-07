using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    float speed = 5;
    [Range(0f, 1f)]
    [SerializeField]
    float speedPercentageIncrease = .05f;
    [SerializeField]
    float movementOffset = 1f;
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
            if (!playerMovement)
                return Vector3.down;
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
        playerMovement = this;
    }

    void FixedUpdate()
    {
        deltaSpeed = speed * Time.fixedDeltaTime;
        rigidbody.velocity = Vector3.zero;
        float xMovement = Input.GetAxis("Horizontal");
        float yMovement = Input.GetAxis("Vertical");
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        if (playerScreenPos.x + movementOffset >= Screen.width)
        {
            if (xMovement > 0)
                xMovement = 0;
        }
        else if (playerScreenPos.x - movementOffset <= 0)
        {
            if (xMovement < 0)
                xMovement = 0;
        }
        if(playerScreenPos.y + movementOffset >= Screen.height)
        {
            if (yMovement > 0)
                yMovement = 0;
        }
        else if(playerScreenPos.y - movementOffset <= 0)
        {
            if (yMovement < 0)
                yMovement = 0;
        }



        rigidbody.MovePosition(
            transform.position +
            new Vector3(
                deltaSpeed * xMovement,
                0,
                deltaSpeed * yMovement
                )
            );
    }
}
