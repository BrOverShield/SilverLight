using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guardBehavior : MonoBehaviour
{
    public TileInfo mytile;
    public bool IDontKnowWhereIam = true;
    Generator GM;
    bool isTRiggerd = false;
    bool doorisopen = false;
    int TalkingTimer = 5;
    bool ElReverso = false;
    PlayerController player;
    int DMG = 10;
    public int Life = 50;
    public int id = 0;
    public GameObject SonDeClocheAlertePrefab;
    bool isAlerted = false;
    public GameObject Blood;
    public GameObject GuardPrefab;
    public bool IsActive = false;
    private void Start()
    {
        GM = FindObjectOfType<Generator>();
        player = FindObjectOfType<PlayerController>();
        
    }
    void Update()
    {
        if (IDontKnowWhereIam)
        {
            if (id == 0)
            {
                if (GM.mapCootoTI.ContainsKey(GM.CootoString(17, 1)))
                {

                    mytile = GM.mapCootoTI[GM.CootoString(17, 1)];
                    IDontKnowWhereIam = false;
                }
            }
            
        }
        else
        {

        }
        if (Life <= 0)
        {
            mytile.HasBlood = true;
            player.CurentLife += 100;
            Instantiate(Blood, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
    void Attac()
    {
        if (Random.Range(1, 10) < 30)
        {
            GameObject Sound = Instantiate(SonDeClocheAlertePrefab, this.transform.position, Quaternion.identity);
            Sound.GetComponentInChildren<TextMesh>().text = "Die WhereWolf";
        }
        player.CurentLife -= DMG;
    }
    void calltheGards()
    {
        Instantiate(GuardPrefab, new Vector3(17f, 0f, 1f), Quaternion.identity);
    }
    List<int[]> ReversePath = new List<int[]>();
    public void DoTurn()
    {
       /* if(mytile==null)
        {
            print("mytileestnull");
            if(GM.mapCootoTI.ContainsKey(GM.CootoString(17,1)))
            {
                print("pas de trouble");
            }
            mytile = GM.mapCootoTI[GM.CootoString(17, 1)];
            
        }
        if (mytile == null)
        {
            print("mytileeststill null");
            mytile = GM.mapCootoTI[GM.CootoString(17, 1)];
            
        }*/
        if (GM.mapCootoTI.ContainsKey(GM.CootoString(mytile.PositionX, mytile.PositionY - 1)))//up
        {
            TileInfo ti = GM.mapCootoTI[GM.CootoString(mytile.PositionX, mytile.PositionY - 1)];
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
                int[] coo = { mytile.PositionX, mytile.PositionY - 1 };//vas vers ca
                ReversePath.Insert(0, coo);
                moveto(mytile.PositionX, mytile.PositionY - 1);
                return;
            }
            if(ti.HasBlood)
            {
                GameObject Sound = Instantiate(SonDeClocheAlertePrefab, this.transform.position, Quaternion.identity);
                Sound.GetComponentInChildren<TextMesh>().text = "There is Blood Here, CALL THE GUARDS";
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
                Sound.GetComponentInChildren<TextMesh>().text = "There is Blood Here, CALL THE GUARDS";
                Sound.GetComponent<Timeout>().t = 1;
                calltheGards();
                return;
            }

        }
        if (GM.mapCootoTI.ContainsKey(GM.CootoString(mytile.PositionX + 1, mytile.PositionY)))//left
        {
            TileInfo ti = GM.mapCootoTI[GM.CootoString(mytile.PositionX + 1, mytile.PositionY)];
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
                int[] coo = { mytile.PositionX + 1, mytile.PositionY };//vas vers ca
                ReversePath.Insert(0, coo);
                moveto(mytile.PositionX + 1, mytile.PositionY);
                return;
            }
            if (ti.HasBlood)
            {
                GameObject Sound = Instantiate(SonDeClocheAlertePrefab, this.transform.position, Quaternion.identity);
                Sound.GetComponentInChildren<TextMesh>().text = "There is Blood Here, CALL THE GUARDS";
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
                Sound.GetComponentInChildren<TextMesh>().text = "There is Blood Here, CALL THE GUARDS";
                Sound.GetComponent<Timeout>().t = 1;
                calltheGards();
                return;
            }

        }
        Path1();
    }
    void Path1()
    {
        if (true)
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

            if (mytile.PositionX == 17)
            {
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
    void RevervePath()
    {

        
        if (ReversePath.Count <= 0)
        {
            isTRiggerd = false;
            ElReverso = false;
            return;
        }
        int[] coo = ReversePath[0];
        moveto(coo[0], coo[1]);
        ReversePath.Remove(ReversePath[0]);
        return;
    }
    void moveto(int x, int y)
    {
        mytile = GM.mapCootoTI[GM.CootoString(x, y)];
        this.transform.position = new Vector3(x, 0, y);
    }

}
