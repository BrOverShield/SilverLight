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
    Dictionary<GameObject, TileInfo> mapGOtoTI = new Dictionary<GameObject, TileInfo>();//map le visuel vers les info
    List<TileInfo> mapinfo = new List<TileInfo>();//Contiens toutes les info de la map
    void GenerateMap1()
    {

        for (int x = 0; x < MapWidth; x++)
        {
            for (int y = 0; y < MapHeight; y++)
            {
                Color P = Map2D1.GetPixel(x, y);
                TileInfo TI = new TileInfo(x,y,P,0);//Tile info
                GameObject TIGO = Instantiate(TilePrefab, new Vector3(x, 0, y), Quaternion.identity,MapHolder);//Tile PRefab
                Mapping(TIGO, TI);//Mapping des deux dans mes dictionaires
                UpdateGo(TI);//Update le tile prefab attribue le ti au go et le go au ti
            }
        }
    }

    void Mapping(GameObject go, TileInfo ti)
    {
        mapGOtoTI.Add(go, ti);
        mapTItoGO.Add(ti, go);
        mapinfo.Add(ti);

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
