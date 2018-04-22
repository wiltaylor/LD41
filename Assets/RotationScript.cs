using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{

    public float Speed = 5f;

	void Update ()
	{
		transform.Rotate(Vector3.up, Speed * Time.deltaTime);
	}
}
