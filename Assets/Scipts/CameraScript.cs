using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    Vector3 offset;
    Transform Player;
	void Start ()
    {
       /* Player = FindObjectOfType<PlayerController>().gameObject.transform;
        offset = this.transform.position - Player.transform.position;*/
	}
	
	// Update is called once per frame
	void Update ()
    {
       // this.transform.position = Player.transform.position + offset;
	}
}
