using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionEnemy : MonoBehaviour
{
    Generator GM;
    public PlayerController Player;
    GameObject PlayerGo;
    public bool Triggerd=false;
    
   
    
    void Start ()
    {
        GM = FindObjectOfType<Generator>();
        Player = FindObjectOfType<PlayerController>();
        PlayerGo = Player.gameObject;
	}

    bool isMoving = false;
    bool isAttacking = false;
    public float T = 0;
    void Update ()
    {
        if(Vector3.Distance(this.transform.position,PlayerGo.transform.position)<=6)
        {
            Triggerd = true;
            isMoving = true;
        }
        else
        {
            Triggerd = false;
        }
        if (Vector3.Distance(this.transform.position, PlayerGo.transform.position) <= 1)
        {
            
            isMoving = false;
            isAttacking = true;
        }
        
        if(GM.IsPlayerTurn==false)
        {
            T += Time.deltaTime;
            if (Triggerd)
            {
                if (isMoving)
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position, PlayerGo.transform.position,0.1f);
                    
                }
                

            }
            if (T > 2)
            {
                isMoving = false;
                GM.IsPlayerTurn = true;
            }

        }
        else
        {
            
        }
		
	}
}
