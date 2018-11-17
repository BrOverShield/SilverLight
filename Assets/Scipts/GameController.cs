using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Procedural.Carre;
using TurnManaging;
public class GameController : MonoBehaviour
{
    public static MapGenerator GM;
    public GameObject TilePrefab;
    public Transform TileHolder;
    public static TurnManager TM;
    public Material[] mats;
    // Use this for initialization
    void Start ()
    {
        TM = new TurnManager();
        GM = new MapGenerator(TilePrefab, TileHolder);
        GM.GenerateMap(50, 50);
        GM.VisualUpdate = UpdateVisual;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    public void UpdateVisual(TileInfo ti)
    {
        //ti.MyVisual.GetComponentInChildren<TextMesh>().text = ti.R256.ToString();
        ti.MyVisual.GetComponentInChildren<MeshRenderer>().material = mats[0];
        
    }
}
