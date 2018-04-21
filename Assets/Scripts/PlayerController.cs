using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public float RotationSpeed;
        
    private Rigidbody _rigidbody;
    private PlayerCombatController _combatController;


	void Start ()
	{
	    _rigidbody = GetComponent<Rigidbody>();
	    _combatController = GetComponent<PlayerCombatController>();
	}
	
	void Update ()
	{

	    var moveVector = transform.position;

        if (Input.GetAxis("Horizontal") > 0.01f)
        {
            _rigidbody.AddTorque(Vector3.up * (RotationSpeed * Time.deltaTime), ForceMode.Force);
        }

        if (Input.GetAxis("Horizontal") < -0.01f)
        {
            _rigidbody.AddTorque(Vector3.up * (-RotationSpeed * Time.deltaTime), ForceMode.Force);
        }

        if (Input.GetAxis("Vertical") > 0.01f)
        {
            moveVector += transform.forward * (Speed * Time.deltaTime);
        }

        if (Input.GetAxis("Vertical") < -0.01f)
        {
            moveVector += -transform.forward * (Speed * Time.deltaTime);
        }

        if (Input.GetAxis("SideStep") > 0.01f)
        {
            moveVector += transform.right * (Speed * Time.deltaTime);
        }

        if (Input.GetAxis("SideStep") < -0.01f)
        {
            moveVector += -transform.right * (Speed * Time.deltaTime);
        }

        _rigidbody.MovePosition(moveVector);

        if (Input.GetButton("Fire"))
        {
            _combatController.Shoot();
        }

        //Stosp weird movment caused by rigidbody.
	    _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
	    
	}
}
