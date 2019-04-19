
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
    public int tacticalModifier = 1000;

    public GameObject tile;
    public Renderer rend;

    public LayerMask wall;
    public LayerMask enemy;

    public Collider[] hit;

    public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
    {
        walkable = _walkable;
        worldPos = _worldPos;

        gridX = _gridX;
        gridY = _gridY;

    }
    public int fCost()
    {
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
