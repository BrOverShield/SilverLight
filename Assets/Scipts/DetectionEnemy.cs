using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionEnemy : MonoBehaviour
{
    public PlayerController Player;
    GameObject PlayerGo;
    bool Triggerd=false;
    private void OnTriggerEnter(Collider other)    
    {
        if(other.gameObject==PlayerGo)
        {

            Triggerd = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == PlayerGo)
        {

            Triggerd = false;
        }
    }
    void MoveToward()
    {

        Transform target = PlayerGo.transform;
        Vector3 MyPosition = this.transform.parent.transform.position;
        Vector3.MoveTowards(MyPosition, target.position, 0.2f);
    }
    void Start ()
    {
        Player = FindObjectOfType<PlayerController>();
        PlayerGo = Player.gameObject;
	}
	
	
	void Update ()
    {
        
		if(Triggerd)
        {
            MoveToward();
        }
	}
}
