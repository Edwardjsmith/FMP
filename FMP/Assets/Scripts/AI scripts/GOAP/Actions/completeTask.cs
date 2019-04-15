using UnityEngine;

public class completeTask : goapAction
{
    public completeTask() : base()
    {
        addPrecondition("hasWood", true);
        //addEffect("hasWood", false);
        addEffect("taskComplete", true);
        addPreEffect("hasWood", true);
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
        if (inRange)
        {
            
            agent.setWorldState(agent.hasToolState);
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


}
