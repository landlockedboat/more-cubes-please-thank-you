using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private float speed = 5;
    private new Rigidbody rigidbody;
    float deltaSpeed;
    // Use this for initialization
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        deltaSpeed = speed * Time.fixedDeltaTime;

    }

    // Update is called once per frame
    void FixedUpdate()
    {

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
