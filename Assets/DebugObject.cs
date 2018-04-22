using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugObject : MonoBehaviour
{

    public GameObject EnemyPrefab;
	void Start ()
	{
	    EnemySpawner.Instance.Spawn(EnemyPrefab, 5);	
	}
	

}
