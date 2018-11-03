using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //controle du joueur
    //on clic sur une thuile et tu fait un pathfinding pour t<y rendre
    Generator GM;
    public GameObject TileClicked;
    public TileInfo tiClicked;

    GameObject myPresentTile;
    TileInfo myPresentTileInfo;
	void Start ()
    {
        GM = FindObjectOfType<Generator>();
	}
	
	
	void Update ()
    {
		
	}
    public void PathFinding()
    {
        while(myPresentTileInfo!=tiClicked)
        {
            TileInfo[] myNeighboors = FindLegalNeighboors();
            TileInfo NextStep = FindBestNeighboor(myNeighboors, tiClicked);
            Moveto(NextStep);
        }
        
    }
    void Moveto(TileInfo ti)
    {
        Vector3 myPosition = this.transform.position;
        Vector3 Destination = new Vector3(ti.PositionX, this.transform.position.y, ti.PositionY);
        while(this.transform.position!=Destination)
        {
            Vector3.MoveTowards(myPosition,Destination,0.2f);
        }
        myPresentTileInfo = ti;
    }
    TileInfo[] FindLegalNeighboors()//Go ou Tile info?
    {
        TileInfo mytile = myPresentTileInfo;
        TileInfo[] Voisins = new TileInfo[4];
        int[] coo = new int[2];
        coo[0] = mytile.PositionX;
        coo[1] = mytile.PositionY+1;
        Voisins[0] = GM.mapCootoTI[coo];
        coo[0] = mytile.PositionX+1;
        coo[1] = mytile.PositionY;
        Voisins[1] = GM.mapCootoTI[coo];
        coo[0] = mytile.PositionX;
        coo[1] = mytile.PositionY - 1;
        Voisins[2] = GM.mapCootoTI[coo];
        coo[0] = mytile.PositionX-1;
        coo[1] = mytile.PositionY;
        Voisins[3] = GM.mapCootoTI[coo];

        for (int i = 0; i < 4; i++)
        {
            if(Voisins[i].type==100)
            {
                Voisins[i] = null;
            }
        }
        return Voisins;

    }
    TileInfo FindBestNeighboor(TileInfo[] Voisins,TileInfo Goal)
    {
        TileInfo BestNeighboor=null;
        float GoalDistance = 9999;
        for (int i = 0; i < Voisins.Length; i++)
        {
            float Distance = Vector2.Distance(new Vector2(Voisins[i].PositionX, Voisins[i].PositionY), new Vector2(Voisins[i].PositionX, Voisins[i].PositionY));
            if (Voisins[i]!=null) 
            {
                if (BestNeighboor == null) 
                {
                    GoalDistance = Distance;
                    BestNeighboor = Voisins[i];
                }
            }
            else
            {
                if(Distance<GoalDistance)
                {
                    BestNeighboor = Voisins[i];
                    GoalDistance = Distance;
                }

            }
        }
        return BestNeighboor;
    }
    
}
