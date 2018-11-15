using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IA.PathFinding.mapCarre;
public class GenericProcedural : MonoBehaviour
{

	
    
}
namespace Procedural.Carre
{
    public class TileInfo
    {
        public int Coox;
        public int Cooy;
        public int CooH;
        public float Height;
        public float R;
        public int R256;
        public float G;
        public int G256;
        public float B;
        public int B256;
        public float A;
        public int A256;
        public Color P;
        public GameObject MyVisual;
        public bool b1;
        public bool b2;
        public bool b3;
        
        public TileInfo(int x,int y)
        {
            Coox = x;
            Cooy = y;
        }
        public TileInfo(int x, int y,int h)
        {
            Coox = x;
            Cooy = y;
            CooH = h;
        }
        public TileInfo(int x, int y,Color p)
        {
            Coox = x;
            Cooy = y;
            P = p;
            R = p.r;
            R256 = (int)(p.r * 255);
            G = p.g;
            G256 = (int)(p.g * 255);
            B = p.b;
            B256 = (int)(p.b * 255);
            A = p.a;
            A256 = (int)(p.a * 255);
        }
        public TileInfo(int x, int y,int h, Color p)
        {
            Coox = x;
            Cooy = y;
            P = p;
            R = p.r;
            R256 = (int)(p.r * 255);
            G = p.g;
            G256 = (int)(p.g * 255);
            B = p.b;
            B256 = (int)(p.b * 255);
            A = p.a;
            A256 = (int)(p.a * 255);
            CooH = h;
        }
    }
    public class MapGenerator : MonoBehaviour
    {
        
        public int MapWidth;
        public int MapHeight;
        public Texture2D Map2D;
        public GameObject TilePrefab;//Le prefab des thuiles generes
        public Transform MapHolder;
        public Dictionary<TileInfo, GameObject> mapTItoGO = new Dictionary<TileInfo, GameObject>();//map les info vers le visuel de la thuil
        public Dictionary<GameObject, TileInfo> mapGOtoTI = new Dictionary<GameObject, TileInfo>();//map le visuel vers les info
        public Dictionary<string, TileInfo> mapCootoTI = new Dictionary<string, TileInfo>();//map les coo vers les tileinfo
        public List<TileInfo> mapinfo = new List<TileInfo>();//Contiens toutes les info de la map
        public delegate void Updater(TileInfo ti);
        public delegate void AditionalsFonctions();
        public Updater VisualUpdate;
        public AditionalsFonctions PreVisualUpdates;
        public AditionalsFonctions PostVisualUpdates;
        public AditionalsFonctions AfterGeneration;
        public MapGenerator(GameObject Tile, Transform TileHolder)
        {
            TilePrefab = Tile;
            MapHolder = TileHolder;
            PreVisualUpdates = EmptyVoid;
            PostVisualUpdates = EmptyVoid;
            AfterGeneration = EmptyVoid;
            VisualUpdate = EmptyVoid;
        }
        public TileInfo FindTile(int x,int y)
        {
            if(mapCootoTI.ContainsKey(CootoString(x,y))==false)
            {
                //print("Tile Not In Dictionary");
                return null;
            }
            else
            {
                //print("Tile Added");
                return mapCootoTI[CootoString(x, y)];
            }
        }
        public void destroyMap()//DestroyMap
        {
            foreach (TileInfo ti in mapinfo)
            {
                Destroy(mapTItoGO[ti]);

            }
            mapinfo.Clear();
        }
        public List<TileInfo> SaveMap()//SaveMap
        {
            List<TileInfo> SavedMap = new List<TileInfo>();
            foreach (TileInfo ti in mapinfo)
            {
                SavedMap.Add(ti);
                Destroy(mapTItoGO[ti]);

            }
            mapinfo.Clear();
            return SavedMap;
        }
        public void GenerateMap(int Largeur, int Longeur)//GenerateMap
        {
            MapWidth = Largeur;
            MapHeight = Longeur;
            for (int x = 0; x < Largeur; x++)
            {
                for (int y = 0; y < Longeur; y++)
                {
                    TileInfo TI = new TileInfo(x, y);//Tile info
                    GameObject TIGO = Instantiate(TilePrefab, new Vector3(x, 0, y), Quaternion.identity);//Tile PRefab
                    Mapping(TIGO, TI,x,y);//Mapping des deux dans mes dictionaires
                    TI.MyVisual = TIGO;
                    PreVisualUpdates();
                    VisualUpdate(TI);
                    PostVisualUpdates();
                }
            }
            AfterGeneration();
        }
        public void GenerateMap(int Largeur, int Longeur,int MinHauteur,int MaxHauteur)//GenerateMap
        {
            MapWidth = Largeur;
            MapHeight = Longeur;
            for (int x = 0; x < Largeur; x++)
            {
                for (int y = 0; y < Longeur; y++)
                {
                    TileInfo TI = new TileInfo(x, y);//Tile info
                    GameObject TIGO = Instantiate(TilePrefab, new Vector3(x, Random.Range(MinHauteur,MaxHauteur), y), Quaternion.identity);//Tile PRefab
                    Mapping(TIGO, TI,x,y);//Mapping des deux dans mes dictionaires
                    TI.MyVisual = TIGO;
                    PreVisualUpdates();
                    VisualUpdate(TI);
                    PostVisualUpdates();
                }
            }
            AfterGeneration();
        }
        public void GenerateMap(Texture2D Map2D1)
        {
            MapWidth = Map2D1.width;
            MapHeight = Map2D1.height;
            Map2D = Map2D1;
            for (int x = 0; x < MapWidth; x++)
            {
                for (int y = 0; y < MapHeight; y++)
                {
                    Color P = Map2D1.GetPixel(x, y);
                    TileInfo TI = new TileInfo(x, y, P);//Tile info
                    GameObject TIGO = Instantiate(TilePrefab, new Vector3(x, 0, y), Quaternion.identity, MapHolder);//Tile PRefab
                    Mapping(TIGO, TI, x, y);//Mapping des deux dans mes dictionaires
                    TI.MyVisual = TIGO;
                    PreVisualUpdates();
                    VisualUpdate(TI);
                    PostVisualUpdates();


                }
            }
            AfterGeneration();


        }
        void Mapping(GameObject go, TileInfo ti,int x,int y)
        {
            mapGOtoTI.Add(go, ti);
            mapTItoGO.Add(ti, go);
            mapinfo.Add(ti);
            string coo = CootoString(x, y);

            mapCootoTI.Add(coo, ti);


        }
        void Mapping(GameObject go, TileInfo ti)
        {
            mapGOtoTI.Add(go, ti);
            mapTItoGO.Add(ti, go);
            mapinfo.Add(ti);
            


        }
        public string CootoString(int x, int y)
        {
            return x.ToString() + "," + y.ToString();
        }
        public void flattenAll()
        {
            foreach (TileInfo ti in mapinfo)
            {
                ti.Height = 1;
                VisualUpdate(ti);
            }
        }
        public void MakeWalls(int longeur, int largeur, int Hauteur)
        {
            foreach (TileInfo ti in mapinfo)
            {
                if (ti.Coox == 0 || ti.Coox == longeur - 1 || ti.Cooy == 0 || ti.Cooy == largeur - 1)
                {
                    ti.Height = Hauteur;
                    VisualUpdate(ti);
                }
            }
        }
        public void MakeMountains(int centerX, int centerY, float Height, int Radius)
        {
            foreach (TileInfo ti in mapinfo)
            {
                //jeleve ce qui est a x,y 
                //je leleve de Height
                //J<eleve tout ce qui est autours dans un rayon de radius
                //je les eleve de height-distanceFromcenter
                //si je suis au centre, je suis 1*height
                //si je suis a radius du centre, je suis a 0*radius

                if (ti.Coox <= centerX + Radius && ti.Cooy >= centerX - Radius)//je veux savoir si tu te trouve a moins de radius du centre
                {
                    if (ti.Cooy <= centerY + Radius && ti.Cooy >= centerY - Radius)
                    {
                        //Je veux savoir a combien de % de radius tu te trouve
                        float distanceFromCenter = Vector2.Distance(new Vector2(centerX, centerY), new Vector2(ti.Coox, ti.Cooy));
                        if (distanceFromCenter != 0)
                        {
                            ti.Height += Height / distanceFromCenter;
                            VisualUpdate(ti);
                        }
                        else
                        {
                            ti.Height += Height;
                            VisualUpdate(ti);
                        }
                    }


                }


            }
        }
        public void MakeMountainInverse(int centerX, int centerY, float Height, int Radius)
        {
            foreach (TileInfo ti in mapinfo)
            {


                if (ti.Coox <= centerX + Radius && ti.Coox >= centerX - Radius)//je veux savoir si tu te trouve a moins de radius du centre
                {
                    if (ti.Cooy <= centerY + Radius && ti.Cooy >= centerY - Radius)
                    {
                        //Je veux savoir a combien de % de radius tu te trouve
                        float distanceFromCenter = Vector2.Distance(new Vector2(centerX, centerY), new Vector2(ti.Coox, ti.Cooy));
                        ti.Height += Height * distanceFromCenter * 0.1f;
                        ti.CooH = (int)ti.Height;
                        VisualUpdate(ti);
                        
                    }


                }


            }
        }
        public void MakePlateauCercle(int centerX, int centerY, float Heigh, int Radius)
        {
            foreach (TileInfo ti in mapinfo)
            {


                if (ti.Coox <= centerX + Radius && ti.Coox >= centerX - Radius)//je veux savoir si tu te trouve a moins de radius du centre
                {
                    if (ti.Cooy <= centerY + Radius && ti.Cooy >= centerY - Radius)
                    {


                        ti.Height += Heigh;
                        ti.CooH = (int)ti.Height;
                        VisualUpdate(ti);
                    }


                }


            }
        }
        public void MakePlateauCarre(int centerX, int centerY, float Heigh, int longeur, int largeur)
        {
            foreach (TileInfo ti in mapinfo)
            {


                if (ti.Coox <= centerX + largeur && ti.Coox >= centerX - largeur)//je veux savoir si tu te trouve a moins de radius du centre
                {
                    if (ti.Cooy <= centerY + longeur && ti.Cooy >= centerY - longeur)
                    {


                        ti.Height += Heigh;
                        ti.CooH = (int)ti.Height;
                        VisualUpdate(ti);
                    }


                }


            }
        }
        void EmptyVoid()
        {

        }
        void EmptyVoid(TileInfo ti)
        {

        }
    }


}


















namespace Procedural.Hex
{
    public class TileInfo
    {
        float RayonDuHex = 1f;

        public int Coox;//q
        public int Cooy;//r
        public int Coos;//s
        public int CooH;
        public float Height;
        public float R;
        public int R256;
        public float G;
        public int G256;
        public float B;
        public int B256;
        public float A;
        public int A256;
        public Color P;
        public GameObject MyVisual;
        public bool b1;
        public bool b2;
        public bool b3;

        public TileInfo(int x, int y)
        {
            Coox = x;
            Cooy = y;
            Coos = -(x + y);
        }
        public TileInfo(int x, int y, int h)
        {
            Coox = x;
            Cooy = y;
            CooH = h;
        }
        public TileInfo(int x, int y, Color p)
        {
            Coox = x;
            Cooy = y;
            Coos = -(x + y);
            P = p;
            R = p.r;
            R256 = (int)(p.r * 255);
            G = p.g;
            G256 = (int)(p.g * 255);
            B = p.b;
            B256 = (int)(p.b * 255);
            A = p.a;
            A256 = (int)(p.a * 255);
        }
        public TileInfo(int x, int y, int h, Color p)
        {
            Coox = x;
            Cooy = y;
            Coos = -(x + y);
            P = p;
            R = p.r;
            R256 = (int)(p.r * 255);
            G = p.g;
            G256 = (int)(p.g * 255);
            B = p.b;
            B256 = (int)(p.b * 255);
            A = p.a;
            A256 = (int)(p.a * 255);
            CooH = h;
        }
        public Vector3 Position()
        {
            float Radius = RayonDuHex;//=1
            float Height = Radius * 2;//=2
            float Width = 0.866f * Height;//=1.73

            float Vert = Height * 0.75f;//offset = 1.5
            float Horiz = Width;//=1.73

            return new Vector3(Horiz * (Coox + Cooy / 2f), 0f, Vert * Cooy);
        }
    }
    public class MapGenerator : MonoBehaviour
    {
        public int MapWidth;
        public int MapHeight;
        public Texture2D Map2D;
        public GameObject TilePrefab;//Le prefab des thuiles generes
        public Transform MapHolder;
        public Dictionary<TileInfo, GameObject> mapTItoGO = new Dictionary<TileInfo, GameObject>();//map les info vers le visuel de la thuil
        public Dictionary<GameObject, TileInfo> mapGOtoTI = new Dictionary<GameObject, TileInfo>();//map le visuel vers les info
        public Dictionary<string, TileInfo> mapCootoTI = new Dictionary<string, TileInfo>();//map les coo vers les tileinfo
        public List<TileInfo> mapinfo = new List<TileInfo>();//Contiens toutes les info de la map
        public delegate void Updater(TileInfo ti);
        public delegate void AditionalsFonctions();
        public Updater VisualUpdate;
        public AditionalsFonctions PreVisualUpdates;
        public AditionalsFonctions PostVisualUpdates;
        public AditionalsFonctions AfterGeneration;
        public MapGenerator(GameObject Tile,Transform TileHolder)
        {
            TilePrefab = Tile;
            MapHolder = TileHolder;
            PreVisualUpdates = EmptyVoid;
            PostVisualUpdates = EmptyVoid;
            AfterGeneration = EmptyVoid;
            VisualUpdate = EmptyVoid;
        }

        public void GenerateMap(int Largeur, int Longeur)//GenerateMap
        {
            MapWidth = Largeur;
            MapHeight = Longeur;
            for (int x = 0; x < Largeur; x++)
            {
                for (int y = 0; y < Longeur; y++)
                {
                    TileInfo TI = new TileInfo(x, y);//Tile info
                    GameObject TIGO = Instantiate(TilePrefab, TI.Position(), Quaternion.identity);//Tile PRefab
                    Mapping(TIGO, TI,x,y);//Mapping des deux dans mes dictionaires
                    TI.MyVisual = TIGO;

                    PreVisualUpdates();
                    VisualUpdate(TI);
                    PostVisualUpdates();
                }
            }
            AfterGeneration();
        }
        public void GenerateMap(int Largeur, int Longeur, int MinHauteur, int MaxHauteur)//GenerateMap
        {
            MapWidth = Largeur;
            MapHeight = Longeur;
            for (int x = 0; x < Largeur; x++)
            {
                for (int y = 0; y < Longeur; y++)
                {
                    TileInfo TI = new TileInfo(x, y);//Tile info
                    GameObject TIGO = Instantiate(TilePrefab, new Vector3(0, Random.Range(MinHauteur, MaxHauteur), 0)+ TI.Position(), Quaternion.identity);//Tile PRefab
                    Mapping(TIGO, TI,x,y);//Mapping des deux dans mes dictionaires
                    TI.MyVisual = TIGO;
                    PreVisualUpdates();
                    VisualUpdate(TI);
                    PostVisualUpdates();
                }
            }
            AfterGeneration();
        }
        public void GenerateMap(Texture2D Map2D1)
        {
            MapWidth = Map2D1.width;
            MapHeight = Map2D1.height;
            Map2D = Map2D1;
            for (int x = 0; x < MapWidth; x++)
            {
                for (int y = 0; y < MapHeight; y++)
                {
                    Color P = Map2D1.GetPixel(x, y);
                    TileInfo TI = new TileInfo(x, y, P);//Tile info
                    GameObject TIGO = Instantiate(TilePrefab, TI.Position(), Quaternion.identity, MapHolder);//Tile PRefab
                    Mapping(TIGO, TI, x, y);//Mapping des deux dans mes dictionaires
                    TI.MyVisual = TIGO;
                    PreVisualUpdates();
                    VisualUpdate(TI);
                    PostVisualUpdates();


                }
            }
            AfterGeneration();


        }
        void Mapping(GameObject go, TileInfo ti, int x, int y)
        {
            mapGOtoTI.Add(go, ti);
            mapTItoGO.Add(ti, go);
            mapinfo.Add(ti);
            string coo = CootoString(x, y);

            mapCootoTI.Add(coo, ti);


        }
        void Mapping(GameObject go, TileInfo ti)
        {
            mapGOtoTI.Add(go, ti);
            mapTItoGO.Add(ti, go);
            mapinfo.Add(ti);



        }
        public string CootoString(int x, int y)
        {
            return x.ToString() + "," + y.ToString();
        }
        public void flattenAll()
        {
            foreach (TileInfo ti in mapinfo)
            {
                ti.Height = 1;
                VisualUpdate(ti);
            }
        }
        public void MakeWalls(int longeur, int largeur, int Hauteur)
        {
            foreach (TileInfo ti in mapinfo)
            {
                if (ti.Coox == 0 || ti.Coox == longeur - 1 || ti.Cooy == 0 || ti.Cooy == largeur - 1)
                {
                    ti.Height = Hauteur;
                    VisualUpdate(ti);
                }
            }
        }
        public void MakeMountains(int centerX, int centerY, float Height, int Radius)
        {
            foreach (TileInfo ti in mapinfo)
            {
                //jeleve ce qui est a x,y 
                //je leleve de Height
                //J<eleve tout ce qui est autours dans un rayon de radius
                //je les eleve de height-distanceFromcenter
                //si je suis au centre, je suis 1*height
                //si je suis a radius du centre, je suis a 0*radius

                if (ti.Coox <= centerX + Radius && ti.Cooy >= centerX - Radius)//je veux savoir si tu te trouve a moins de radius du centre
                {
                    if (ti.Cooy <= centerY + Radius && ti.Cooy >= centerY - Radius)
                    {
                        //Je veux savoir a combien de % de radius tu te trouve
                        float distanceFromCenter = Vector2.Distance(new Vector2(centerX, centerY), new Vector2(ti.Coox, ti.Cooy));
                        if (distanceFromCenter != 0)
                        {
                            ti.Height += Height / distanceFromCenter;
                            VisualUpdate(ti);
                        }
                        else
                        {
                            ti.Height += Height;
                            VisualUpdate(ti);
                        }
                    }


                }


            }
        }
        public void MakeMountainInverse(int centerX, int centerY, float Height, int Radius)
        {
            foreach (TileInfo ti in mapinfo)
            {


                if (ti.Coox <= centerX + Radius && ti.Coox >= centerX - Radius)//je veux savoir si tu te trouve a moins de radius du centre
                {
                    if (ti.Cooy <= centerY + Radius && ti.Cooy >= centerY - Radius)
                    {
                        //Je veux savoir a combien de % de radius tu te trouve
                        float distanceFromCenter = Vector2.Distance(new Vector2(centerX, centerY), new Vector2(ti.Coox, ti.Cooy));
                        ti.Height += Height * distanceFromCenter * 0.1f;
                        ti.CooH = (int)ti.Height;
                        VisualUpdate(ti);

                    }


                }


            }
        }
        public void MakePlateauCercle(int centerX, int centerY, float Heigh, int Radius)
        {
            foreach (TileInfo ti in mapinfo)
            {


                if (ti.Coox <= centerX + Radius && ti.Coox >= centerX - Radius)//je veux savoir si tu te trouve a moins de radius du centre
                {
                    if (ti.Cooy <= centerY + Radius && ti.Cooy >= centerY - Radius)
                    {


                        ti.Height += Heigh;
                        ti.CooH = (int)ti.Height;
                        VisualUpdate(ti);
                    }


                }


            }
        }
        public void MakePlateauCarre(int centerX, int centerY, float Heigh, int longeur, int largeur)
        {
            foreach (TileInfo ti in mapinfo)
            {


                if (ti.Coox <= centerX + largeur && ti.Coox >= centerX - largeur)//je veux savoir si tu te trouve a moins de radius du centre
                {
                    if (ti.Cooy <= centerY + longeur && ti.Cooy >= centerY - longeur)
                    {


                        ti.Height += Heigh;
                        ti.CooH = (int)ti.Height;
                        VisualUpdate(ti);
                    }


                }


            }
        }
        void EmptyVoid()
        {

        }
        void EmptyVoid(TileInfo ti)
        {

        }
        public TileInfo FindTile(int x, int y)
        {
            if (mapCootoTI.ContainsKey(CootoString(x, y)) == false)
            {
                //print("Tile Not In Dictionary");
                return null;
            }
            else
            {
                //print("Tile Added");
                return mapCootoTI[CootoString(x, y)];
            }
        }

    }


}
