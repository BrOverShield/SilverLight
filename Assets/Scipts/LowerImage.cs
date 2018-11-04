using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerImage : MonoBehaviour {

	Vector3 startPosition;
	// Use this for initialization
	void Start () {
		startPosition = transform.position;
		StartCoroutine(DoFade());
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	IEnumerator DoFade ()
	{
		CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
		while (transform.position.y != 0)
		{
			//transform.position = startPosition - new Vector3(0, 10, 0);
			yield return null;
		}
		yield return null;
	}
}
