using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : SuperState
{

    public IdleState(HSMAgent agent) : base(agent)
    {
        States.Add("Patrol", new Patrol(agent));
        States.Add("Guard", new Guard(agent));

        transitions = new List<Transition>();
        Transition transitionToAlert = new Transition();

        transitionToAlert.Condition += agent.getSenses().getTarget;
        transitionToAlert.targetState = "Alert";
        transitions.Add(transitionToAlert);
        initialState = new Patrol(agent);
    
    }

    public override void EnterState()
    {
        Debug.Log("Entering idle");
    }
    public override void Update()
    {
        base.Update();
    }
    public override void ExitState()
    {
        Debug.Log("Exiting idle");
    }

}
