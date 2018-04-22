using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public float RotationSpeed;
    public GameObject MeatFountain;
        
    private Rigidbody _rigidbody;
    private MeshRenderer _render;
    private PlayerCombatController _combatController;


	void Start ()
	{
	    _rigidbody = GetComponent<Rigidbody>();
	    _combatController = GetComponent<PlayerCombatController>();
	    _render = GetComponent<MeshRenderer>();
	}
	
	void Update ()
	{
	    if (PlayerStats.Instance.HP <= 0)
	    {
	        _render.enabled = false;
            MeatFountain.SetActive(true);

	        _rigidbody.velocity = Vector3.zero;
	        _rigidbody.angularVelocity = Vector3.zero;


            return;
	    }


	    var moveVector = Vector3.zero;
	    var angleVector = Vector3.zero;

        if (Input.GetAxis("Horizontal") > 0.01f)
        {
            
            transform.Rotate(Vector3.up, RotationSpeed * Time.deltaTime);
        }

        if (Input.GetAxis("Horizontal") < -0.01f)
        {
            _rigidbody.AddTorque(Vector3.up * (-RotationSpeed * Time.deltaTime), ForceMode.Force);

            transform.Rotate(Vector3.up, -RotationSpeed * Time.deltaTime);
        }

        if (Input.GetAxis("Vertical") > 0.01f)
        {
            moveVector += transform.forward; //* (Speed * Time.deltaTime);
        }

        if (Input.GetAxis("Vertical") < -0.01f)
        {
            moveVector += -transform.forward; //* (Speed * Time.deltaTime);
        }

        if (Input.GetAxis("SideStep") > 0.01f)
        {
            moveVector += transform.right; //* (Speed * Time.deltaTime);
        }

        if (Input.GetAxis("SideStep") < -0.01f)
        {
            moveVector += -transform.right; // * (Speed * Time.deltaTime);
        }

        //_rigidbody.MovePosition(moveVector);

        if (Input.GetButton("Fire"))
        {
            _combatController.Shoot();
        }

        //Stop weird movment caused by rigidbody.
	    _rigidbody.velocity = moveVector * Speed;
	    _rigidbody.angularVelocity = Vector3.zero;

	}
}
