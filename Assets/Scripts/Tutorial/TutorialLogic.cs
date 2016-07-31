using UnityEngine;
using System.Collections;

public class TutorialLogic : MonoBehaviour {
    Transform playerGeom;    

    [Header("Bullets")]
    [SerializeField]
    float cooldownTime = .1f;
    [SerializeField]
    float bulletSpeed = 20;
    [SerializeField]
    int currentBulletsShot = 1;
    [SerializeField]
    MuzzleLogic muzzleLogic;

    float currentTime;
    bool canShootMissiles = true;

    [Header("Movement")]
    [SerializeField]
    float speed = 5;
    [Range(0f, 1f)]
    new Rigidbody rigidbody;
    float deltaSpeed;

    static TutorialLogic tutorialLogic;

    public static TutorialLogic instance
    {
        get
        {
            if (!tutorialLogic)
            {
                tutorialLogic = FindObjectOfType<TutorialLogic>();
                if (!tutorialLogic)
                {
                    Debug.LogError("There needs to be one active TutorialLogic script on a GameObject in your scene.");
                }
                else
                {
                    tutorialLogic.Init();
                }
            }

            return tutorialLogic;
        }
    }

    public static float Speed
    {
        get
        {
            return instance.bulletSpeed;
        }

        set
        {
            instance.bulletSpeed = value;
        }
    }

    void Init()
    {

    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerGeom = transform.GetChild(0);

        playerGeom = transform.GetChild(0);
        currentTime = cooldownTime;
        UpdateMuzzles();
    }

    void UpdateMuzzles()
    {
        int currentMuzzles = muzzleLogic.CurrentMuzzles;
        for (int i = 0; i < currentBulletsShot - currentMuzzles; i++)
        {
            muzzleLogic.AddMuzzle();
        }
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

    void Update()
    {
        Vector3 mouse = Input.mousePosition;
        mouse.z = Camera.main.transform.position.y - playerGeom.position.y;
        playerGeom.LookAt(Camera.main.ScreenToWorldPoint(mouse));
        if (Input.GetMouseButton(0) && currentTime <= 0)
        {
            currentTime = cooldownTime;
            muzzleLogic.Shoot();            
        }
        if (currentTime > 0)
            currentTime -= Time.deltaTime;
        if (Input.GetMouseButtonDown(1))
        {
            if (canShootMissiles)
            {
                muzzleLogic.ShootMissile();
            }
        }
    }
}
