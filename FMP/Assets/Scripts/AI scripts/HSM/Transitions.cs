
using UnityEngine;

public class Transitions : MonoBehaviour
{
    float guardTimer = 10.0f;
    public bool enemyTargetFound = false;

    public bool amHit = false;
    public bool covered = false;

    float coverTimer = 1.5f;
    HSMAgent agent;

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
        if(isHit())
        {
            return true;
        }
        else
        {
            if(coverTimer <= 0)
            {
                coverTimer = 1.5f;
                return true;
            }
            coverTimer -= Time.deltaTime;
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
