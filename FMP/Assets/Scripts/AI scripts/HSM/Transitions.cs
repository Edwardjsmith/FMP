
using UnityEngine;

public class Transitions : MonoBehaviour
{
    float guardTimer = 10.0f;
    public bool enemyTargetFound = false;

    public bool amHit = false;
    public bool covered = false;
    HSMAgent agent;

    public bool playerHeard = false;
    public bool noTarget = false;
    private void Start()
    {
        agent = GetComponent<HSMAgent>();
    }
    public bool TransitionToGuard()
    {
        guardTimer -= Time.deltaTime;

        if(guardTimer <= 0)
        {
            guardTimer = 10.0f;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool requireCover()
    {
        return getEnemyTargetFound();
    }

    public bool isHit()
    {
        return amHit;
    }
    public bool inCover()
    {
        return covered;
    }
    public bool getEnemyTargetFound()
    {
        return enemyTargetFound;
    }

    public bool enemyLost()
    {
        if (agent.getSenses().getTarget(agent.getSenses().otherAgents).Count == 0)
        {
            agent.getData().alertPosition = agent.getData().enemyTarget.transform.position;
            return true;
        }

        return false;
    }

    public void heardPlayer(Vector3 pos)
    {
        if (Vector3.Distance(transform.position, pos) < agent.getData().hearingRadius)
        {
            agent.getData().alertPosition = pos;
            playerHeard = true;
        }
        else
        {
            playerHeard = false;
        }
    }

    public bool getPlayerHeard()
    {
        return playerHeard;
    }

    public bool noTargetFound()
    {
        return noTarget;
    }

}
