using UnityEngine;
public class pickUpAxe : goapAction
{
    GameObject axe;

    public pickUpAxe() : base()
    {
        //addPrecondition("hasWood", false);
        addPrecondition("hasAxe", false);
        addEffect("hasAxe", true);
        //addEffect("taskComplete", false);
    }
    private void Start()
    {
        axe = GameObject.Find("Axe");
    }
    public override bool checkTarget(goapAgent agent)
    {
        //target = axe;
        return true;
    }

    public override bool executeAction(goapAgent agent)
    {
        if(inRange)
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
