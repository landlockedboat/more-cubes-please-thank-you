using UnityEngine;
using System.Collections;

public class GravityModifier : MonoBehaviour {
    new Rigidbody rigidbody;
    [SerializeField]
    float gravityMultiplier = 1f;
    [SerializeField]
    bool limitVerticalSpeed;
    [SerializeField]
    float minimumVerticalSpeed = -10f;


    void Start () {
        rigidbody = GetComponent<Rigidbody>();
    }
	
	void Update () {
        if(rigidbody.velocity.y > minimumVerticalSpeed)
        {
            Vector3 forceToAdd = Physics.gravity * rigidbody.mass * gravityMultiplier;
            rigidbody.AddForce(forceToAdd);
        }
	}
}
