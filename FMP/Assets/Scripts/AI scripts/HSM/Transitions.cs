
using UnityEngine;

public class Transitions : MonoBehaviour
{
    float timer = 10.0f;
    public bool enemyTargetFound = false;

    public bool amHit = false;
    public bool covered = false;

    HSMAgent agent;

    private void Start()
    {
        agent = GetComponent<HSMAgent>();
    }
    public bool TransitionToGuard()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            timer = 10.0f;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool requireCover()
    {
        if(isHit())
        {
            amHit = false;
            return true;
        }
        else
        {
             if(agent.getActions().fireCounter >= 8)
            {
                agent.getActions().fireCounter = 0;
                return true;
            }
        }

        return false;
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
        if (!enemyTargetFound)
        {
            return true;
        }

        return false;
    }

}
