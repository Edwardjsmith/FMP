using UnityEngine;
using UnityEngine.AI;

public class agentActions : MonoBehaviour
{
    baseAI agent;
    public int fireCounter = 0;

    private void Start()
    {
        agent = GetComponent<baseAI>();
    }

    public bool moveTo(GameObject target)
    {
        agent.getData().GetAgent().destination = target.transform.position;
        if (Vector3.Distance(transform.position, target.transform.position) > 1.5f)
        {
            return false;
        }

        return true;
    }

    public Vector3 moveToRandom(Vector3 seed)
    {
        Vector3 randomMove = Random.insideUnitSphere * 100;
        randomMove += seed;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomMove, out hit, 100, 1))
        {
            return hit.position;
        }
        else
        {
            return Vector3.zero;
        }
    }
    public bool moveTo(Vector3 target)
    {
        agent.getData().GetAgent().destination = target;
        Debug.Log(target);
        if (Vector3.Distance(transform.position, target) > 1.5f)
        {
            return false;
        }

        return true;
    }


    public void Shoot(GameObject target)
    {
        if (Vector3.Distance(transform.position, target.transform.position) < agent.GetWeapon().projectileRange)
        {
            if (agent.GetWeapon().ammo > 0)
            {
                if (agent.GetWeapon().Fire())
                {
                    Debug.Log("Fire!");
                    fireCounter++;
                }
                
            }
            else
            {
                agent.GetWeapon().reload(agent);
            }
        }
    }

    

    public void Aim()
    {
        transform.LookAt(new Vector3(agent.getData().enemyTarget.transform.position.x, transform.position.y, agent.getData().enemyTarget.transform.position.z));
        transform.GetChild(0).transform.LookAt(agent.getData().enemyTarget.transform.position);
        
    }


    public bool takeCover()
    {
        return moveTo(agent.getData().coverTarget);
    }
}
