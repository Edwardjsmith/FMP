using UnityEngine;

public class dropOffWood : goapAction
{
    GameObject dropOff;
    public dropOffWood() : base()
    {
        addPrecondition("hasWood", true);
        //addEffect("hasWood", false);
        addEffect("taskComplete", true);
    }
    private void Start()
    {
        dropOff = GameObject.Find("DropOff");
    }
    public override bool checkTarget(goapAgent agent)
    {
        //target = dropOff;
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
