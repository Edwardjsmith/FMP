using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Senses : MonoBehaviour
{
    HSMAgent agent;
    public LayerMask otherAgents;
    public LayerMask cover;
    List<GameObject> verifiedTargets;

  
    // Use this for initialization
    void Start ()
    {
        agent = GetComponent<HSMAgent>();
        verifiedTargets = new List<GameObject>();
	}

    public bool verifyTarget(GameObject target)
    {
        if (Vector3.Dot(transform.forward, target.transform.position) > 0 && Vector3.Angle(transform.forward, target.transform.position) < agent.getData().fieldOfView)
        {
            return true;
        }
        
        return false;
    }

    public List<GameObject> getTarget(LayerMask layer)
    {
        Collider[] potentialTargets = Physics.OverlapSphere(transform.position, agent.getData().sightRange, layer);

        verifiedTargets.Clear();
        foreach(Collider col in potentialTargets)
        {
            if(col.gameObject != gameObject)
            {
                if(verifyTarget(col.gameObject))
                {
                    verifiedTargets.Add(col.gameObject);
                }
            }
        }
        if (GetComponent<HSMAgent>())
        {
            if (verifiedTargets.Count > 0)
            {
                agent.getTransitions().enemyTargetFound = true;
            }
            else
            {
                agent.getTransitions().enemyTargetFound = false;
            }
        }

        return verifiedTargets;
    }

    public GameObject GetClosestObj(List<GameObject> obj)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in obj)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        
        return tMin;
    }

    public GameObject GetClosestSuitableCover(List<GameObject> obj)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in obj)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            float enemyDist = Vector3.Distance(t.transform.position, agent.getData().enemyTarget.transform.position);
      

               if(enemyDist > agent.getData().safeDistance && dist < minDist)
               {
                    tMin = t;
                    minDist = dist;
               }
          
        }
        return tMin;
        }

        

    public bool targetDirection(GameObject target)
    {
        if(target.transform.position.z > transform.position.z) //Enemy somewhere in front
        {
            return true;
        }
        else //Enemy somewhere behind
        {
            return false;
        }
    }

    public void getCover()
    {
        if (agent.getData().coverTarget == null)
        {
            agent.getData().coverTarget = GetClosestSuitableCover(agent.getSenses().getTarget(cover));

            if (agent.getData().coverTarget != null)
            {
                if (targetDirection(agent.getData().enemyTarget))
                {
                    agent.getData().coverTarget = agent.getData().coverTarget.transform.GetChild(1).gameObject;
                }
                else
                {
                    agent.getData().coverTarget = agent.getData().coverTarget.transform.GetChild(0).gameObject;
                }
            }
        }
    }
}
