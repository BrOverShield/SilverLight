using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Procedural.Carre;
using TurnManaging;
public class GameController : MonoBehaviour
{
    public Texture2D map;//Info des thuiles du sol
    public Texture2D mapEtage1;//Info des thuiles du premier etage
    public Texture2D mapEtage2;//Info des thuiles du deuxieme etage
    public static MapGenerator GM;//Classe de generation procedurale
    public GameObject TilePrefab;//Prefab de thuile
    public Transform TileHolder;//Parent des thuiles du sol
    public Transform TileHolderEtage1;//Parent des thuiles du premier etage
    public Transform TileHolderEtage2;//Parent des thuiles du deuxieme etage
    public static TurnManager TM;//Classe de turn managing
    public Material[] mats;
   
    //G10 Descend de 1 etage
    //G20 descend de 2 etages
    //G60 Monte de 1 etage
    //G70 Monte de 2 etages
    //Bleu= genere thuile sur l'etage

    void Start ()
    {
        TM = new TurnManager();//Instance statique de turn manager
        SolGeneration();//Genere les thuile du sol
        Etage1Generation();//Genere les thuile du premier etage
        Etage2Generation();//Genere les thuile du deuxiemme etage
        GM.VisualUpdate = UpdateVisual;//Assigne VisualUpdate
    }
	
	
    public void UpdateVisual(TileInfo ti)//Assigne le material par default
    {
        
        ti.MyVisual.GetComponentInChildren<MeshRenderer>().material = mats[0];
        
    }
    void SolGeneration()//Genere les thuile du sol
    {
        GM = new MapGenerator(TilePrefab, TileHolder);
        GM.GenerateMap(map);
    }
    void Etage1Generation()//Genere les thuile de premier etage
    {
        h = 1;//CooH des thuiles du premier etage = 1
        GM.PreFabrication = SetHeight;//Attribue cooH et height (height=cooH*2)
        GM.GenerationCondition = EtageFiltrer;//Condition pour generer les thuiles
        GM.GenerateMap(mapEtage1, TileHolderEtage1);
        
    }
    void Etage2Generation()//Meme chose que la fonction etage1generation() mais un etage plus haut
    {
        h = 2;
        GM.PreFabrication = SetHeight;
        GM.GenerationCondition = EtageFiltrer;
        GM.GenerateMap(mapEtage2, TileHolderEtage2);
        
    }
    int h = 0;
    void SetHeight(TileInfo ti)//Attribue cooH et height
    {
        ti.CooH = h;
        ti.Height = 2f * h;
    }
    bool EtageFiltrer(TileInfo ti)//Condition pour generer les thuiles
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
