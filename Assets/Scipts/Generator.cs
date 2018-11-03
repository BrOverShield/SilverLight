using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Generator : MonoBehaviour
{
    int MapWidth = 27;
    int MapHeight = 38;
    public Texture2D Map2D1;
    public GameObject TilePrefab;//Le prefab des thuiles generes
    public Transform MapHolder;
    Dictionary<TileInfo, GameObject> mapTItoGO = new Dictionary<TileInfo, GameObject>();//map les info vers le visuel de la thuil
    public Dictionary<GameObject, TileInfo> mapGOtoTI = new Dictionary<GameObject, TileInfo>();//map le visuel vers les info
    public Dictionary<string, TileInfo> mapCootoTI = new Dictionary<string, TileInfo>();//map les coo vers les tileinfo
    public List<TileInfo> mapinfo = new List<TileInfo>();//Contiens toutes les info de la map
    void GenerateMap1()
    {

        for (int x = 0; x < MapWidth; x++)
        {
            for (int y = 0; y < MapHeight; y++)
            {
                Color P = Map2D1.GetPixel(x, y);
                TileInfo TI = new TileInfo(x,y,P,0);//Tile info
                GameObject TIGO = Instantiate(TilePrefab, new Vector3(x, 0, y), Quaternion.identity,MapHolder);//Tile PRefab
                Mapping(TIGO, TI,x,y);//Mapping des deux dans mes dictionaires
                
                UpdateGo(TI);//Update le tile prefab attribue le ti au go et le go au ti
                StartLocation(x, y, TI);
                UpdateGo(TI);
            }
        }
    }
    void StartLocation(int x,int y,TileInfo ti)
    {
        if(x==2&&y==2)
        {
            mapTItoGO[ti].GetComponent<TileUpdater>().StartLocation = true; 
        }
    }
    public string CootoString(int x,int y)
    {
        return x.ToString() +","+ y.ToString();
    }
    void Mapping(GameObject go, TileInfo ti,int x,int y)
    {
        mapGOtoTI.Add(go, ti);
        mapTItoGO.Add(ti, go);
        mapinfo.Add(ti);
        string coo = CootoString(x, y);

        mapCootoTI.Add(coo, ti);

    }
    void UpdateGo(TileInfo ti)
    {
        mapTItoGO[ti].GetComponent<TileUpdater>().UpdateMe(ti);
    }
    private void Start()
    {
        GenerateMap1();
    }

}
