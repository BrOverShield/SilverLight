using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IA.PathFinding.mapCarre;
using Procedural.Carre;
public class PlayerController : MonoBehaviour
{
    /* clic gauche pour choisir notre destination, ca nous indique le chemin
     * clic droit pour se deplacer,
     * quand on se deplace, la camera change pour nous montrer l<animation de deplacement
     * quand on a fini de se deplacer, la camera zoom out
     * on peut appuyer sur espace pour passer un tour sur place*/
    // Use this for initialization

    public GameObject TileClicked;
    public GameObject MyTile;
    public delegate void Onclick();
    public Onclick Clicked;
    Pathfinding PF;
    int coox=0;
    int cooy=0;

    public Material[] DistanceColor = new Material[3];
    void Start ()
    {
        MyTile = NewGenerator.GM.mapTItoGO[NewGenerator.GM.FindTile(coox,cooy)];
        PF = new Pathfinding(NewGenerator.GM);
        PF.Legal = IllegalesMoves;
        Clicked = ClickControl;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    bool IllegalesMoves(Action a)
    {
        //return true si l<action est legal
        if (a != null && a.To != null)
        {
            if (a.To.R256 == 80|| a.To.R256 == 120)
            {
                return true;
            }
            if (a.To.R256 > 60)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }
    void ClickControl()
    {
        //Quand on clic sur une thuile, on appel la fonction Astar et on allume les thuile
        //On indique le nombre de tours pour s<y rendre
        //seulement si cest le tour du joueur
        if(NewGenerator.TM.IsPlayerTurn)
        {
            //Allume toutes les tiles sur le chemin

            //List des Actions (from,to)
            foreach (TileInfo ti in NewGenerator.GM.mapinfo)
            {
                NewGenerator.GM.VisualUpdate(ti);
                ti.MyVisual.GetComponentInChildren<TextMesh>().text = "";
            }
            int Distance = 0;
            foreach (Action a in PF.AStar(NewGenerator.GM.mapGOtoTI[MyTile], NewGenerator.GM.mapGOtoTI[TileClicked]))
            {
                Distance++;
                if(Distance<=5)
                {
                    a.To.MyVisual.GetComponentInChildren<TextMesh>().text = "1";
                    NewGenerator.GM.mapTItoGO[a.To].GetComponentInChildren<MeshRenderer>().material = DistanceColor[0];
                }
                else if(Distance <= 10)
                {
                    a.To.MyVisual.GetComponentInChildren<TextMesh>().text = "2";
                    NewGenerator.GM.mapTItoGO[a.To].GetComponentInChildren<MeshRenderer>().material = DistanceColor[1];
                }
                else
                {
                    a.To.MyVisual.GetComponentInChildren<TextMesh>().text = ((int)Distance/5+1).ToString();
                    NewGenerator.GM.mapTItoGO[a.To].GetComponentInChildren<MeshRenderer>().material = DistanceColor[2];
                }
            }
            
        }

    }
    void DoMove()
    {

    }
}
