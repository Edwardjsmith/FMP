using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flockBehaviour : MonoBehaviour {

  
    Vector3 averageHeading;
    Vector3 averagePos;
    float neighbourDistance = 3.0f;
    bool turning = false;
    public Vector3 goalPos;

    float speed = 5.0f;
    float rotationSpeed = 10.0f;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        goalPos = flockController.goalPos;
        roam(this);
    }

    public void roam(flockBehaviour flocker)
    {
        turning = Vector3.Distance(transform.position, Vector3.zero) >= flockController.patrolAreaSize ? true : false;

        if (turning)
        {
            Vector3 direction = Vector3.zero - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                    Quaternion.LookRotation(direction),
                                                      flocker.rotationSpeed * Time.deltaTime);
        }
        else
        {
            if (Random.Range(0, 5) < 1)
            {
                applyRules(flocker);
            }
        }


        transform.Translate(0, 0, Time.deltaTime * speed);
        
    }

    void applyRules(flockBehaviour flocker)
    {
        GameObject[] gos;
        gos = flockController.totalEnemies;

        Vector3 vCentre = Vector3.zero;
        Vector3 vAvoid = Vector3.zero;
        float gSpeed = 0.1f;

        

        

        int groupSize = 0;

        foreach(GameObject go in gos)
        {
            if (go != this.gameObject)
            {
                if (go != null)
                {
                    float dist = Vector3.Distance(go.transform.position, this.transform.position);

                    if (dist <= neighbourDistance)
                    {
                        vCentre += go.transform.position;
                        groupSize++;

                        if (dist < 15.0f)
                        {
                            vAvoid = vAvoid + ((this.transform.position - go.transform.position) * 10);
                        }

                        flockBehaviour anotherFlock = go.GetComponent<flockBehaviour>();
                        gSpeed = gSpeed + anotherFlock.speed;
                    }
                }
            }
        }

        if(groupSize > 0)
        {
            vCentre = vCentre / groupSize + (goalPos - this.transform.position);
           
            flocker.speed =  gSpeed / groupSize;

            Vector3 direction = (vCentre + vAvoid) - transform.position;

            if(direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                        Quaternion.LookRotation(direction),
                                                            flocker.rotationSpeed * Time.deltaTime);
            }
        }
    }
}
