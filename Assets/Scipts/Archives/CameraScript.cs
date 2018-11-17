using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    Vector3 offset;
    Transform Player;
    Quaternion MYstartRotation;
	void Start ()
    {
        /* Player = FindObjectOfType<PlayerController>().gameObject.transform;
         offset = this.transform.position - Player.transform.position;*/
        MYstartRotation = this.transform.rotation;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // this.transform.position = Player.transform.position + offset;
        if (Input.GetKey(KeyCode.W))
        {
            this.gameObject.GetComponent<Camera>().fieldOfView += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.gameObject.GetComponent<Camera>().fieldOfView -= 1;
        }
        if (Input.GetKey(KeyCode.X))
        {
            this.gameObject.GetComponent<Camera>().fieldOfView = 60;
            this.transform.rotation = MYstartRotation;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            this.gameObject.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.Euler(-135f, 270f, -1f), 0.1f);
        }
        if (Input.GetKey(KeyCode.E))
        {
            this.gameObject.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.Euler(-135f, 90f, -1f), 0.1f);
        }
    }
}
