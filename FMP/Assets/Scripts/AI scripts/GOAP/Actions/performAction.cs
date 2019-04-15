using UnityEngine;

public class performAction : goapAction
{
    float cutTime = 8.0f;
    public performAction() : base()
    {
        addPrecondition("hasAxe", true);
        addEffect("hasWood", true);
        addPreEffect("hasAxe", true);
    }

    private void Start()
    {
    }

    public override bool checkTarget(goapAgent agent)
    {
        return true;
    }

    public override bool executeAction(goapAgent agent)
    {
        cutTime -= Time.deltaTime;
        if (inRange)
        {
            agent.getAnim().Play("Attack");
            if (cutTime <= 0)
            {
                if (Random.Range(0, 3) < 1)
                {
                    agent.tool.SetActive(false);
                }
                cutTime = 8.0f;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public override bool taskComplete(goapAgent agent)
    {
        return executeAction(agent);
    }
}
