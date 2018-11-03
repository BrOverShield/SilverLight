using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //controle du joueur
    //on clic sur une thuile et tu fait un pathfinding pour t<y rendre
    Generator GM;
	void Start ()
    {
        GM = FindObjectOfType<Generator>();
	}
	
	
	void Update ()
    {
		
	}
    void SelectTile()
    {

    }
    void MoveHere(GameObject tile)//Go ou Tile info?
    {
        
    }
    
}
