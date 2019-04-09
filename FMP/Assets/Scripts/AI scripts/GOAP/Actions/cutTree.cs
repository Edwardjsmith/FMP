using System.Linq;
using UnityEngine;

public class cutTree : goapAction
{
    GameObject[] trees;
    
    public cutTree() : base()
    {
        //addPrecondition("hasWood", false);
        addPrecondition("hasAxe", true);
        addEffect("hasWood", true);
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
        return agent.hasWood = true;
    }

    public override bool taskComplete(goapAgent agent)
    {
        return agent.hasWood;
    }

    public override bool testAction(goapAgent agent)
    {
        throw new System.NotImplementedException();
    }
}
