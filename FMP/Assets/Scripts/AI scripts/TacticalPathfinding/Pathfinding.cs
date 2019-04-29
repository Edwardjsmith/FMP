﻿
using UnityEngine;
using System.Collections.Generic;
using System.Threading;

public class Pathfinding : MonoBehaviour
{
    public Grid grid;

    GameObject[] targetObj;
    public Vector3 target;
    public List<Node> newPath;

    public List<Vector3> targetPositions;
   
    public bool threadCreated = false;
    tacticalAgent player;
    Vector3 playerPos;

    public bool createNewPath = true;
    bool threadRunning = true;

    Color red;
    Color blue;
    Color yellow;
    Color clear;
    float transparency = 0.1f;
    bool hideMetrics = false;
    Thread pathfindingThread;

    private void Start()
    {
        clear = new Color(0, 0, 0, 0);
        targetObj = GameObject.FindGameObjectsWithTag("point");
        player = GetComponentInParent<tacticalAgent>();
        playerPos = player.transform.position;
        targetPositions = new List<Vector3>();

        foreach(GameObject go in targetObj)
        {
            targetPositions.Add(go.transform.position);
        }

        plotTarget();

        red = new Color(Color.red.r, Color.red.g, Color.red.b, transparency);
        blue = new Color(Color.blue.r, Color.blue.g, Color.blue.b, transparency);
        yellow = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, transparency);

        pathfindingThread = new Thread(new ThreadStart(calculatePath));
        pathfindingThread.Start();
    }

    private void Update()
    {
        playerPos = player.transform.position;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            hideMetrics = !hideMetrics;
        }
    }

    private void FixedUpdate()
    {
        if (grid != null)
        {
            if (!hideMetrics)
            {
                foreach (Node node in grid.worldGrid)
                {
                    node.rend.material.color = (node.enemyLineOfSight) ? red : clear;

                    if (!node.walkable) node.rend.material.color = yellow;

                    if (newPath != null)
                    {
                        if (newPath.Contains(node))
                        {
                            node.rend.material.color = blue;
                        }
                    }
                }
            }
            else
            {
                foreach(Node n in grid.worldGrid)
                {
                    n.rend.material.color = Color.clear;
                }
            }
        }
    }


    public void calculatePath()
    {
        while (threadRunning)
        {
            Vector3 startPos = playerPos;
            Vector3 targetPos = target;

            Node startNode = grid.getNodefromWorldPoint(startPos);
            Node targetNode = grid.getNodefromWorldPoint(targetPos);
            //targetNode.walkable = true;

            List<Node> openSet = new List<Node>();
            HashSet<Node> closedSet = new HashSet<Node>();

            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet[0];

                for(int i = 0; i < openSet.Count; i++)
                {
                    if(openSet[i].fCost() < currentNode.fCost() || (openSet[i].fCost() == currentNode.fCost() && openSet[i].hCost < currentNode.hCost))
                    {
                        currentNode = openSet[i];
                    }
                }

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    retracePath(startNode, targetNode);
                }

                foreach (Node neighbour in grid.getNeighbours(currentNode))
                {
                    if (!neighbour.walkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newMoveCostToNeighbour = currentNode.gCost + getDistanceToGoal(currentNode, neighbour);

                    if (newMoveCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMoveCostToNeighbour;
                        neighbour.hCost = getDistanceToGoal(neighbour, targetNode);

                        neighbour.parent = currentNode;

                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                    }
                }
            }
        }
    }

  

    void retracePath(Node start, Node end)
    {
        List<Node> path = new List<Node>();
        Node currentNode = end;

        while(currentNode != start)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        newPath = path;
        player.canMove = true;
    }

    int getDistanceToGoal(Node a, Node b)
    {
        float distX = a.gridX - b.gridX;
        float distY = a.gridY - b.gridY;

        return (int)Mathf.Sqrt((distX * distX) + (distY * distY));
    }

    public void plotTarget()
    {
        Vector3 newTarget;
        do
        {
            newTarget = targetPositions[Random.Range(0, targetPositions.Count)];

        } while (!grid.getNodefromWorldPoint(newTarget).walkable || newTarget == target);

        target = newTarget;
    }

    private void OnDisable()
    {
        threadRunning = false;
        pathfindingThread.Join();
    }
}
