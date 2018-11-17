using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Procedural.Carre;
using TurnManaging;
public class GameController : MonoBehaviour
{
    public Texture2D map;
    public Texture2D mapEtage1;
    public Texture2D mapEtage2;
    public static MapGenerator GM;
    public GameObject TilePrefab;
    public Transform TileHolder;
    public Transform TileHolderEtage1;
    public Transform TileHolderEtage2;
    public static TurnManager TM;
    public Material[] mats;
   
    //G10 Descend de 1 etage
    //G20 descend de 2 etages
    //G60 Monte de 1 etage
    //G70 Monte de 2 etages
    //Bleu= genere thuile sur l'etage

    void Start ()
    {
        TM = new TurnManager();
        SolGeneration();
        Etage1Generation();
        Etage2Generation();
        GM.VisualUpdate = UpdateVisual;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    public void UpdateVisual(TileInfo ti)
    {
        
        ti.MyVisual.GetComponentInChildren<MeshRenderer>().material = mats[0];
        
    }
    void SolGeneration()
    {
        GM = new MapGenerator(TilePrefab, TileHolder);
        GM.GenerateMap(map);
    }
    void Etage1Generation()
    {
        h = 1;
        GM.PreFabrication = SetHeight;
        GM.GenerationCondition = EtageFiltrer;
        GM.GenerateMap(mapEtage1, TileHolderEtage1);
        
    }
    void Etage2Generation()
    {
        h = 2;
        GM.PreFabrication = SetHeight;
        GM.GenerationCondition = EtageFiltrer;
        GM.GenerateMap(mapEtage2, TileHolderEtage2);
        
    }
    int h = 0;
    void SetHeight(TileInfo ti)
    {
        ti.CooH = h;
        ti.Height = 2f * h;
    }
    bool EtageFiltrer(TileInfo ti)
    {
        
        if (ti.B256 == 255 && ti.G256 == 0&&ti.R256==0)
        {
            //safe
            
            return true;
        }
        if (ti.B256 == 0 && ti.G256 <= 70 && ti.R256 == 0)
        {
            //Safe
            return true;
        }
        
        ti = null;
        return false;
    }
}
