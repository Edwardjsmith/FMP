using UnityEngine;
public class pickUpAxe : goapAction
{
    GameObject axe;

    public pickUpAxe() : base()
    {
        //addPrecondition("hasAxe", false);
        addEffect("hasAxe", true);
    }
    private void Start()
    {
        axe = GameObject.Find("Axe");
    }
    public override bool checkTarget(goapAgent agent)
    {
        return true;
    }

    public override bool executeAction(goapAgent agent)
    {
        return true;
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
