
using System.Collections.Generic;
using UnityEngine;

public class Senses : MonoBehaviour
{
    baseAI agent;
    HSMAgent hsmagent;
    bool hsm = false;
    public LayerMask otherAgents;
    public LayerMask cover;
    List<GameObject> verifiedTargets;

  
    // Use this for initialization
    void Start ()
    {
        if(GetComponent<HSMAgent>())
        {
            agent = GetComponent<HSMAgent>();
            hsmagent = (HSMAgent)agent;
            hsm = true;
        }
        else
        {
            agent = GetComponent<baseAI>();
        }
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
        Collider[] potentialTargets = Physics.OverlapSphere(agent.transform.position, agent.getData().sightRange, layer);

        verifiedTargets.Clear();
        foreach (Collider col in potentialTargets)
        {
            if (col.gameObject != gameObject)
            {
                if (hsm && verifyTarget(col.gameObject))
                {
                    verifiedTargets.Add(col.gameObject);
                }
                else
                {
                    verifiedTargets.Add(col.gameObject);
                }
            }
        }
        if (hsm)
        {
            if (verifiedTargets.Count > 0)
            {
                hsmagent.getTransitions().enemyTargetFound = true;
            }
            else
            {
                hsmagent.getTransitions().enemyTargetFound = false;
            }
        }

        return verifiedTargets;
    }

    public List<GameObject> getTarget(LayerMask layer, Vector3 checkPos)
    {
        Collider[] potentialTargets = Physics.OverlapSphere(checkPos, agent.getData().sightRange, layer);

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
        if (hsm)
        {
            if (verifiedTargets.Count > 0)
            {
                hsmagent.getTransitions().enemyTargetFound = true;
            }
            else
            {
                hsmagent.getTransitions().enemyTargetFound = false;
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

    public GameObject GetClosestSuitableCover(List<GameObject> obj, Vector3 pos)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = pos;
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
    public GameObject GetFlankSuitableCover(List<GameObject> obj, Vector3 pos)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = pos;
        float currentDist = Vector3.Distance(pos, agent.getData().enemyTarget.transform.position);

        foreach (GameObject t in obj)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            float enemyDist = Vector3.Distance(t.transform.position, agent.getData().enemyTarget.transform.position);

            if (dist < minDist && enemyDist < currentDist)
            {
                tMin = t;
                minDist = dist;
            }

        }
        return tMin;
    }

    public void getCoverFlank(Vector3 position)
    {
        agent.getData().coverTarget = GetFlankSuitableCover(agent.getSenses().getTarget(cover), position).transform.position;

        Vector3 targetDir = agent.getData().enemyTarget.transform.position - agent.getData().coverTarget;

        agent.getData().coverTarget = agent.getData().coverTarget + (targetDir.normalized * -1f);
    }

    public void getCover()
    {
        agent.getData().coverTarget = GetClosestSuitableCover(agent.getData().getCover(), agent.transform.position).transform.position;

        Vector3 targetDir = agent.getData().enemyTarget.transform.position - agent.getData().coverTarget;

        agent.getData().coverTarget = agent.getData().coverTarget + (targetDir.normalized * -1f);
    }
}
