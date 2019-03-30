using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class agentActions : MonoBehaviour
{
    baseAI agent;

    float rateOfFire = 0.0f;
    float reloadTime = 3.0f;
    public int fireCounter = 0;
    public bool reloading = false;

    private void Start()
    {
        agent = GetComponent<baseAI>();
    }

    public bool moveTo(GameObject target)
    {

        agent.getData().GetAgent().destination = target.transform.position;
        if (Vector3.Distance(transform.position, target.transform.position) > 1.0f)
        {
            return false;
        }

        return true;
    }

    public Vector3 moveToRandom(Vector3 seed)
    {
        Vector3 randomMove = Random.insideUnitSphere * 10;
        randomMove += seed;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomMove, out hit, 10, 1))
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
        if (Vector3.Distance(transform.position, target) > 1.0f)
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
                if (rateOfFire <= 0)
                {
                    agent.GetWeapon().Fire();
                    Debug.Log("Fire!");
                    rateOfFire = 1.0f;
                    fireCounter++;
                }
                rateOfFire -= Time.deltaTime;
            }
            else
            {
                reload();
            }
        }
    }

    public void reload()
    {
        reloadTime -= Time.deltaTime;
        reloading = true;
        if(reloadTime <= 0)
        {
            reloadTime = 3.0f;
            agent.GetWeapon().ammo = 10;
            reloading = false;
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
