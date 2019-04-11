using Unity.Collections;
using Unity.Jobs;
using UnityEngine.Jobs;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;


struct findPathJob : IJobParallelFor
{
    public void Execute(int i)
    {

    }
}
public class Pathfinding : MonoBehaviour
{
    public Grid grid;

    public GameObject[] targetObj;
    Vector3 target;
    public List<Node> newPath;

    public bool pathFound = false;
    private void Start()
    {
        
    }

    private void Update()
    {
        if (!pathFound)
        {
            Vector3 newTarget;
            do
            {
                newTarget = targetObj[Random.Range(0, targetObj.Length - 1)].transform.position;

            } while (newTarget == target);


            target = newTarget;
            findPath(transform.position, target);
            pathFound = true;
        }
    }

    private void FixedUpdate()
    {
        if (grid != null)
        {
            foreach (Node node in grid.worldGrid)
            {
                node.rend.material.color = (node.enemyLineOfSight) ? Color.red : Color.white;

                if (!node.walkable) node.rend.material.color = Color.yellow;

                if (newPath != null)
                {
                    if (newPath.Contains(node))
                    {
                        node.rend.material.color = Color.blue;
                    }
                }
            }
        }
    }


    public void findPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = grid.nodeFromWorldPoint(startPos);
        Node targetNode = grid.nodeFromWorldPoint(targetPos);

        Heap<Node> openSet = new Heap<Node>(grid.maxSize);
        HashSet<Node> closedSet = new HashSet<Node>();

        openSet.add(startNode);

        while(openSet.count > 0)
        {
            Node currentNode = openSet.removeFirst();
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                StartCoroutine(retracePath(startNode, targetNode));
            }

            foreach (Node neighbour in grid.getNeighbours(currentNode))
            {

                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newMoveCostToNeighbour = currentNode.gCost + getDistance(currentNode, neighbour);

                if (newMoveCostToNeighbour < neighbour.gCost || !openSet.contains(neighbour))
                {
                    neighbour.gCost = newMoveCostToNeighbour;
                    neighbour.hCost = getDistance(neighbour, targetNode);

                    neighbour.parent = currentNode;

                    if (!openSet.contains(neighbour))
                    {
                        openSet.add(neighbour);

                    }
                    else
                    {
                        openSet.updateItem(neighbour);
                    }
                }
            }
        }
    }

  

    IEnumerator retracePath(Node start, Node end)
    {
        List<Node> path = new List<Node>();
        Node currentNode = end;

        while(currentNode != start)
        {
            //currentNode.worldPos = new Vector3(currentNode.worldPos.x, emp.transform.position.y, currentNode.worldPos.z); 
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        yield return null;

        path.Reverse();
        newPath = path;
    }

    int getDistance(Node a, Node b)
    {
        float distX = a.gridX - b.gridX;
        float distY = a.gridY - b.gridY;

        return (int)Mathf.Sqrt((distX * distX) + (distY * distY));
    }
}
