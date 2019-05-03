
using UnityEngine;

public class moveToAlertSpot : State<HSMAgent>
{
    bool transitionToLookAround()
    {
        return lookAround;
    }

    bool lookAround = false;
    public moveToAlertSpot(HSMAgent agent) : base(agent)
    {
        Transition transitionToLook = new Transition();

        transitionToLook.Condition = transitionToLookAround;
        transitionToLook.targetState = "scanPoint";
        transitions.Add(transitionToLook);
    }
    public override void EnterState()
    {
        agent.getActions().moveTo(agent.getData().alertPosition);
    }

    public override void ExitState()
    {
        lookAround = false;
    }

    public override void Update()
    {
        if(agent.getActions().moveTo(agent.getData().alertPosition))
        {
            lookAround = true;
        }
    }
}
