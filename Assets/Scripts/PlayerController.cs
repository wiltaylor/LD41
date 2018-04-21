using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public float RotationSpeed;
        
    private Rigidbody _rigidbody;

	void Start ()
	{
	    _rigidbody = GetComponent<Rigidbody>();

	}
	
	void Update ()
    {
        if (Input.GetAxis("Horizontal") > 0.01f)
        {

            _rigidbody.AddRelativeTorque(Vector3.up * (RotationSpeed * Time.deltaTime), ForceMode.VelocityChange);
        }

        if (Input.GetAxis("Horizontal") < -0.01f)
        {
            _rigidbody.AddRelativeTorque(Vector3.up * (-RotationSpeed * Time.deltaTime), ForceMode.VelocityChange);
        }

        if(Input.GetAxis("Horizontal") < 0.01f && Input.GetAxis("Horizontal") > -0.01f)
            _rigidbody.angularVelocity = Vector3.zero;

        if (Input.GetAxis("Vertical") > 0.01f)
        {
            _rigidbody.MovePosition(transform.position + transform.forward * (Speed * Time.deltaTime));
        }

        if (Input.GetAxis("Vertical") < -0.01f)
        {
            _rigidbody.MovePosition(transform.position + -transform.forward * (Speed * Time.deltaTime));
        }

        if (Input.GetAxis("SideStep") > 0.01f)
        {
            _rigidbody.MovePosition(transform.position + transform.right * (Speed * Time.deltaTime));

        }

        if (Input.GetAxis("SideStep") < -0.01f)
        {
            _rigidbody.MovePosition(transform.position + -transform.right * (Speed * Time.deltaTime));
        }
    }
}
