using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node  : IHeapItem<Node> 
{

    public bool walkable;
    public Vector3 worldPos;

    public int gridX;
    public int gridY;

    Vector3 pos;

    public int gCost;
    public int hCost;
    public float tacticalCost = 0;

    public Node parent;

    int heapIndex;
    public bool enemyLineOfSight = false;
    public int tacticalModifier = 100;

    public GameObject tile;
    public Renderer rend;

    public LayerMask wall;
    public LayerMask enemy;

    public Collider[] hit;

    public List<Enemy> enemies;
    public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
    {
        walkable = _walkable;
        worldPos = _worldPos;

        gridX = _gridX;
        gridY = _gridY;
        enemies = new List<Enemy>();

    }

    public struct Enemy
    {
        public Vector3 pos;
        public string name;
    }

    public int fCost()
    {
        /*tacticalCost = 0;
        enemyLineOfSight = false;

        Collider[] hit = Physics.OverlapSphere(worldPos, 0.1f, enemy);
        foreach (Collider h in hit)
        {
            if (h.tag == "enemy")
            {
                RaycastHit hitInfo;
                if(Physics.Linecast(worldPos, h.transform.position, out hitInfo, wall))
                {
                    if (hitInfo.transform.name != "Wall" &&  hitInfo.collider != h)
                    {
                        float proximity = Vector3.Distance(worldPos, h.transform.position);
                        tacticalCost += tacticalModifier / proximity;
                        enemyLineOfSight = true;
                    }
                }
            }

        }*/

        return gCost + hCost + (int)tacticalCost;
    }

    int IHeapItem<Node>.heapIndex
    {
        get
        {
            return heapIndex;
        }

        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(Node nodeToCompare)
    {
        int compare = fCost().CompareTo(nodeToCompare.fCost());

        if(compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }
}
