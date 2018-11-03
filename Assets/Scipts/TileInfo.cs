using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo
{
    public int PositionX;
    public int PositionY;
    public int type;
    public int Lumiere;
    public int Etage;
    public GameObject myGo;
    public TileInfo(int x, int y,Color P,int etage)
    {
        
        type = (int)(P.r*255);
        Lumiere = (int)(P.g*255);
        Etage = etage;
        PositionX = x;
        PositionY = y;

    }
	
}
