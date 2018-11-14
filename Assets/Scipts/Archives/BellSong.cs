using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class BellSong : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    public void BellSongF()
    {
        GetComponent<AudioSource>().Play();
        this.transform.rotation = Quaternion.Euler(-38.612f, -89.97401f, 89.984f);
        Thread.Sleep(500);
        GetComponent<AudioSource>().Play();
        this.transform.rotation = Quaternion.Euler(-150.576f, -90.02301f, 90.010f);
        Thread.Sleep(500);
        GetComponent<AudioSource>().Play();
        this.transform.rotation = Quaternion.Euler(-92.23599f, -269.481f, 269.481f);
    }
}
