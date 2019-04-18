﻿
using UnityEngine;
using System.Threading;
using System.Collections.Generic;

public class testTacticalPlayerScript : MonoBehaviour
{

    Pathfinding pathFinding;
    Vector3 followPath;
    public int currentPathPoint = 0;
    float maxTurnSpeed = 5;
    float maxMoveSpeed = 5;

    Thread pathfindingThread;

    public bool canMove = false;
    // Use this for initialization
    void Start ()
    {
        pathFinding = GetComponent<Pathfinding>();
    }
	
	// Update is called once per frame
	void LateUpdate ()
    {
        if (!pathFinding.threadCreated)
        {
            pathFinding.threadCreated = true;
            pathfindingThread = new Thread(new ThreadStart(pathFinding.calculatePath));
            pathfindingThread.Start();
        }
        Movement();
    }

    private bool inBounds(int index, List<Node> List)
    {
        return (index >= 0) && (index < List.Count);
    }

    private void Movement()
    {
        if(canMove && inBounds(currentPathPoint, pathFinding.newPath))
        {
            followPath = pathFinding.newPath[currentPathPoint].worldPos;
            Vector3 pathPosition = new Vector3(followPath.x, transform.position.y, followPath.z);
            Vector3 targetDirection = pathPosition - transform.position;
            targetDirection = targetDirection.normalized;

            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                    Quaternion.LookRotation(targetDirection),
                                                     maxTurnSpeed * Time.deltaTime);

            transform.position += transform.forward * Time.deltaTime * maxMoveSpeed;

            if (Vector3.Distance(pathPosition, transform.position) <= 0.7f)
            {
                currentPathPoint = (currentPathPoint + 1) % pathFinding.newPath.Count;
            }
        }
        else
        {
            transform.position += transform.forward * Time.deltaTime * maxMoveSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "point")
        {
            canMove = false;
            currentPathPoint = 0;
            pathFinding.createNewPath = true;
        }
    }
}
