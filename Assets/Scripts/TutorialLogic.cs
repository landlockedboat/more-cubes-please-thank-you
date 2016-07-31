using UnityEngine;
using System.Collections;

public class TutorialLogic : MonoBehaviour {

    [SerializeField]
    float speed = 5;
    [Range(0f, 1f)]
    new Rigidbody rigidbody;
    float deltaSpeed;

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
