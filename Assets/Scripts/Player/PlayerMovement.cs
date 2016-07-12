using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    float speed = 5;
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

    // Use this for initialization
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        deltaSpeed = speed * Time.fixedDeltaTime;
        pos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
