using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : SuperState
{



    public AlertState(HSMAgent agent) : base(agent)
    {

        transitions = new List<Transition>();
        Transition transitionToIdle = new Transition();

        States.Add("Player detected", new PlayerDetected(agent));

        // transitionToIdle.Condition += AgentTriggers.trigger1;
        transitionToIdle.targetState = "Idle";
        transitions.Add(transitionToIdle);
        initialState = States["Player detected"];

    }


    public override void EnterState()
    {
        Debug.Log("Entering alert");
    }
    public override void Update()
    {
        base.Update();
    }
    public override void ExitState()
    {
        Debug.Log("Exiting alert");
    }
}
