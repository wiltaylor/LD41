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
            transform.Rotate(Vector3.up, RotationSpeed * Time.deltaTime);

        }

        if (Input.GetAxis("Horizontal") < -0.01f)
        {
            transform.Rotate(Vector3.up, -RotationSpeed * Time.deltaTime);
        }

        if (Input.GetAxis("Vertical") > 0.01f)
        {
            transform.position = transform.position + transform.forward * (Speed * Time.deltaTime);

        }

        if (Input.GetAxis("Vertical") < -0.01f)
        {
            transform.position = transform.position + -transform.forward * (Speed * Time.deltaTime);

        }

        if (Input.GetAxis("SideStep") > 0.01f)
        {
            transform.position = transform.position + transform.right * (Speed * Time.deltaTime);

        }

        if (Input.GetAxis("SideStep") < -0.01f)
        {
            transform.position = transform.position + -transform.right * (Speed * Time.deltaTime);
        }
    }
}
