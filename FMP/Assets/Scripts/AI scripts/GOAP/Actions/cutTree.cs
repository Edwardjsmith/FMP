using UnityEngine;

public class cutTree : goapAction
{
    GameObject[] trees;
    
    public cutTree() : base()
    {
        //addPrecondition("hasWood", false);
        addPrecondition("hasAxe", true);
        addEffect("hasWood", true);
        //addPreEffect("hasAxe", true);
        
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
        if (inRange)
        {
            return true;
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
