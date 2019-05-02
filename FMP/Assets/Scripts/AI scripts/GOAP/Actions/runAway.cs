
public class runAway : goapAction
{
    runAway() : base()
    {
        addEffect("Safe", true);
        addPrecondition("Safe", false);
    }
    public override bool checkTarget(goapAgent agent)
    {
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

}
