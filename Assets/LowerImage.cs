using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerImage : MonoBehaviour {

	Vector3 startPosition;
	// Use this for initialization
	void Start () {
		startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position = startPosition - new Vector3(0, 10, 0);
	}
}
