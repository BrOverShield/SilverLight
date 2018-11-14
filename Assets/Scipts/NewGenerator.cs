using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Procedural.Carre;

public class NewGenerator : MonoBehaviour
{
    public static MapGenerator GM;
    public Texture2D Map;
    public GameObject TilePrefab;
    public Transform MapHolderTransform;
    public Material[] mats;
	void Start ()
    {
        GM = new MapGenerator(TilePrefab,MapHolderTransform);
        GM.AfterGeneration = AddPrebabs;
        GM.VisualUpdate = UpdateVisual;

        GM.GenerateMap(Map);
	}
	
	
	void Update ()
    {
		
	}

    public GameObject BLCornerPrefab;
    public GameObject BRCornerPrefab;
    public GameObject FLCornerPrefab;
    public GameObject FRCornerPrefab;
    public GameObject FWallPrefab;
    public GameObject LWallPrefab;
    public GameObject BWallPrefab;
    public GameObject RWallPrefab;
    public GameObject TreePrefab;
    public GameObject PillarPrefab;
    public GameObject BWindowPrefab;
    public GameObject FWindowPrefab;
    public GameObject LWindowPrefab;
    public GameObject RWindowPrefab;

    public GameObject BDoorPrefab;
    public GameObject FDoorPrefab;
    public GameObject LDoorPrefab;
    public GameObject RDoorPrefab;

    public Transform MapHolder;

    public void UpdateVisual(TileInfo ti)
    {
        ti.MyVisual.GetComponentInChildren<TextMesh>().text = ti.R256.ToString();
        if(ti.R256>60)
        {
            ti.MyVisual.GetComponentInChildren<MeshRenderer>().material = mats[0];
        }
        if (ti.R256 == 60)
        {
            ti.MyVisual.GetComponentInChildren<MeshRenderer>().material = mats[1];
        }
        if (ti.R256 < 60)
        {
            ti.MyVisual.GetComponentInChildren<MeshRenderer>().material = mats[2];
        }
        if(ti.R256==220)
        {
            ti.MyVisual.GetComponentInChildren<MeshRenderer>().material = mats[1];
        }

    }
    public void AddPrebabs()
    {
        AjouterPrefabs(Map.width, Map.height);
    }
    void AddEnemies()
    {

    }
    public void AjouterPrefabs(int MapWidth, int MapHeight)
    {
        for (int x = 1; x < MapWidth - 1; x++)
        {
            for (int y = 1; y < MapHeight - 1; y++)
            {
                
                
                if (GM.mapCootoTI[GM.CootoString(x, y)].R256 == 100)
                {

                    if (GM.mapCootoTI[GM.CootoString(x + 1, y)].R256 == 80
                        && GM.mapCootoTI[GM.CootoString(x, y + 1)].R256 == 80
                        && GM.mapCootoTI[GM.CootoString(x + 1, y + 1)].R256 == 80)
                    { Instantiate(PillarPrefab, new Vector3(x, 0, y), Quaternion.identity, MapHolder); }

                    else if (GM.mapCootoTI[GM.CootoString(x + 1, y)].R256 == 80
                        && GM.mapCootoTI[GM.CootoString(x, y - 1)].R256 == 80
                        && GM.mapCootoTI[GM.CootoString(x + 1, y - 1)].R256 == 80)
                    { Instantiate(PillarPrefab, new Vector3(x, 0, y), Quaternion.identity, MapHolder); }

                    else if (GM.mapCootoTI[GM.CootoString(x - 1, y)].R256 == 80
                        && GM.mapCootoTI[GM.CootoString(x, y + 1)].R256 == 80
                        && GM.mapCootoTI[GM.CootoString(x - 1, y + 1)].R256 == 80)
                    { Instantiate(PillarPrefab, new Vector3(x, 0, y), Quaternion.identity, MapHolder); }

                    else if (GM.mapCootoTI[GM.CootoString(x - 1, y)].R256 == 80
                        && GM.mapCootoTI[GM.CootoString(x, y - 1)].R256 == 80
                        && GM.mapCootoTI[GM.CootoString(x - 1, y - 1)].R256 == 80)
                    { Instantiate(PillarPrefab, new Vector3(x, 0, y), Quaternion.identity, MapHolder); }





                    else if (GM.mapCootoTI[GM.CootoString(x + 1, y)].R256 == 100
                        && GM.mapCootoTI[GM.CootoString(x, y + 1)].R256 == 100
                        && GM.mapCootoTI[GM.CootoString(x + 1, y + 1)].R256 == 80)
                    { Instantiate(BLCornerPrefab, new Vector3(x, 0, y), Quaternion.identity, MapHolder); }

                    else if (GM.mapCootoTI[GM.CootoString(x + 1, y)].R256 == 100
                        && GM.mapCootoTI[GM.CootoString(x, y - 1)].R256 == 100
                        && GM.mapCootoTI[GM.CootoString(x + 1, y - 1)].R256 == 80)
                    { Instantiate(BRCornerPrefab, new Vector3(x, 0, y), Quaternion.identity, MapHolder); }

                    else if (GM.mapCootoTI[GM.CootoString(x - 1, y)].R256 == 100
                        && GM.mapCootoTI[GM.CootoString(x, y + 1)].R256 == 100
                        && GM.mapCootoTI[GM.CootoString(x - 1, y + 1)].R256 == 80)
                    { Instantiate(FLCornerPrefab, new Vector3(x, 0, y), Quaternion.identity, MapHolder); }

                    else if (GM.mapCootoTI[GM.CootoString(x - 1, y)].R256 == 100
                        && GM.mapCootoTI[GM.CootoString(x, y - 1)].R256 == 100
                        && GM.mapCootoTI[GM.CootoString(x - 1, y - 1)].R256 == 80)
                    { Instantiate(FRCornerPrefab, new Vector3(x, 0, y), Quaternion.identity, MapHolder); }



                    else if (GM.mapCootoTI[GM.CootoString(x + 1, y)].R256 == 80)
                    { Instantiate(BWallPrefab, new Vector3(x, 0, y), Quaternion.identity, MapHolder); }

                    else if (GM.mapCootoTI[GM.CootoString(x, y + 1)].R256 == 80)
                    { Instantiate(LWallPrefab, new Vector3(x, 0, y), Quaternion.identity, MapHolder); }

                    else if (GM.mapCootoTI[GM.CootoString(x - 1, y)].R256 == 80)
                    { Instantiate(FWallPrefab, new Vector3(x, 0, y), Quaternion.identity, MapHolder); }

                    else if (GM.mapCootoTI[GM.CootoString(x, y - 1)].R256 == 80)
                    { Instantiate(RWallPrefab, new Vector3(x, 0, y), Quaternion.identity, MapHolder); }





                }


                if (GM.mapCootoTI[GM.CootoString(x, y)].R256 == 120)
                {
                    if (GM.mapCootoTI[GM.CootoString(x + 1, y)].R256 == 80)
                    { Instantiate(BDoorPrefab, new Vector3(x, 0, y), Quaternion.identity, MapHolder); }
                    if (GM.mapCootoTI[GM.CootoString(x - 1, y)].R256 == 80)
                    { Instantiate(FDoorPrefab, new Vector3(x, 0, y), Quaternion.identity, MapHolder); }
                    if (GM.mapCootoTI[GM.CootoString(x, y + 1)].R256 == 80)
                    { Instantiate(LDoorPrefab, new Vector3(x, 0, y), Quaternion.identity, MapHolder); }
                    if (GM.mapCootoTI[GM.CootoString(x, y - 1)].R256 == 80)
                    { Instantiate(RDoorPrefab, new Vector3(x, 0, y), Quaternion.identity, MapHolder); }
                }


                if (GM.mapCootoTI[GM.CootoString(x, y)].R256 == 200)
                {
                    if (GM.mapCootoTI[GM.CootoString(x + 1, y)].R256 == 80)
                    { Instantiate(BWindowPrefab, new Vector3(x, 0, y), Quaternion.identity, MapHolder); }
                    if (GM.mapCootoTI[GM.CootoString(x - 1, y)].R256 == 80)
                    { Instantiate(FWindowPrefab, new Vector3(x, 0, y), Quaternion.identity, MapHolder); }
                    if (GM.mapCootoTI[GM.CootoString(x, y + 1)].R256 == 80)
                    { Instantiate(LWindowPrefab, new Vector3(x, 0, y), Quaternion.identity, MapHolder); }
                    if (GM.mapCootoTI[GM.CootoString(x, y - 1)].R256 == 80)
                    { Instantiate(RWindowPrefab, new Vector3(x, 0, y), Quaternion.identity, MapHolder); }
                }




                if (GM.mapCootoTI[GM.CootoString(x, y)].R256 == 220)
                {
                    if (GM.mapCootoTI[GM.CootoString(x - 1, y)].R256 == 220
                        && GM.mapCootoTI[GM.CootoString(x, y - 1)].R256 == 220
                        && GM.mapCootoTI[GM.CootoString(x - 1, y - 1)].R256 == 220)
                    { Instantiate(TreePrefab, new Vector3(x - 0.5f, 0, y - 0.5f), Quaternion.identity, MapHolder); }
                }


            }
        }
    }
}
