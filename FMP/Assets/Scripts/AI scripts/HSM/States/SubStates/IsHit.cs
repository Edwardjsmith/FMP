using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsHit : State<HSMAgent>
{

public IsHit(HSMAgent agent) : base(agent)
    {


        transitions = new List<Transition>();
        Transition transitionToAlert = new Transition();

        //transitionToAlert.Condition += AgentTriggers.trigger3;
        transitionToAlert.targetState = "Idle";
        transitions.Add(transitionToAlert);

    }


    public override void EnterState()
    {
        Debug.Log("Entering inner state IsHit");
    }

    public override void ExitState()
    {
        Debug.Log("Exiting inner state IsHit");
    }

    public override void Update()
    {

    }
}
