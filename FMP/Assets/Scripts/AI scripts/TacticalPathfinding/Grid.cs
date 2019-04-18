using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour
{
    public Vector2 gridWorldSize;
    float nodeRadius = 1f;
    public Node[,] worldGrid;

    public float nodeDiameter;
    int gridSizeX, gridSizeY;
    Vector3 worldBottomLeft;

    public GameObject tile;

    public LayerMask enemy;
    public LayerMask wall;

    int xPos, zPos;


    private void Start()
    {
        
        gridWorldSize = new Vector2(transform.localScale.x - 1, transform.localScale.z - 1); //Sizes the grid depending on the size of the floor

        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        createGrid();
    }


    public int maxSize
    {
        get
        {
            return gridSizeX * gridSizeY;
        }
    }
    public Node nodeFromWorldPoint(Vector3 worldPos)
    {
        float percentX = (worldPos.x - xPos) / gridWorldSize.x + 0.5f - (nodeRadius / gridWorldSize.x);
        float percentY = (worldPos.z - zPos) / gridWorldSize.y + 0.5f - (nodeRadius / gridWorldSize.y);

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return worldGrid[x, y];

    }

    public List<Node> getNeighbours(Node n)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }

                int checkX = n.gridX + x;
                int checkY = n.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(worldGrid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    void calculateNodeFCost()
    {
        foreach (Node n in worldGrid)
        {
            n.tacticalCost = 0;
            n.enemyLineOfSight = false;
            n.hit = Physics.OverlapSphere(n.worldPos, 0.1f, enemy);

            foreach (Collider h in n.hit)
            {
                if (h.tag == "enemy")
                {
                    RaycastHit hitInfo;
                    if (Physics.Linecast(n.worldPos, h.transform.position, out hitInfo, wall))
                    {
                        if (hitInfo.transform.name != "Wall")
                        {
                            float proximity = Vector3.Distance(n.worldPos, h.transform.position);
                            n.tacticalCost += n.tacticalModifier / proximity;
                            n.enemyLineOfSight = true;
                        }
                    }
                }
            }
        }
    }

    private void Update()
    {
        calculateNodeFCost();
    }
    void createGrid()
    {
        xPos = (int)transform.position.x;
        zPos = (int)transform.position.z;
        worldGrid = new Node[gridSizeX, gridSizeY];

        worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 //Gives bottom left corner of world
            - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius)
                    + Vector3.forward * (y * nodeDiameter + nodeRadius);

                Collider[] hit = Physics.OverlapSphere(worldPoint, nodeRadius);

                bool walkable = true;

                foreach (Collider h in hit)
                {
                    if (h.tag == "unwalkable")
                    {
                        walkable = false;
                    }

                }
                worldGrid[x, y] = new Node(walkable, worldPoint, x, y);
                GameObject t = Instantiate(tile, new Vector3(worldPoint.x, worldPoint.y + 0.6f, worldPoint.z), tile.transform.rotation);
                worldGrid[x, y].tile = t;
                worldGrid[x, y].rend = t.GetComponent<Renderer>();
                worldGrid[x, y].enemy = enemy;
                worldGrid[x, y].wall = wall;
            }

        }
    }
}
