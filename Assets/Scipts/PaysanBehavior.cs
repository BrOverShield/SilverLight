using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaysanBehavior : MonoBehaviour
{

    
   public TileInfo mytile;
   bool IDontKnowWhereIam=true;
   Generator GM;
   bool isTRiggerd = false;
   bool doorisopen=false;
    int TalkingTimer=5;
    bool ElReverso = false;
    PlayerController player;
    int DMG=5;
    public int Life = 30;
    public int id = 0;
    public GameObject SonDeClocheAlertePrefab;
    bool isAlerted = false;
    public GameObject Blood;
    public GameObject GuardPrefab;
    public List<GameObject> Guards=new List<GameObject>();
    public guardBehavior[] GuardsBehaviors;
    bool isactive=false;
   void Start ()
   {
       GM = FindObjectOfType<Generator>();
        player = FindObjectOfType<PlayerController>();
        GuardsBehaviors = FindObjectsOfType<guardBehavior>();
        foreach (guardBehavior GB in GuardsBehaviors)
        {
            Guards.Add(GB.gameObject);
        }
   }

   // Update is called once per frame
   void Update ()
   {
       if(IDontKnowWhereIam)
       {
            if(id==1)
            {
                if (GM.mapCootoTI.ContainsKey(GM.CootoString(16, 11)))
                {

                    mytile = GM.mapCootoTI[GM.CootoString(16, 11)];
                    IDontKnowWhereIam = false;
                }
            }
            if (id == 2)
            {
                if (GM.mapCootoTI.ContainsKey(GM.CootoString(4, 7)))
                {

                    mytile = GM.mapCootoTI[GM.CootoString(4, 7)];
                    IDontKnowWhereIam = false;
                }
            }
            if (id == 3)
            {
                if (GM.mapCootoTI.ContainsKey(GM.CootoString(17, 1)))
                {

                    mytile = GM.mapCootoTI[GM.CootoString(17, 1)];
                    IDontKnowWhereIam = false;
                }
                this.DMG += 5;
                
            }
            if (id == 4)
            {
                if (GM.mapCootoTI.ContainsKey(GM.CootoString(16, 1)))
                {

                    mytile = GM.mapCootoTI[GM.CootoString(16, 1)];
                    IDontKnowWhereIam = false;
                }
                this.DMG += 5;


            }
            if (id == 5)
            {
                if (GM.mapCootoTI.ContainsKey(GM.CootoString(18, 1)))
                {

                    mytile = GM.mapCootoTI[GM.CootoString(18, 1)];
                    IDontKnowWhereIam = false;
                }
                this.DMG += 5;


            }
        }
       else
       {

       }
       if(Life<=0)
        {
            player.CurentLife += 80;
            Instantiate(Blood, this.transform.position, Quaternion.identity);
            mytile.HasBlood = true;
            Destroy(this.gameObject);
        }
   }
    bool HasBencalled = false;
    void calltheGards()
    {
        if(HasBencalled)
        {
            return;
        }
        foreach (PaysanBehavior p in GM.Paysans)
        {
            if(p.id==3)
            {
                p.isactive = true;
            }
        }
        HasBencalled = true;
    }
    void Attac()
    {
        if(Random.Range(1,10)<30)
        {
           GameObject Sound = Instantiate(SonDeClocheAlertePrefab, this.transform.position, Quaternion.identity);
            Sound.GetComponentInChildren<TextMesh>().text = "Die WhereWolf";
        }
        player.CurentLife -= DMG;
    }
    void SonneAlerte()
    {
        Instantiate(SonDeClocheAlertePrefab, this.transform.position, Quaternion.identity);
        calltheGards();
    }
    List<int[]> ReversePath = new List<int[]>();
   public void DoTurn()
   {
        
        if(GM.mapCootoTI.ContainsKey(GM.CootoString(mytile.PositionX,mytile.PositionY-1)))//up
        {
            TileInfo ti = GM.mapCootoTI[GM.CootoString(mytile.PositionX, mytile.PositionY - 1)];
            if(ti==player.myPresentTileInfo)
            {
                Attac();
                return;
            }
            if (GM.mapTItoGO[ti].GetComponentInChildren<TileUpdater>().HasPasDansLaNeige)//Si tu trouve des traces de pas
            {
                GameObject Sound = Instantiate(SonDeClocheAlertePrefab, this.transform.position, Quaternion.identity);
                Sound.GetComponentInChildren<TextMesh>().text = "Is that Wolf Print in the snow?";
                Sound.GetComponent<Timeout>().t = 1;
                isTRiggerd = true;
                int[] coo = { mytile.PositionX, mytile.PositionY - 1 };//vas vers ca
                ReversePath.Insert(0, coo);
                moveto(mytile.PositionX, mytile.PositionY-1);
                return;
            }
            if (ti.HasBlood)
            {
                GameObject Sound = Instantiate(SonDeClocheAlertePrefab, this.transform.position, Quaternion.identity);
                Sound.GetComponentInChildren<TextMesh>().text = "Help Someone Has Ben Killed! CALL THE GUARDS";
                Sound.GetComponent<Timeout>().t = 1;
                calltheGards();
                return;
            }

        }
        if (GM.mapCootoTI.ContainsKey(GM.CootoString(mytile.PositionX, mytile.PositionY + 1)))//down
        {
            TileInfo ti = GM.mapCootoTI[GM.CootoString(mytile.PositionX, mytile.PositionY + 1)];
            if (ti == player.myPresentTileInfo)
            {
                Attac();
                return;
            }
            if (GM.mapTItoGO[ti].GetComponentInChildren<TileUpdater>().HasPasDansLaNeige)//Si tu trouve des traces de pas
            {
                GameObject Sound = Instantiate(SonDeClocheAlertePrefab, this.transform.position, Quaternion.identity);
                Sound.GetComponentInChildren<TextMesh>().text = "Is that Wolf Print in the snow?";
                Sound.GetComponent<Timeout>().t = 1;
                isTRiggerd = true;
                int[] coo = { mytile.PositionX, mytile.PositionY + 1 };//vas vers ca
                ReversePath.Insert(0, coo);
                moveto(mytile.PositionX, mytile.PositionY + 1);
                return;
            }
            if (ti.HasBlood)
            {
                GameObject Sound = Instantiate(SonDeClocheAlertePrefab, this.transform.position, Quaternion.identity);
                Sound.GetComponentInChildren<TextMesh>().text = "Help Someone Has Ben Killed! CALL THE GUARDS";
                Sound.GetComponent<Timeout>().t = 1;
                calltheGards();
                return;
            }

        }
        if (GM.mapCootoTI.ContainsKey(GM.CootoString(mytile.PositionX+1, mytile.PositionY )))//left
        {
            TileInfo ti = GM.mapCootoTI[GM.CootoString(mytile.PositionX+1, mytile.PositionY)];
            if (ti == player.myPresentTileInfo)
            {
                Attac();
                return;
            }
            if (GM.mapTItoGO[ti].GetComponentInChildren<TileUpdater>().HasPasDansLaNeige)//Si tu trouve des traces de pas
            {

                GameObject Sound = Instantiate(SonDeClocheAlertePrefab, this.transform.position, Quaternion.identity);
                Sound.GetComponentInChildren<TextMesh>().text = "Is that Wolf Print in the snow?";
                Sound.GetComponent<Timeout>().t = 1;
                isTRiggerd = true;
                int[] coo = { mytile.PositionX+1, mytile.PositionY };//vas vers ca
                ReversePath.Insert(0, coo);
                moveto(mytile.PositionX+1, mytile.PositionY );
                return;
            }
            if (ti.HasBlood)
            {
                GameObject Sound = Instantiate(SonDeClocheAlertePrefab, this.transform.position, Quaternion.identity);
                Sound.GetComponentInChildren<TextMesh>().text = "Help Someone Has Ben Killed! CALL THE GUARDS";
                Sound.GetComponent<Timeout>().t = 1;
                calltheGards();
                return;
            }

        }
        if (GM.mapCootoTI.ContainsKey(GM.CootoString(mytile.PositionX - 1, mytile.PositionY)))//Right
        {
            TileInfo ti = GM.mapCootoTI[GM.CootoString(mytile.PositionX - 1, mytile.PositionY)];
            if (ti == player.myPresentTileInfo)
            {
                Attac();
                return;
            }
            if (GM.mapTItoGO[ti].GetComponentInChildren<TileUpdater>().HasPasDansLaNeige)//Si tu trouve des traces de pas
            {
                GameObject Sound = Instantiate(SonDeClocheAlertePrefab, this.transform.position, Quaternion.identity);
                Sound.GetComponentInChildren<TextMesh>().text = "Is that Wolf Print in the snow?";
                Sound.GetComponent<Timeout>().t = 1;
                isTRiggerd = true;
                int[] coo = { mytile.PositionX - 1, mytile.PositionY };//vas vers ca
                ReversePath.Insert(0, coo);
                moveto(mytile.PositionX - 1, mytile.PositionY);
                return;
            }
            if (ti.HasBlood)
            {
                GameObject Sound = Instantiate(SonDeClocheAlertePrefab, this.transform.position, Quaternion.identity);
                Sound.GetComponentInChildren<TextMesh>().text = "Help Someone Has Ben Killed! CALL THE GUARDS";
                Sound.GetComponent<Timeout>().t = 1;
                calltheGards();
                return;
            }

        }
        if (GM.mapCootoTI.ContainsKey(GM.CootoString(mytile.PositionX+1, mytile.PositionY - 1)))//upLeft
        {
            TileInfo ti = GM.mapCootoTI[GM.CootoString(mytile.PositionX+1, mytile.PositionY - 1)];
            if (ti == player.myPresentTileInfo)
            {
                Attac();
                return;
            }
            if (GM.mapTItoGO[ti].GetComponentInChildren<TileUpdater>().HasPasDansLaNeige)//Si tu trouve des traces de pas
            {
                GameObject Sound = Instantiate(SonDeClocheAlertePrefab, this.transform.position, Quaternion.identity);
                Sound.GetComponentInChildren<TextMesh>().text = "Is that Wolf Print in the snow?";
                Sound.GetComponent<Timeout>().t = 1;
                isTRiggerd = true;
                int[] coo = { mytile.PositionX+1, mytile.PositionY - 1 };//vas vers ca
                ReversePath.Insert(0, coo);
                moveto(mytile.PositionX+1, mytile.PositionY - 1);
                return;
            }
            if (ti.HasBlood)
            {
                GameObject Sound = Instantiate(SonDeClocheAlertePrefab, this.transform.position, Quaternion.identity);
                Sound.GetComponentInChildren<TextMesh>().text = "Help Someone Has Ben Killed! CALL THE GUARDS";
                Sound.GetComponent<Timeout>().t = 1;
                calltheGards();
                return;
            }
            
        }
        if (GM.mapCootoTI.ContainsKey(GM.CootoString(mytile.PositionX - 1, mytile.PositionY - 1)))//upRight
        {
            TileInfo ti = GM.mapCootoTI[GM.CootoString(mytile.PositionX - 1, mytile.PositionY - 1)];
            if (ti == player.myPresentTileInfo)
            {
                Attac();
                return;
            }
            if (GM.mapTItoGO[ti].GetComponentInChildren<TileUpdater>().HasPasDansLaNeige)//Si tu trouve des traces de pas
            {
                GameObject Sound = Instantiate(SonDeClocheAlertePrefab, this.transform.position, Quaternion.identity);
                Sound.GetComponentInChildren<TextMesh>().text = "Is that Wolf Print in the snow?";
                Sound.GetComponent<Timeout>().t = 1;
                isTRiggerd = true;
                int[] coo = { mytile.PositionX - 1, mytile.PositionY - 1 };//vas vers ca
                ReversePath.Insert(0, coo);
                moveto(mytile.PositionX - 1, mytile.PositionY - 1);
                return;
            }
            if (ti.HasBlood)
            {
                GameObject Sound = Instantiate(SonDeClocheAlertePrefab, this.transform.position, Quaternion.identity);
                Sound.GetComponentInChildren<TextMesh>().text = "Help Someone Has Ben Killed! CALL THE GUARDS";
                Sound.GetComponent<Timeout>().t = 1;
                calltheGards();
                return;
            }

        }
        if (GM.mapCootoTI.ContainsKey(GM.CootoString(mytile.PositionX - 1, mytile.PositionY + 1)))//DownRight
        {
            TileInfo ti = GM.mapCootoTI[GM.CootoString(mytile.PositionX - 1, mytile.PositionY + 1)];
            if (ti == player.myPresentTileInfo)
            {
                Attac();
                return;
            }
            if (GM.mapTItoGO[ti].GetComponentInChildren<TileUpdater>().HasPasDansLaNeige)//Si tu trouve des traces de pas
            {
                GameObject Sound = Instantiate(SonDeClocheAlertePrefab, this.transform.position, Quaternion.identity);
                Sound.GetComponentInChildren<TextMesh>().text = "Is that Wolf Print in the snow?";
                Sound.GetComponent<Timeout>().t = 1;
                isTRiggerd = true;
                int[] coo = { mytile.PositionX - 1, mytile.PositionY + 1 };//vas vers ca
                ReversePath.Insert(0, coo);
                moveto(mytile.PositionX - 1, mytile.PositionY + 1);
                return;
            }
            if (ti.HasBlood)
            {
                GameObject Sound = Instantiate(SonDeClocheAlertePrefab, this.transform.position, Quaternion.identity);
                Sound.GetComponentInChildren<TextMesh>().text = "Help Someone Has Ben Killed! CALL THE GUARDS";
                Sound.GetComponent<Timeout>().t = 1;
                calltheGards();
                return;
            }

        }
        if (GM.mapCootoTI.ContainsKey(GM.CootoString(mytile.PositionX + 1, mytile.PositionY + 1)))//DownLeft
        {
            TileInfo ti = GM.mapCootoTI[GM.CootoString(mytile.PositionX + 1, mytile.PositionY + 1)];
            if (ti == player.myPresentTileInfo)
            {
                Attac();
                return;
            }
            if (GM.mapTItoGO[ti].GetComponentInChildren<TileUpdater>().HasPasDansLaNeige)//Si tu trouve des traces de pas
            {
                GameObject Sound = Instantiate(SonDeClocheAlertePrefab, this.transform.position, Quaternion.identity);
                Sound.GetComponentInChildren<TextMesh>().text = "Is that Wolf Print in the snow?";
                Sound.GetComponent<Timeout>().t = 1;
                isTRiggerd = true;
                int[] coo = { mytile.PositionX + 1, mytile.PositionY + 1 };//vas vers ca
                ReversePath.Insert(0, coo);
                moveto(mytile.PositionX + 1, mytile.PositionY + 1);
                return;
            }
            if (ti.HasBlood)
            {
                GameObject Sound = Instantiate(SonDeClocheAlertePrefab, this.transform.position, Quaternion.identity);
                Sound.GetComponentInChildren<TextMesh>().text = "Help Someone Has Ben Killed! CALL THE GUARDS";
                Sound.GetComponent<Timeout>().t = 1;
                calltheGards();
                return;
            }

        }

        Path1();
        Path2();
        if(isactive)
        {
            Path3();
        }
        
   }
    void Path3()
    {
        if (id==3)
        {
            print("I am spartacus 2");
            //Reverse
            if (ElReverso)
            {
                RevervePath();
                return;
            }
            if (isTRiggerd)//si tu as suivit une piste et la perd
            {
                ElReverso = true;
            }
            //follow etineraire
            print("I am spartacus 3");
            print(mytile.PositionX + "," + mytile.PositionY);
            if (mytile.PositionX == 17)
            {
                print("I am spartacus 4");
                if (mytile.PositionY < 2)
                {
                    TalkingTimer = 5;
                    int[] coo = { mytile.PositionX, mytile.PositionY };
                    ReversePath.Insert(0, coo);
                    moveto(mytile.PositionX, mytile.PositionY + 1);
                    return;
                }
            }
            if (mytile.PositionY == 2)
            {
                if (mytile.PositionX > 12)
                {

                    int[] coo = { mytile.PositionX, mytile.PositionY };
                    ReversePath.Insert(0, coo);
                    moveto(mytile.PositionX - 1, mytile.PositionY);


                    return;
                }
            }
            if (mytile.PositionX == 12)
            {
                if (mytile.PositionY < 4)
                {
                    TalkingTimer = 5;
                    int[] coo = { mytile.PositionX, mytile.PositionY };
                    ReversePath.Insert(0, coo);
                    moveto(mytile.PositionX, mytile.PositionY + 1);
                    return;
                }
            }
            if (mytile.PositionY == 4)
            {
                if (mytile.PositionX > 10)
                {

                    int[] coo = { mytile.PositionX, mytile.PositionY };
                    ReversePath.Insert(0, coo);
                    moveto(mytile.PositionX - 1, mytile.PositionY);


                    return;
                }
            }
            if (mytile.PositionX == 10)
            {
                if (mytile.PositionY < 7)
                {
                    TalkingTimer = 5;
                    int[] coo = { mytile.PositionX, mytile.PositionY };
                    ReversePath.Insert(0, coo);
                    moveto(mytile.PositionX, mytile.PositionY + 1);
                    return;
                }
            }
            if (mytile.PositionY == 7)
            {
                if (mytile.PositionX > 5)
                {

                    int[] coo = { mytile.PositionX, mytile.PositionY };
                    ReversePath.Insert(0, coo);
                    moveto(mytile.PositionX - 1, mytile.PositionY);


                    return;
                }
            }
            if (mytile.PositionX == 5)
            {
                if (mytile.PositionY > 2)
                {
                    TalkingTimer = 5;
                    int[] coo = { mytile.PositionX, mytile.PositionY };
                    ReversePath.Insert(0, coo);
                    moveto(mytile.PositionX, mytile.PositionY - 1);
                    return;
                }
            }


            if (mytile.PositionY == 5)
            {
                if (mytile.PositionX == 2)
                {
                    TalkingTimer--;
                    if (TalkingTimer <= 0)
                    {
                        //reverse
                        ElReverso = true;
                    }


                }
            }
        }
    }
    void Path2()//4,7 vers 4,2
    {
        if(id==2)
        {
           
            if(player.myPresentTileInfo==GM.mapCootoTI[GM.CootoString(8,7)])//si le joueur se trouve a la porte
            {
                //vas tenter de sonner l<alerte
                isAlerted = true;
                GameObject Sound = Instantiate(SonDeClocheAlertePrefab, this.transform.position, Quaternion.identity);
                Sound.GetComponentInChildren<TextMesh>().text = "Help!, Help! A whereWolf!";
            }
            if(isAlerted==true)
            {
                if (mytile.PositionX == 4)
                {
                    if (mytile.PositionY > 2)
                    {



                        moveto(mytile.PositionX, mytile.PositionY - 1);
                        return;
                    }
                }
                if (mytile.PositionX == 4 && mytile.PositionY == 2)
                {
                    SonneAlerte();
                    return;
                }
            }
        }
    }
    void Path1()
    {
        if (id==1)
        {
            //Reverse
            if (ElReverso)
            {
                RevervePath();
                return;
            }
            if (isTRiggerd)//si tu as suivit une piste et la perd
            {
                ElReverso = true;
            }
            //follow etineraire

            if (mytile.PositionY == 11)
            {
                if (mytile.PositionX == 16)
                {

                    if (TalkingTimer > 0)
                    {
                        TalkingTimer--;
                        int[] coo = { mytile.PositionX, mytile.PositionY };
                        ReversePath.Insert(0, coo);
                        return;
                    }

                }
            }
            if (mytile.PositionY == 11)
            {
                if (mytile.PositionX > 12)
                {

                    int[] coo = { mytile.PositionX, mytile.PositionY };
                    ReversePath.Insert(0, coo);
                    moveto(mytile.PositionX - 1, mytile.PositionY);


                    return;
                }
            }
            if (mytile.PositionX == 12)
            {
                if (mytile.PositionY > 9)
                {
                    TalkingTimer = 5;
                    int[] coo = { mytile.PositionX, mytile.PositionY };
                    ReversePath.Insert(0, coo);
                    moveto(mytile.PositionX, mytile.PositionY - 1);
                    return;
                }
            }
            if (mytile.PositionY == 9)
            {
                if (mytile.PositionX > 9)
                {
                    int[] coo = { mytile.PositionX, mytile.PositionY };
                    ReversePath.Insert(0, coo);
                    moveto(mytile.PositionX - 1, mytile.PositionY);
                    return;
                }
            }
            if (mytile.PositionX == 9)
            {
                if (mytile.PositionY > 7)
                {
                    int[] coo = { mytile.PositionX, mytile.PositionY };
                    ReversePath.Insert(0, coo);
                    moveto(mytile.PositionX, mytile.PositionY - 1);
                    return;
                }
            }
            if (mytile.PositionY == 7)
            {
                if (mytile.PositionX > 8)
                {
                    int[] coo = { mytile.PositionX, mytile.PositionY };
                    ReversePath.Insert(0, coo);
                    moveto(mytile.PositionX - 1, mytile.PositionY);
                    return;
                }
            }
            if (mytile.PositionY == 7)
            {
                if (mytile.PositionX == 9)
                {
                    int[] coo = { mytile.PositionX, mytile.PositionY };
                    ReversePath.Insert(0, coo);
                    if (doorisopen == false)
                    {
                        //open door
                        doorisopen = true;
                        return;

                    }
                    else
                    {
                        int[] coo2 = { mytile.PositionX, mytile.PositionY };
                        ReversePath.Insert(0, coo);
                        moveto(mytile.PositionX - 1, mytile.PositionY);
                        return;
                    }

                }
            }
            if (mytile.PositionY == 7)
            {
                if (mytile.PositionX == 8)
                {
                    //on door space
                    int[] coo = { mytile.PositionX, mytile.PositionY };
                    ReversePath.Insert(0, coo);
                    moveto(mytile.PositionX - 1, mytile.PositionY);
                    return;

                }
            }
            if (mytile.PositionY == 7)
            {
                if (mytile.PositionX == 7)
                {

                    if (doorisopen)
                    {
                        //close door
                        int[] coo = { mytile.PositionX, mytile.PositionY };
                        ReversePath.Insert(0, coo);
                        doorisopen = false;
                        return;

                    }
                    else
                    {
                        int[] coo = { mytile.PositionX, mytile.PositionY };
                        ReversePath.Insert(0, coo);
                        moveto(mytile.PositionX - 1, mytile.PositionY);
                        return;
                    }

                }
            }

            if (mytile.PositionY == 7)
            {
                if (mytile.PositionX > 5)
                {


                    int[] coo = { mytile.PositionX, mytile.PositionY };
                    ReversePath.Insert(0, coo);
                    moveto(mytile.PositionX - 1, mytile.PositionY);
                    return;
                }
            }
            if (mytile.PositionY == 7)
            {
                if (mytile.PositionX == 5)
                {
                    TalkingTimer--;
                    if (TalkingTimer <= 0)
                    {
                        //reverse
                        ElReverso = true;
                    }


                }
            }
        }
    }
    int i = 0;
    void RevervePath()
    {

       /*  i++;

   if(ReversePath.Count<i)
     {
         ReversePath.Clear();
         i = 0;
         TalkingTimer = 5;
         ElReverso = false;
         return;
    }
         int[] coo = ReversePath[i];
         moveto(coo[0], coo[1]);
         return;
        */
        if(ReversePath.Count<=0)
        {
            isTRiggerd = false;
            ElReverso = false;
            return;
        }
        int[] coo = ReversePath[0];
        moveto(coo[0],coo[1]);
        ReversePath.Remove(ReversePath[0]);
        return;
    }
   void moveLeftDown(int fromX,int FromY,int ToX,int ToY)
   {
       if (mytile.PositionY == FromY)
       {
           if (mytile.PositionX > ToX)
           {
               moveto(mytile.PositionX - 1, mytile.PositionY);
           }
       }
       if (mytile.PositionX == fromX)
       {
           if (mytile.PositionY > ToY)
           {
               moveto(mytile.PositionX, mytile.PositionY - 1);
           }
       }
   }
   void moveto(int x,int y)
   {
       mytile= GM.mapCootoTI[GM.CootoString(x, y)];
       this.transform.position = new Vector3(x, 0, y);
   }
    void moveto2turn(int x1, int y1, int x2, int y2)
    {

    }

}
