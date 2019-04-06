using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testTacticalPlayerScript : MonoBehaviour
{

    Pathfinding pathFinding;
    Vector3 followPath;
    public int currentPathPoint = 0;
    float maxTurnSpeed = 5;
    float maxMoveSpeed = 5;

    // Use this for initialization
    void Start ()
    {
        pathFinding = GetComponent<Pathfinding>();
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        Movement();
        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void Movement()
    {
        if (pathFinding.newPath[currentPathPoint].worldPos != null)
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "point")
        {
            currentPathPoint = 0;
            pathFinding.pathFound = false;
        }
    }
}
