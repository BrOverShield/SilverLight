using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehavior : MonoBehaviour
{
    
    PlayerController Player;
    private void Start()
    {
        Player = FindObjectOfType<PlayerController>();
    }
    private void OnMouseDown()
    {

        print(GameController.GM.mapGOtoTI[this.gameObject].CooH);
        print(this.transform.position);
        Player.TileClicked = this.gameObject;
        Player.ClickControl();
    }
    
    
}
