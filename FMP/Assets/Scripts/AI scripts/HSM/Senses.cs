using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Senses : MonoBehaviour
{
    HSMAgent agent;
    public LayerMask otherAgents;

	// Use this for initialization
	void Start ()
    {
        agent = GetComponent<HSMAgent>();
	}

    public bool verifyTarget(GameObject target)
    {
        if (Vector3.Dot(transform.forward, target.transform.position) > 0 && Vector3.Angle(transform.forward, target.transform.position) < agent.getData().fieldOfView)
        {
            return true;
        }
        
        return false;
    }

    public bool getTarget()
    {
        Collider[] potentialEnemies = Physics.OverlapSphere(transform.position, agent.getData().sightRange, otherAgents);

        foreach(Collider col in potentialEnemies)
        {
            if(col.gameObject != gameObject)
            {
                return verifyTarget(col.gameObject);
            }
        }

        return false;
    }
}
