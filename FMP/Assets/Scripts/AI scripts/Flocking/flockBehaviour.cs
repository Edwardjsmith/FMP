using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flockBehaviour : flockEntity
{
    Vector3 averageHeading;
    Vector3 averagePos;
    float neighbourDistance = 2.0f;
    bool turning = false;
    public Vector3 goalPos;

    Animator anim;

    public flockController controller;

    Vector3 rotationTarget;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("Swim");
    }

    // Update is called once per frame
    void Update()
    {
        goalPos = controller.goalPos;
        anim.speed = speed / 3;
        roam(this);
    }

    public void roam(flockBehaviour flocker)
    {
        if (turning)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                    Quaternion.LookRotation(rotationTarget),
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
        gos = controller.flockAgents;

        Vector3 vCentre = Vector3.zero;
        Vector3 vAvoid = Vector3.zero;
        float gSpeed = 0.1f;

        int groupSize = 0;

        foreach (GameObject go in gos)
        {
            if (go != gameObject)
            {
                if (go != null)
                {
                    float dist = Vector3.Distance(go.transform.position, transform.position);

                    if (dist <= neighbourDistance)
                    {
                        vCentre += go.transform.position;
                        groupSize++;

                        if (dist < 15.0f)
                        {
                            vAvoid = vAvoid + ((this.transform.position - go.transform.position));
                        }

                        flockEntity anotherFlock = go.GetComponent<flockEntity>();
                        gSpeed = gSpeed + anotherFlock.speed;
                    }
                }
            }
        }

        if (groupSize > 0)
        {
            vCentre = vCentre / groupSize + (goalPos - transform.position);

            flocker.speed = gSpeed / groupSize;

            Vector3 direction = (vCentre + vAvoid) - transform.position;

            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                        Quaternion.LookRotation(direction),
                                                            flocker.rotationSpeed * Time.deltaTime);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "flock")
        {
            rotationTarget = transform.position - other.gameObject.transform.position;
            turning = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "flock")
        {
            turning = false;
        }
    }
}


/*public float flockingAlignment(GameObject flocker, GameObject[] totalAgents)
{
    float averageSpeed = 0;

    foreach (GameObject agent in totalAgents)
    {
        if (agent != flocker)
        {
            averageSpeed += agent.GetComponent<flockBehaviour>().speed;
        }
    }

    return averageSpeed = (averageSpeed / (totalAgents.Length - 1));
}

public Vector3 flockingCohesion(GameObject flocker, GameObject[] totalAgents)
{

    Vector3 averagePos = Vector3.zero;

    foreach (GameObject agent in totalAgents)
    {
        if (agent != flocker)
        {
            averagePos += agent.transform.position;
        }
    }

    return (averagePos / (totalAgents.Length - 1)).normalized;
}


public Vector3 flockingSeperation(GameObject flocker, GameObject[] totalAgents)
{
    Vector3 seperation = Vector3.zero;

    foreach (GameObject agent in totalAgents)
    {
        if (agent != flocker)
        {
            if (Vector3.Distance(flocker.transform.position, agent.transform.position) <= seperationDistance)
            {
                seperation -= (agent.transform.position - flocker.transform.position);
            }
        }
    }

    return seperation.normalized * 5;
}*/


