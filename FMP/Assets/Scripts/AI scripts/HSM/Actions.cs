using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Actions : MonoBehaviour
{
    HSMAgent agent;
    private void Start()
    {
        agent = GetComponent<HSMAgent>();
    }

    public bool moveTo(GameObject target)
    {
        agent.getData().GetAgent().destination = target.transform.position;
        if (transform.position != target.transform.position)
        {
            return false;
        }

        return true;
    }

    public bool Shoot(GameObject target)
    {
        if (Vector3.Distance(transform.position, target.transform.position) < agent.getData().weaponRange)
        {
            agent.GetWeapon().Fire();
            return true;
        }

        return false;
    }

    Transform GetClosest(Transform[] obj, string tag)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in obj)
        {
            if (t.tag == tag)
            {
                float dist = Vector3.Distance(t.position, currentPos);
                if (dist < minDist)
                {
                    tMin = t;
                    minDist = dist;
                }
            }
        }
        return tMin;
    }

    public bool takeCover(GameObject target)
    {
        moveTo(target);

        if(Vector3.Distance(transform.position, target.transform.position) < 1)
        {

        }

        return false; 
    }

}
