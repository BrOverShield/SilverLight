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
        Player.TileClicked = this.gameObject;
        Player.Clicked();
        
    }
}
