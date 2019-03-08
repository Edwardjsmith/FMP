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
        States.Add("FindCover", new findCover(agent));

        transitionToIdle.Condition += agent.getTransitions().enemyLost;
        transitionToIdle.targetState = "Idle";
        transitions.Add(transitionToIdle);
    }


    public override void EnterState()
    {
        if(agent.getTransitions().isHit())
        {
            currentState = States["FindCover"];
        }
        else
        {
            currentState = States["Player detected"];
        }
    }
    public override void Update()
    {
        base.Update();
    }
    public override void ExitState()
    {
        
    }
}
