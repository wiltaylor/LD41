using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackableObject : MonoBehaviour
{


    public Color Colour;

	// Use this for initialization
	void Start ()
	{
		OffScreenTracker.Instance.AddTrackableObject(gameObject, Colour);
	}
	
}
