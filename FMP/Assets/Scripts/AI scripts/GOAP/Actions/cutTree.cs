using UnityEngine;

public class cutTree : goapAction
{
    GameObject[] trees;
    float cutTime = 8.0f;
    public cutTree() : base()
    {
        addPrecondition("hasAxe", true);
        addEffect("hasWood", true);
        addPreEffect("hasAxe", true);
    }

    private void Start()
    {
        trees = GameObject.FindGameObjectsWithTag("Tree");
    }

    public override bool checkTarget(goapAgent agent)
    {
        //target = agent.getSenses().GetClosestObj(trees.ToList());
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
                cutTime = 8.0f;
                if (Random.Range(0, 3) < 1)
                {
                    agent.tool.SetActive(false);
                }

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

    public override bool testAction(goapAgent agent)
    {
        throw new System.NotImplementedException();
    }
}
