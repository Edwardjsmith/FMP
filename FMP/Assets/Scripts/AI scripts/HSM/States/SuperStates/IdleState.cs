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
        Transition transitionToAlertEnemySpotted = new Transition();

        transitionToAlertEnemySpotted.Condition = agent.getTransitions().getEnemyTargetFound();
        transitionToAlertEnemySpotted.targetState = "Alert";
        transitions.Add(transitionToAlertEnemySpotted);

        Transition transitionToAlertHit = new Transition();
        transitionToAlertHit.Condition = agent.getTransitions().isHit();
        transitionToAlertHit.targetState = "Alert";
        transitions.Add(transitionToAlertHit);

        initialState = States["Patrol"];
    
    }

    public override void EnterState()
    {
        
    }
    public override void Update()
    {
        base.Update();

        if (agent.getSenses().getTarget(agent.getSenses().otherAgents).Count > 0)
        {
            agent.getData().enemyTarget = agent.getSenses().GetClosestObj(agent.getSenses().getTarget(agent.getSenses().otherAgents));
        }
    }
    public override void ExitState()
    {
        agent.getTransitions().amHit = false;
    }

}
