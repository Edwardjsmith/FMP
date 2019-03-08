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
        agent.getSenses().getCover();


        if (agent.getActions().takeCover())
        {
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

