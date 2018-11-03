﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
public class PlayerController : MonoBehaviour
{

    //controle du joueur
    //on clic sur une thuile et tu fait un pathfinding pour t<y rendre
    Generator GM;
    public GameObject TileClicked;
    public TileInfo tiClicked;

    //GameObject myPresentTile;
    public TileInfo myPresentTileInfo;

    public int Porte = 5;
    

	void Start ()
    {
        
        
        GM = FindObjectOfType<Generator>();
        
    }

    bool isMoving = false;
    float T=0;
	void Update ()
    {
        if(isMoving)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(tiClicked.PositionX, 0.5f, tiClicked.PositionY), 0.2f);
            T += Time.deltaTime;
            
        }
        if(T>5)
        {
            
            isMoving = false;
            
        }

    }
    public void MoveMan()
    {
        if (GM.IsPlayerTurn)
        {

            if (TileClicked != null)
            {
                if (FindIfLegal(tiClicked))
                {
                    T = 0;
                    isMoving = true;
                    myPresentTileInfo = tiClicked;
                    TileClicked = null;
                    GM.IsPlayerTurn = false;
                    GM.ResetEnemyTurn();
                }
            }
        }
    }
    bool FindIfLegal(TileInfo ti)
    {
        /* if(DetectBlockingWay(myPresentTileInfo,ti))
         {
             return false;
         }*/
         if(Vector2.Distance(new Vector2(this.transform.position.x,this.transform.position.z),new Vector2(ti.PositionX,ti.PositionY))>5)
         {
            return false;
         }
        if (Vector2.Distance(new Vector2(this.transform.position.x, this.transform.position.z), new Vector2(ti.PositionX, ti.PositionY)) < 5)
        {
            if (BlockingWay2(ti)==false)
            {
                return true;
            }
        }
        if (BlockingWay2(ti))
        {
            return false;
        }
        if (ti.type==100||ti.type==120||ti.type==110||ti.type==200)
        {
            return false;
        }
        if(ti.PositionX <= myPresentTileInfo.PositionX + Porte && ti.PositionX >= myPresentTileInfo.PositionX - Porte)
        {
            if(ti.PositionY <= myPresentTileInfo.PositionY + Porte && ti.PositionY >= myPresentTileInfo.PositionY - Porte)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
           

        
    }
    bool BlockingWay2(TileInfo ti)
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position,new Vector3(ti.PositionX,0.5f,ti.PositionY), out hit, 5f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
             
            if(Blocking(GM.mapGOtoTI[hit.collider.gameObject].type))
            {
                Debug.Log("Did Hit");
                return true;

            }
            else
            {
                print(GM.mapGOtoTI[hit.collider.gameObject].type);
                return false;
            }
            
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            
            return false;
        }
    }
    bool Blocking(int type)
    {
        if (type == 100 || type == 120 || type == 110 || type == 200)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    bool DetectBlockingWay(TileInfo Depart,TileInfo Destination)
    {
        List<TileInfo> CeQuiSeTrouveEntreLeDepartEtLaDestination=new List<TileInfo>();
        TileInfo Checking;
        if(myPresentTileInfo.PositionX<Destination.PositionX)
        {
            for (int x = myPresentTileInfo.PositionX; x < Destination.PositionX; x++)
            {
                if (myPresentTileInfo.PositionY > Destination.PositionY)
                {
                    for (int y = myPresentTileInfo.PositionY; y < Destination.PositionY; y++)
                    {
                        Checking = GM.mapCootoTI[GM.CootoString(x, y)];
                        if(Blocking(Checking.type))
                        {
                            return false;
                        }
                    }
                }
                if (myPresentTileInfo.PositionY < Destination.PositionY)
                {
                    for (int y = Destination.PositionY; y < myPresentTileInfo.PositionY; y++)
                    {
                        Checking = GM.mapCootoTI[GM.CootoString(x, y)];
                        if (Blocking(Checking.type))
                        {
                            return false;
                        }
                    }
                }

            }
        }
        if (myPresentTileInfo.PositionX > Destination.PositionX)
        {
            for (int x = Destination.PositionX; x < myPresentTileInfo.PositionX; x++)
            {
                if (myPresentTileInfo.PositionY > Destination.PositionY)
                {
                    for (int y = myPresentTileInfo.PositionY; y < Destination.PositionY; y++)
                    {
                        Checking = GM.mapCootoTI[GM.CootoString(x, y)];
                        if (Blocking(Checking.type))
                        {
                            return false;
                        }
                    }
                }
                if (myPresentTileInfo.PositionY < Destination.PositionY)
                {
                    for (int y = Destination.PositionY; y < myPresentTileInfo.PositionY; y++)
                    {
                        Checking = GM.mapCootoTI[GM.CootoString(x, y)];
                        if (Blocking(Checking.type))
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return true;

    }


    void MoveLeft()
    {
        Vector3 Myposition = this.transform.position;
        Vector3 Destination = this.transform.position + new Vector3(-1, 0, 0);
        for (int i = 0; i < 5; i++)    
        {
            this.transform.position = Vector3.MoveTowards(Myposition, Destination,0.2f);
            
        }
        myPresentTileInfo = GM.mapCootoTI[GM.CootoString(myPresentTileInfo.PositionX - 1, myPresentTileInfo.PositionY)];

    }
    void MoveRight()
    {
        Vector3 Myposition = this.transform.position;
        Vector3 Destination = this.transform.position + new Vector3(+1, 0, 0);
        for (int i = 0; i < 5; i++)
        {
            this.transform.position = Vector3.MoveTowards(Myposition, Destination, 0.2f);
        }
        myPresentTileInfo = GM.mapCootoTI[GM.CootoString(myPresentTileInfo.PositionX + 1, myPresentTileInfo.PositionY)];
    }
    void Moveup()
    {
        Vector3 Myposition = this.transform.position;
        Vector3 Destination = this.transform.position + new Vector3(0, 0, 1);
        for (int i = 0; i < 5; i++)
        {
            this.transform.position = Vector3.MoveTowards(Myposition, Destination, 0.2f);
        }
        myPresentTileInfo = GM.mapCootoTI[GM.CootoString(myPresentTileInfo.PositionX , myPresentTileInfo.PositionY+1)];
    }
    void Movedown()
    {
        Vector3 Myposition = this.transform.position;
        Vector3 Destination = this.transform.position + new Vector3(0, 0, -1);
        for (int i = 0; i < 5; i++)
        {
            this.transform.position = Vector3.MoveTowards(Myposition, Destination, 0.2f);
        }
        myPresentTileInfo = GM.mapCootoTI[GM.CootoString(myPresentTileInfo.PositionX, myPresentTileInfo.PositionY-1)];
    }








    /*public void Pathfinding()
    {
        float Distance = Vector2.Distance(new Vector2(myPresentTileInfo.PositionX, myPresentTileInfo.PositionY), new Vector2(tiClicked.PositionX, tiClicked.PositionY));
        if (Distance<=5)
        {
            TileInfo[] myneighbors = FindLegalNeighboors();
            foreach (TileInfo ti in myneighbors)
            {
                print(ti.PositionX + "," + ti.PositionY);
            }
        }
    }
    TileInfo[] FindLegalNeighboors()//Go ou Tile info?
    {
        TileInfo mytile = myPresentTileInfo;
        TileInfo[] Voisins = new TileInfo[4];
        int x = myPresentTileInfo.PositionX;
        int y = myPresentTileInfo.PositionY + 1;
        string coo = GM.CootoString(x, y);
        if (GM.mapCootoTI.ContainsKey(coo))
        {
            Voisins[0] = GM.mapCootoTI[coo];
        }

        x = myPresentTileInfo.PositionX+1;
        y = myPresentTileInfo.PositionY;
        coo = GM.CootoString(x, y);
        if (GM.mapCootoTI.ContainsKey(coo))
        {
            Voisins[1] = GM.mapCootoTI[coo];
        }

        x = myPresentTileInfo.PositionX;
        y = myPresentTileInfo.PositionY - 1;
        coo = GM.CootoString(x, y);
        if (GM.mapCootoTI.ContainsKey(coo))
        {
            Voisins[2] = GM.mapCootoTI[coo];
        }
        x = myPresentTileInfo.PositionX-1;
        y = myPresentTileInfo.PositionY;
        coo = GM.CootoString(x, y);
        if (GM.mapCootoTI.ContainsKey(coo))
        {
            Voisins[3] = GM.mapCootoTI[coo];
        }


        for (int i = 0; i < Voisins.Length; i++)
        {
            if (Voisins[i] != null)
            {

                if (Voisins[i].type == 100)
                {
                    Voisins[i] = null;
                }
            }

        }
        return Voisins;

    }
    
    public void PathFinding()
    {
        int x = 0;
        print("IN path finding the tile cliked is" + tiClicked.PositionX + "," + tiClicked.PositionY);
        while(myPresentTileInfo!=tiClicked)
        {
            x++;
            Thread.Sleep(500);
            TileInfo[] myNeighboors = FindLegalNeighboors();

            TileInfo NextStep = FindBestNeighboor(myNeighboors, tiClicked);
            //print("MyBestNeighbooris" + NextStep.PositionX +"  "+ NextStep.PositionY);
            if(NextStep!=null)
            {
                print("Movingtonextstep");
                Moveto(NextStep);
            }
            else
            {
                print("I have no next Step");
                break;
            }
            if(x>=50)
            {
                print("TimeOut" + x);
                break;
            }
        }
        print(myPresentTileInfo.PositionX + "," + myPresentTileInfo.PositionY);
        TileClicked = null;
        
    }
    void Moveto(TileInfo ti)
    {
        
        Vector3 myPosition = this.transform.position;
        Vector3 Destination = new Vector3(ti.PositionX, this.transform.position.y, ti.PositionY);
        print("Destinationis:" + ti.PositionX + "," + ti.PositionY);
        int x = 0;
        print("Moving from" + myPosition.ToString() + " to " + Destination.ToString());
        while (this.transform.position!=Destination)
        {
            x++;
            this.transform.position = Destination;
            
            if(x>=5)
            {
                break;
            }
        }
        myPresentTileInfo = ti;
    }
    /*TileInfo[] FindLegalNeighboors()//Go ou Tile info?
    {
        TileInfo mytile = myPresentTileInfo;
        TileInfo[] Voisins = new TileInfo[4];
        int x = myPresentTileInfo.PositionX;
        int y = myPresentTileInfo.PositionY+1;
        string coo = GM.CootoString(x, y);
        if (GM.mapCootoTI.ContainsKey(coo))
        {
            Voisins[0] = GM.mapCootoTI[coo];
        }

        x = myPresentTileInfo.PositionX;
        y = myPresentTileInfo.PositionY + 1;
        coo = GM.CootoString(x, y);
        if (GM.mapCootoTI.ContainsKey(coo))
        {
            Voisins[1] = GM.mapCootoTI[coo];
        }

        x = myPresentTileInfo.PositionX;
        y = myPresentTileInfo.PositionY + 1;
        coo = GM.CootoString(x, y);
        if (GM.mapCootoTI.ContainsKey(coo))
        {
            Voisins[2] = GM.mapCootoTI[coo];
        }
        x = myPresentTileInfo.PositionX;
        y = myPresentTileInfo.PositionY + 1;
        coo = GM.CootoString(x, y);
        if (GM.mapCootoTI.ContainsKey(coo))
        {
            Voisins[3] = GM.mapCootoTI[coo];
        }
        

        for (int i = 0; i < Voisins.Length; i++)
        {
            if(Voisins[i]!=null)
            {
               
                if (Voisins[i].type == 100)
                {
                    //Voisins[i] = null;
                }
            }
            
        }
        return Voisins;

    }*/
    /*TileInfo FindBestNeighboor(TileInfo[] Voisins,TileInfo Goal)
    {

        TileInfo BestNeighboor=null;
        float GoalDistance = 9999;
        int x = 0;
        for (int i = 0; i < Voisins.Length; i++)
        {
            x++;

            if(Voisins[i]!=null)
            {

                float Distance = Vector2.Distance(new Vector2(Voisins[i].PositionX, Voisins[i].PositionY), new Vector2(Voisins[i].PositionX, Voisins[i].PositionY));
                if (Voisins[i] != null)
                {
                    if (BestNeighboor == null)
                    {
                        GoalDistance = Distance;
                        BestNeighboor = Voisins[i];
                    }
                    else
                    {
                        if (Distance < GoalDistance)
                        {
                            BestNeighboor = Voisins[i];
                            GoalDistance = Distance;
                        }

                    }
                }
                
            }
            else
            {
                print("Mon voisin " + i + " est null");
            }
            
        }
        return BestNeighboor;
    }*/

}
