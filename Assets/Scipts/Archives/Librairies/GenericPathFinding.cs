using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Procedural.Carre;
using Procedural.Hex;
public class GenericPathFinding : MonoBehaviour
{

	
}
namespace IA.PathFinding.mapCarre
{
    public class Node
    {
        public State state;
        public Node parent;
        public Action action;
        public int pathCost;

        public Node(State s,Node p,Action a,int pc)
        {
            state = s;
            parent = p;
            action = a;
            pathCost = pc;
        }
        public Node ChildNode(Pathfinding pathfinding,Node parent,Action action)
        {
            State s = pathfinding.Result(parent.state, action);
            Node p = parent;
            Action a = action;
            int pc = parent.pathCost + pathfinding.StepCost(parent.state, action);

            return new Node(s, p, a, pc);
        }
    }
    public class State
    {
        public Procedural.Carre.TileInfo mytile;
        public State(Procedural.Carre.TileInfo ti)
        {
            mytile = ti;
        }
    }
    public class Action
    {
        public bool isAttack = false;
        public bool isDiagonal = false;
        public Procedural.Carre.TileInfo Target;
        public Procedural.Carre.TileInfo From;
        public Procedural.Carre.TileInfo To;
        public bool b1;
        public bool b2;
        public bool b3;
        public int Direction;
        public Action(Procedural.Carre.TileInfo from, Procedural.Carre.TileInfo to)
        {
            From = from;
            To = to;
        }
        public Action(Procedural.Carre.TileInfo from, Procedural.Carre.TileInfo to,int direction)
        {
            From = from;
            To = to;
            Direction = direction;
        }
    }
    
         

    public class Pathfinding
    {
        public bool AllowDiagonal=false;
        public bool AllowUpAndDown = false;

        List<Action> solution=new List<Action>();
        List<Node> frontier=new List<Node>();
        List<Node> explored=new List<Node>();

        

        Procedural.Carre.MapGenerator GM;

        public int TimeOut = 1000;//Le timeout par default est 1000
        public delegate int StepCostCalulation(Procedural.Carre.TileInfo from, Procedural.Carre.TileInfo to);
        public StepCostCalulation StepCostPerso;//StepCost
        public delegate bool Check(Action a);
        public Check Legal;//Legals Moves Check
        public Check CanAttack;//EnemyDetection Check
        public delegate void ActionEffect(GameObject unit,Procedural.Carre.TileInfo from, Procedural.Carre.TileInfo to);
        public ActionEffect MoveEffect;//Moving
        public ActionEffect AttackEffect;//Attacking
        public Check OthersActionCheck;
        public ActionEffect OthersActionEffect;
        public Pathfinding(Procedural.Carre.MapGenerator gm)
        {
            GM=gm;
            Legal = DefaultLegalCheck;//Tout ce qui est dans la map est legal
            MoveEffect = DefaultMoveEffect;//deplace le unit game object
            AttackEffect = DefaultAttackEffect;//Par default ne fait rien, pour ajouter une attack, ajouter une fonction dans attackeffect
            CanAttack = DefaultCanAttack;//par default false, pour activer la detection d<attaque, ajouter une fonction dans le delagate canattack
        }
        public void FindPathBFS(GameObject unit,Procedural.Carre.TileInfo From, Procedural.Carre.TileInfo To)//Trouve un path et L<execute
        {
            foreach (Action a in BFS(From,To))
            {
                DoActionEffect(a, unit);

            }
        }
        public Action[] LegalsActions(State s)//Trouve les actions Legales, //up down left right et diagonales // on defenie ce qui est illegal dans le delegate check legal
        {
            List<Action> myActions=new List<Action>();

            Action up = new Action(s.mytile, GM.FindTile(s.mytile.Coox, s.mytile.Cooy + 1,s.mytile.CooH),0);//up
            myActions.Add(up);
            Action down = new Action(s.mytile, GM.FindTile(s.mytile.Coox, s.mytile.Cooy - 1, s.mytile.CooH),2);//down
            myActions.Add(down);
            Action left = new Action(s.mytile, GM.FindTile(s.mytile.Coox-1, s.mytile.Cooy, s.mytile.CooH),3);//left
            myActions.Add(left);
            Action right = new Action(s.mytile, GM.FindTile(s.mytile.Coox+1, s.mytile.Cooy, s.mytile.CooH),1);//right
            myActions.Add(right);
            if(AllowDiagonal)
            {
                Action upLeft = new Action(s.mytile,GM.FindTile(s.mytile.Coox-1, s.mytile.Cooy + 1, s.mytile.CooH));//up left
                upLeft.isDiagonal = true;
                myActions.Add(upLeft);
                Action upRight = new Action(s.mytile, GM.FindTile(s.mytile.Coox+1, s.mytile.Cooy + 1, s.mytile.CooH));//up right
                upRight.isDiagonal = true;
                myActions.Add(upRight);
                Action DownLeft = new Action(s.mytile, GM.FindTile(s.mytile.Coox-1, s.mytile.Cooy - 1, s.mytile.CooH));//down left
                DownLeft.isDiagonal = true;
                myActions.Add(DownLeft);
                Action DownRight = new Action(s.mytile, GM.FindTile(s.mytile.Coox+1, s.mytile.Cooy - 1, s.mytile.CooH));//down right
                DownRight.isDiagonal = true;
                myActions.Add(DownRight);
            }
            if(AllowUpAndDown)
            {
                Action upUp = new Action(s.mytile, GM.FindTile(s.mytile.Coox, s.mytile.Cooy + 1,s.mytile.CooH+1), 0);//up
                myActions.Add(upUp);
                Action downUp = new Action(s.mytile, GM.FindTile(s.mytile.Coox, s.mytile.Cooy - 1, s.mytile.CooH + 1), 2);//down
                myActions.Add(downUp);
                Action leftUp = new Action(s.mytile, GM.FindTile(s.mytile.Coox - 1, s.mytile.Cooy, s.mytile.CooH + 1), 3);//left
                myActions.Add(leftUp);
                Action rightUp = new Action(s.mytile, GM.FindTile(s.mytile.Coox + 1, s.mytile.Cooy, s.mytile.CooH + 1), 1);//right
                myActions.Add(rightUp);

                Action upDown = new Action(s.mytile, GM.FindTile(s.mytile.Coox, s.mytile.Cooy + 1, s.mytile.CooH - 1), 0);//up
                myActions.Add(upDown);
                Action downDown = new Action(s.mytile, GM.FindTile(s.mytile.Coox, s.mytile.Cooy - 1, s.mytile.CooH - 1), 2);//down
                myActions.Add(downDown);
                Action leftDown = new Action(s.mytile, GM.FindTile(s.mytile.Coox - 1, s.mytile.Cooy, s.mytile.CooH - 1), 3);//left
                myActions.Add(leftDown);
                Action rightDown = new Action(s.mytile, GM.FindTile(s.mytile.Coox + 1, s.mytile.Cooy, s.mytile.CooH - 1), 1);//right
                myActions.Add(rightDown);
            }
            
            List<Action> illigalsActions = new List<Action>();
            foreach (Action a in myActions)//calcul ce qui est illegal
            {
                if (CanAttack(a))//par default void, pour activer la detection d<attaque, ajouter une fonction dans le delagate canattack
                {
                    a.Target = a.To;
                    a.isAttack = true;
                }
                if (Legal(a)==false)
                {
                    illigalsActions.Add(a);
                }
                if(a==null)
                {
                    illigalsActions.Add(a);
                }
            }
            foreach (Action a in illigalsActions)//retire ce qui est illegal
            {
                myActions.Remove(a);
            }
            return myActions.ToArray();
            //return les actions legales depuis le state s
        }
        public bool DefaultCanAttack(Action a)//Fonction par Default de detection D,enemie
        {
            return false;
        }
        public void DefaultMoveEffect(GameObject Unit,Procedural.Carre.TileInfo from, Procedural.Carre.TileInfo to)//Effet de deplacement par default
        {
            Unit.transform.position = new Vector3(to.Coox, 0f, to.Cooy);
           // print("Move from" + from.Coox + "," + from.Cooy + " to " + to.Coox + "," + to.Cooy);
            
        }
        public void DefaultAttackEffect(GameObject Unit, Procedural.Carre.TileInfo from, Procedural.Carre.TileInfo to)//Effet D<Attaque par default
        {
            

        }
        public void DoActionEffect(Action a,GameObject unit)//Execute le mouvement ou l<attaque depuis le unit attack a priorite sur le mouvement
        {
            if(OthersActionCheck!=null)//CHeck supplementaire personalise
            {
                if (OthersActionCheck(a))
                {
                    OthersActionEffect(unit, a.From, a.To);//Actions Supplementaire personalise
                    return;
                }
            }          
            if(a.isAttack)
            {
                AttackEffect(unit,a.From,a.To);
                return;
            }
            else
            {
                MoveEffect(unit, a.From, a.To);
                return;
            }
        }
        public bool DefaultLegalCheck(Action a)//Regarde si l<action ne te sort pas de la map
        {
            if(a!=null&&a.To!=null)
            {
                
                if (GM.mapCootoTI.ContainsKey(GM.CootoString(a.To.Coox, a.To.Cooy,a.To.CooH)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }            
            else
            {
                return false;
            }
            
        }
        public int DefaultStepcost(State s, Action a)//1 par default
        {
            if(AllowDiagonal)
            {
                if (a.isDiagonal)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
                

            }
            else
            {
                return 1;
            }

        }
        public int StepCost(State s, Action a)//return le cout de l<action a depuis le state s
        {
            if(StepCostPerso!=null)
            {
                return StepCostPerso(a.From, a.To);
            }
            else
            {
                return DefaultStepcost(s,a);
            }
            
            
        }
        public State Result(State s,Action a)//Return le resultat de l<action a sur le state s
        {
            return new State(a.To);
            
        }
        public Action[] Solution(Node Goal)//Calcul la solution, prend la destination puis, remonte les parents
        {
            Node myNode = Goal;
            solution.Clear();
            solution.Insert(0,myNode.action);
            while(myNode.parent.action!=null)
            {
                solution.Insert(0,myNode.parent.action);
                myNode = myNode.parent;
            }
            return solution.ToArray();

        }
        public bool GoalTest(Procedural.Carre.TileInfo myTile, Procedural.Carre.TileInfo GoalTile)//Calcul si on est rendu a destination
        {
            //return true si on a trouve le goal
            if(myTile.Coox==GoalTile.Coox&&myTile.Cooy==GoalTile.Cooy&& myTile.CooH == GoalTile.CooH)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool FrontierOrExploredContains(Node test)//Evite les boucles infines ;)
        {
            
            foreach (Node N in frontier)
            {
                if(test.state.mytile.Coox==N.state.mytile.Coox&& test.state.mytile.Cooy == N.state.mytile.Cooy && test.state.mytile.CooH == N.state.mytile.CooH)
                {
                    return true;
                }
            }
            foreach (Node N in explored)
            {
                if (test.state.mytile.Coox == N.state.mytile.Coox && test.state.mytile.Cooy == N.state.mytile.Cooy && test.state.mytile.CooH == N.state.mytile.CooH)
                {
                    return true;
                }
            }
            return false;
        }

        public List<Action> BFS(Procedural.Carre.TileInfo from, Procedural.Carre.TileInfo to)//Breadth first search
        {

            State initState = new State(from);
            Node init = new Node(initState,null,null,0);
           
            frontier.Clear();
            explored.Clear();
            solution.Clear();
            frontier.Add(init);
            Node mynode;
            int T = 0;
            while (frontier.Count>0)
            {
                T++;
                if(T>=TimeOut)
                {
                    Debug.Log("TimeOut" + T);
                    goto timeisout;
                }
                mynode = frontier[0];
                frontier.Remove(mynode);
                explored.Add(mynode);
                if(GoalTest(mynode.state.mytile,to))
                {
                    //return solution
                   
                    List <Action >Path= new List<Action>();
                    foreach (Action a in Solution(mynode))//Execute la fonction Solution pour trouver la solution
                    {
                        //La solution se trouve aussi dans la liste solution
                        Path.Add(a);
                    }
                    return Path;
                }
                if(LegalsActions(mynode.state)!=null)
                {
                    foreach (Action a in LegalsActions(mynode.state))
                    {
                        Node ChildNode = mynode.ChildNode(this, mynode, a);
                        if(FrontierOrExploredContains(ChildNode)==false)
                        {
                            frontier.Add(ChildNode);
                        }
                    }
                }
            }
            timeisout:
            Debug.Log("No path found");//Reste su place si aucun chemin n<a ete trouve
            List<Action> NoPath = new List<Action>();
            NoPath.Add(new Action(from, from));
            return NoPath;
        }




        public List<Action> DFS(Procedural.Carre.TileInfo from, Procedural.Carre.TileInfo to)//Depth first search
        {

            State initState = new State(from);
            Node init = new Node(initState, null, null, 0);

            frontier.Clear();
            explored.Clear();
            solution.Clear();
            frontier.Add(init);
            Node mynode;
            int T = 0;
            while (frontier.Count > 0)
            {
                T++;
                if (T >= TimeOut)
                {
                    Debug.Log("TimeOut" + T);
                    goto timeisout;
                }
                mynode = frontier[0];
                frontier.Remove(mynode);
                explored.Add(mynode);
                if (GoalTest(mynode.state.mytile, to))
                {
                    //return solution

                    List<Action> Path = new List<Action>();
                    foreach (Action a in Solution(mynode))//Execute la fonction Solution pour trouver la solution
                    {
                        //La solution se trouve aussi dans la liste solution
                        Path.Add(a);
                    }
                    return Path;
                }
                if (LegalsActions(mynode.state) != null)
                {
                    foreach (Action a in LegalsActions(mynode.state))
                    {
                        Node ChildNode = mynode.ChildNode(this, mynode, a);
                        if (FrontierOrExploredContains(ChildNode) == false)
                        {
                            frontier.Insert(0,ChildNode);//La difference avec BFS est ici
                        }
                    }
                }
            }
            timeisout:
            Debug.Log("No path found");//Reste su place si aucun chemin n<a ete trouve
            List<Action> NoPath = new List<Action>();
            NoPath.Add(new Action(from, from));
            return NoPath;
        }


        public List<Action> AStar(Procedural.Carre.TileInfo from, Procedural.Carre.TileInfo to)//A*
        {

            State initState = new State(from);
            Node init = new Node(initState, null, null, 0);

            frontier.Clear();
            explored.Clear();
            solution.Clear();
            frontier.Add(init);
            Node mynode;
            int T = 0;
            while (frontier.Count > 0)
            {
                T++;
                if (T >= TimeOut)
                {
                    Debug.Log("TimeOut" + T);
                    goto timeisout;
                }
                //myNode vas etre celle qui les le plus proche de mon goal en ligne droite
                float Score=99999;
                Node StarNode=null;
                foreach (Node n in frontier)
                {
                    float D = Vector3.Distance(new Vector3(n.state.mytile.Coox, n.state.mytile.Cooy,n.state.mytile.CooH), new Vector3(to.Coox, to.Cooy,to.CooH));
                    if (D<Score)
                    {
                        Score = D;
                        StarNode = n;
                    }
                }
                mynode = StarNode;
                frontier.Remove(mynode);
                explored.Add(mynode);
                if (GoalTest(mynode.state.mytile, to))
                {
                    //return solution

                    List<Action> Path = new List<Action>();
                    foreach (Action a in Solution(mynode))//Execute la fonction Solution pour trouver la solution
                    {
                        //La solution se trouve aussi dans la liste solution
                        Path.Add(a);
                    }
                    return Path;
                }
                if (LegalsActions(mynode.state) != null)
                {
                    foreach (Action a in LegalsActions(mynode.state))
                    {
                        Node ChildNode = mynode.ChildNode(this, mynode, a);
                        if (FrontierOrExploredContains(ChildNode) == false)
                        {
                            frontier.Add(ChildNode);
                        }
                    }
                }
            }
            timeisout:
            Debug.Log("No path found");//Reste su place si aucun chemin n<a ete trouve
            List<Action> NoPath = new List<Action>();
            NoPath.Add(new Action(from, from));
            return NoPath;
        }

    }
}

























namespace IA.PathFinding.mapHex
{
    public class Node
    {
        public State state;
        public Node parent;
        public Action action;
        public int pathCost;

        public Node(State s, Node p, Action a, int pc)
        {
            state = s;
            parent = p;
            action = a;
            pathCost = pc;
        }
        public Node ChildNode(Pathfinding pathfinding, Node parent, Action action)
        {
            State s = pathfinding.Result(parent.state, action);
            Node p = parent;
            Action a = action;
            int pc = parent.pathCost + pathfinding.StepCost(parent.state, action);

            return new Node(s, p, a, pc);
        }
    }
    public class State
    {
        public Procedural.Hex.TileInfo mytile;
        public State(Procedural.Hex.TileInfo ti)
        {
            mytile = ti;
        }
    }
    public class Action
    {
        public bool isAttack = false;
        public Procedural.Hex.TileInfo Target;
        public Procedural.Hex.TileInfo From;
        public Procedural.Hex.TileInfo To;
        public bool b1;
        public bool b2;
        public bool b3;
        public Action(Procedural.Hex.TileInfo from, Procedural.Hex.TileInfo to)
        {
            From = from;
            To = to;
        }
    }



    public class Pathfinding : MonoBehaviour
    {
        public bool AllowDiagonal = false;

        List<Action> solution = new List<Action>();
        List<Node> frontier = new List<Node>();
        List<Node> explored = new List<Node>();



        Procedural.Hex.MapGenerator GM;

        public int TimeOut = 1000;//Le timeout par default est 1000
        public delegate int StepCostCalulation(Procedural.Hex.TileInfo from, Procedural.Hex.TileInfo to);
        public StepCostCalulation StepCostPerso;//StepCost
        public delegate bool Check(Action a);
        public Check Legal;//Legals Moves Check
        public Check CanAttack;//EnemyDetection Check
        public delegate void ActionEffect(GameObject unit, Procedural.Hex.TileInfo from, Procedural.Hex.TileInfo to);
        public ActionEffect MoveEffect;//Moving
        public ActionEffect AttackEffect;//Attacking
        public Check OthersActionCheck;
        public ActionEffect OthersActionEffect;
        public Pathfinding(Procedural.Hex.MapGenerator gm)
        {
            GM = gm;
            Legal = DefaultLegalCheck;//Tout ce qui est dans la map est legal
            MoveEffect = DefaultMoveEffect;//deplace le unit game object
            AttackEffect = DefaultAttackEffect;//Par default ne fait rien, pour ajouter une attack, ajouter une fonction dans attackeffect
            CanAttack = DefaultCanAttack;//par default false, pour activer la detection d<attaque, ajouter une fonction dans le delagate canattack
        }
        public void FindPathBFS(GameObject unit, Procedural.Hex.TileInfo From, Procedural.Hex.TileInfo To)//Trouve un path et L<execute
        {
            foreach (Action a in BFS(From, To))
            {
                DoActionEffect(a, unit);

            }
        }
        public Action[] LegalsActions(State s)//Trouve les actions Legales, //up down left right et diagonales // on defenie ce qui est illegal dans le delegate check legal
        {
            List<Action> myActions = new List<Action>();

            Action up = new Action(s.mytile, GM.FindTile(s.mytile.Coox, s.mytile.Cooy + 1));//up Left
            myActions.Add(up);
            Action down = new Action(s.mytile, GM.FindTile(s.mytile.Coox, s.mytile.Cooy - 1));//down Right
            myActions.Add(down);
            Action left = new Action(s.mytile, GM.FindTile(s.mytile.Coox - 1, s.mytile.Cooy));//left
            myActions.Add(left);
            Action right = new Action(s.mytile, GM.FindTile(s.mytile.Coox + 1, s.mytile.Cooy));//right
            myActions.Add(right);
            
                
                Action upRight = new Action(s.mytile, GM.FindTile(s.mytile.Coox + 1, s.mytile.Cooy + 1));//up right              
                myActions.Add(upRight);

                Action DownLeft = new Action(s.mytile, GM.FindTile(s.mytile.Coox - 1, s.mytile.Cooy - 1));//down left               
                myActions.Add(DownLeft);

                
            

            List<Action> illigalsActions = new List<Action>();
            foreach (Action a in myActions)//calcul ce qui est illegal
            {
                if (CanAttack(a))//par default void, pour activer la detection d<attaque, ajouter une fonction dans le delagate canattack
                {
                    a.Target = a.To;
                    a.isAttack = true;
                }
                if (Legal(a) == false)
                {
                    illigalsActions.Add(a);
                }
                if (a == null)
                {
                    illigalsActions.Add(a);
                }
            }
            foreach (Action a in illigalsActions)//retire ce qui est illegal
            {
                myActions.Remove(a);
            }
            return myActions.ToArray();
            //return les actions legales depuis le state s
        }
        public bool DefaultCanAttack(Action a)//Fonction par Default de detection D,enemie
        {
            return false;
        }
        public void DefaultMoveEffect(GameObject Unit, Procedural.Hex.TileInfo from, Procedural.Hex.TileInfo to)//Effet de deplacement par default
        {
            Unit.transform.position = to.Position();
            // print("Move from" + from.Coox + "," + from.Cooy + " to " + to.Coox + "," + to.Cooy);

        }
        public void DefaultAttackEffect(GameObject Unit, Procedural.Hex.TileInfo from, Procedural.Hex.TileInfo to)//Effet D<Attaque par default
        {


        }
        public void DoActionEffect(Action a, GameObject unit)//Execute le mouvement ou l<attaque depuis le unit attack a priorite sur le mouvement
        {
            if (OthersActionCheck != null)//CHeck supplementaire personalise
            {
                if (OthersActionCheck(a))
                {
                    OthersActionEffect(unit, a.From, a.To);//Actions Supplementaire personalise
                    return;
                }
            }
            if (a.isAttack)
            {
                AttackEffect(unit, a.From, a.To);
                return;
            }
            else
            {
                MoveEffect(unit, a.From, a.To);
                return;
            }
        }
        public bool DefaultLegalCheck(Action a)//Regarde si l<action ne te sort pas de la map
        {
            if (a != null && a.To != null)
            {

                if (GM.mapCootoTI.ContainsKey(GM.CootoString(a.To.Coox, a.To.Cooy)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
        public int DefaultStepcost(State s, Action a)//1 par default
        {
            
            
                return 1;
            

        }
        public int StepCost(State s, Action a)//return le cout de l<action a depuis le state s
        {
            if (StepCostPerso != null)
            {
                return StepCostPerso(a.From, a.To);
            }
            else
            {
                return DefaultStepcost(s, a);
            }


        }
        public State Result(State s, Action a)//Return le resultat de l<action a sur le state s
        {
            return new State(a.To);

        }
        public Action[] Solution(Node Goal)//Calcul la solution, prend la destination puis, remonte les parents
        {
            Node myNode = Goal;
            solution.Clear();
            solution.Insert(0, myNode.action);
            while (myNode.parent.action != null)
            {
                solution.Insert(0, myNode.parent.action);
                myNode = myNode.parent;
            }
            return solution.ToArray();

        }
        public bool GoalTest(Procedural.Hex.TileInfo myTile, Procedural.Hex.TileInfo GoalTile)//Calcul si on est rendu a destination
        {
            //return true si on a trouve le goal
            if (myTile.Coox == GoalTile.Coox && myTile.Cooy == GoalTile.Cooy)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool FrontierOrExploredContains(Node test)//Evite les boucles infines ;)
        {

            foreach (Node N in frontier)
            {
                if (test.state.mytile.Coox == N.state.mytile.Coox && test.state.mytile.Cooy == N.state.mytile.Cooy)
                {
                    return true;
                }
            }
            foreach (Node N in explored)
            {
                if (test.state.mytile.Coox == N.state.mytile.Coox && test.state.mytile.Cooy == N.state.mytile.Cooy)
                {
                    return true;
                }
            }
            return false;
        }

        public List<Action> BFS(Procedural.Hex.TileInfo from, Procedural.Hex.TileInfo to)//Breadth first search
        {

            State initState = new State(from);
            Node init = new Node(initState, null, null, 0);

            frontier.Clear();
            explored.Clear();
            solution.Clear();
            frontier.Add(init);
            Node mynode;
            int T = 0;
            while (frontier.Count > 0)
            {
                T++;
                if (T >= TimeOut)
                {
                    print("TimeOut" + T);
                    goto timeisout;
                }
                mynode = frontier[0];
                frontier.Remove(mynode);
                explored.Add(mynode);
                if (GoalTest(mynode.state.mytile, to))
                {
                    //return solution

                    List<Action> Path = new List<Action>();
                    foreach (Action a in Solution(mynode))//Execute la fonction Solution pour trouver la solution
                    {
                        //La solution se trouve aussi dans la liste solution
                        Path.Add(a);
                    }
                    return Path;
                }
                if (LegalsActions(mynode.state) != null)
                {
                    foreach (Action a in LegalsActions(mynode.state))
                    {
                        Node ChildNode = mynode.ChildNode(this, mynode, a);
                        if (FrontierOrExploredContains(ChildNode) == false)
                        {
                            frontier.Add(ChildNode);
                        }
                    }
                }
            }
            timeisout:
            Debug.Log("No path found");//Reste su place si aucun chemin n<a ete trouve
            List<Action> NoPath = new List<Action>();
            NoPath.Add(new Action(from, from));
            return NoPath;
        }




        public List<Action> DFS(Procedural.Hex.TileInfo from, Procedural.Hex.TileInfo to)//Depth first search
        {

            State initState = new State(from);
            Node init = new Node(initState, null, null, 0);

            frontier.Clear();
            explored.Clear();
            solution.Clear();
            frontier.Add(init);
            Node mynode;
            int T = 0;
            while (frontier.Count > 0)
            {
                T++;
                if (T >= TimeOut)
                {
                    print("TimeOut" + T);
                    goto timeisout;
                }
                mynode = frontier[0];
                frontier.Remove(mynode);
                explored.Add(mynode);
                if (GoalTest(mynode.state.mytile, to))
                {
                    //return solution

                    List<Action> Path = new List<Action>();
                    foreach (Action a in Solution(mynode))//Execute la fonction Solution pour trouver la solution
                    {
                        //La solution se trouve aussi dans la liste solution
                        Path.Add(a);
                    }
                    return Path;
                }
                if (LegalsActions(mynode.state) != null)
                {
                    foreach (Action a in LegalsActions(mynode.state))
                    {
                        Node ChildNode = mynode.ChildNode(this, mynode, a);
                        if (FrontierOrExploredContains(ChildNode) == false)
                        {
                            frontier.Insert(0, ChildNode);//La difference avec BFS est ici
                        }
                    }
                }
            }
            timeisout:
            Debug.Log("No path found");//Reste su place si aucun chemin n<a ete trouve
            List<Action> NoPath = new List<Action>();
            NoPath.Add(new Action(from, from));
            return NoPath;
        }


        public List<Action> AStar(Procedural.Hex.TileInfo from, Procedural.Hex.TileInfo to)//A*
        {

            State initState = new State(from);
            Node init = new Node(initState, null, null, 0);

            frontier.Clear();
            explored.Clear();
            solution.Clear();
            frontier.Add(init);
            Node mynode;
            int T = 0;
            while (frontier.Count > 0)
            {
                T++;
                if (T >= TimeOut)
                {
                    print("TimeOut" + T);
                    goto timeisout;
                }
                //myNode vas etre celle qui les le plus proche de mon goal en ligne droite
                float Score = 99999;
                Node StarNode = null;
                foreach (Node n in frontier)
                {
                    float D = Vector2.Distance(new Vector2(n.state.mytile.Coox, n.state.mytile.Cooy), new Vector2(to.Coox, to.Cooy));
                    if (D < Score)
                    {
                        Score = D;
                        StarNode = n;
                    }
                }
                mynode = StarNode;
                frontier.Remove(mynode);
                explored.Add(mynode);
                if (GoalTest(mynode.state.mytile, to))
                {
                    //return solution

                    List<Action> Path = new List<Action>();
                    foreach (Action a in Solution(mynode))//Execute la fonction Solution pour trouver la solution
                    {
                        //La solution se trouve aussi dans la liste solution
                        Path.Add(a);
                    }
                    return Path;
                }
                if (LegalsActions(mynode.state) != null)
                {
                    foreach (Action a in LegalsActions(mynode.state))
                    {
                        Node ChildNode = mynode.ChildNode(this, mynode, a);
                        if (FrontierOrExploredContains(ChildNode) == false)
                        {
                            frontier.Add(ChildNode);
                        }
                    }
                }
            }
            timeisout:
            Debug.Log("No path found");//Reste su place si aucun chemin n<a ete trouve
            List<Action> NoPath = new List<Action>();
            NoPath.Add(new Action(from, from));
            return NoPath;
        }

    }
}

