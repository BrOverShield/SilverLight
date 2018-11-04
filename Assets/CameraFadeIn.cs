using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFadeIn : MonoBehaviour {

	public float Duration = 1;
	private float t = 0;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		t+=Time.deltaTime;
		float kwame = t = t*t * (3f - 2f*t);
		//getComponent<Image>().Color = Color.lerp(Color.white, new Color(1,1,1,0),kwame);
	}
}