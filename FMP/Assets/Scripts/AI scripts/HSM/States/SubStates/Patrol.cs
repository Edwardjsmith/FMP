using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State<HSMAgent>
{

    public Patrol(HSMAgent agent) : base(agent)
    {
        transitions = new List<Transition>();
        Transition transitionToGuard = new Transition();

        transitionToGuard.Condition = agent.getTransitions().TransitionToGuard;
        transitionToGuard.targetState = "Guard";
        transitions.Add(transitionToGuard);
    }


    public override void EnterState()
    {
        agent.getAnim().Play("Walk");
        agent.getActions().moveTo(agent.patrolTarget);
    }

    public override void ExitState()
    {
        agent.patrolTarget = null;
    }

    public override void Update()
    {
        
    }
}
