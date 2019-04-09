using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetected : State<HSMAgent>
{

    public PlayerDetected(HSMAgent agent) : base(agent)
    {
        transitions = new List<Transition>();
        Transition transitionToCover = new Transition();

        transitionToCover.Condition = agent.getTransitions().requireCover();
        transitionToCover.targetState = "FindCover";
        transitions.Add(transitionToCover);

    }



    public override void EnterState()
    {

    }

    public override void ExitState()
    {
        agent.getData().GetAgent().isStopped = false;
    }

    public override void Update()
    {
        if(Vector3.Distance(agent.transform.position, agent.getData().enemyTarget.transform.position) < agent.GetWeapon().projectileRange)
        {
            agent.getData().GetAgent().isStopped = true;
            agent.getActions().Aim();
            if (agent.GetWeapon().enemyInSights())
            {
                agent.getActions().Shoot(agent.getData().enemyTarget);
            }
        }
        else
        {
            agent.getData().GetAgent().isStopped = false;
            agent.getActions().moveTo(agent.getData().enemyTarget);
        }
    }
}


