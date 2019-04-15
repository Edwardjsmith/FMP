
public class pickUpTool : goapAction
{

    
    public pickUpTool() : base()
    {
        addEffect("hasAxe", true);
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
        if(inRange)
        {
            agent.tool.SetActive(true);
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
