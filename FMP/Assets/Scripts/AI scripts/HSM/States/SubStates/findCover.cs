using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class findCover : State<HSMAgent>
{

    public findCover(HSMAgent agent) : base(agent)
    {


        transitions = new List<Transition>();
        Transition transitionToPlayerDetected = new Transition();

        transitionToPlayerDetected.Condition = agent.getTransitions().inCover;
        transitionToPlayerDetected.targetState = "Player detected";
        transitions.Add(transitionToPlayerDetected);

    }


    public override void EnterState()
    {
        
    }

    public override void ExitState()
    {
        agent.getTransitions().covered = false;
    }

    public override void Update()
    {
        if(agent.getData().enemyTarget != null && Vector3.Distance(agent.getData().enemyTarget.transform.position, agent.transform.position) < agent.getData().safeDistance)
        {
            agent.getData().coverTarget = null;
        }

        agent.getSenses().getCover();


        if (agent.getActions().takeCover())
        {
            agent.getAnim().Play("crouchAim");
            if (agent.GetWeapon().ammo < 5)
            {
                agent.getActions().reload();
            }

            if (!agent.getActions().reloading)
            {
                agent.getTransitions().covered = true;
            }
        }

    }
}

