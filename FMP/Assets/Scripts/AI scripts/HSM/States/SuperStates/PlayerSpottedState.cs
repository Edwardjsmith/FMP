using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpottedState : SuperState
{
    public PlayerSpottedState(HSMAgent agent) : base(agent)
    {
        Transition transitionToAlert = new Transition();

        States.Add("Player detected", new PlayerDetected(agent));
        States.Add("FindCover", new findCover(agent));
        States.Add("fireFromCover", new fireFromCover(agent));
        States.Add("shortRangedAttack", new shortRangedAttack(agent));
        States.Add("Flank", new flankState(agent));

        transitionToAlert.Condition = agent.getTransitions().enemyLost;
        transitionToAlert.targetState = "Alert";
        transitions.Add(transitionToAlert);
    }


    public override void EnterState()
    {
        agent.getData().sightRange = 100;
        currentState = States["FindCover"];
        currentState.EnterState();
    }
    public override void Update()
    {
        base.Update();
    }
    public override void ExitState()
    {
        currentState.ExitState();
    }
}
