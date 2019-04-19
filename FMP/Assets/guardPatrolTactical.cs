using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guardPatrolTactical : MonoBehaviour {

    Vector3 targetPos;
    float maxMoveSpeed;
    float maxTurnSpeed;

    public GameObject targets;

    bool moving = true;

    float idleTimer = 2.0f;
	// Use this for initialization
	void Start ()
    {
        maxMoveSpeed =  2.0f;
        maxTurnSpeed = 10.0f;
        targetPos = targets.transform.GetChild(Random.Range(0, targets.transform.childCount)).transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (moving)
        {
            Vector3 targetDirection = targetPos - transform.position;
            targetDirection = targetDirection.normalized;

            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                    Quaternion.LookRotation(targetDirection),
                                                     maxTurnSpeed * Time.deltaTime);

            transform.position += transform.forward * Time.deltaTime * maxMoveSpeed;

            if(Vector3.Distance(targetPos, transform.position) < 0.5f)
            {
                moving = false;
            }
        }
        else
        {
            if(idleTimer <= 0)
            {
                moving = true;
                idleTimer = 2.0f;

                Vector3 temp;

                do
                {
                    temp = targetPos = targets.transform.GetChild(Random.Range(0, targets.transform.childCount)).transform.position;
                } while (temp != targetPos);

                targetPos = temp;
            }

            idleTimer -= Time.deltaTime;
        }
    }
}
