using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : State<HSMAgent>
{
  
    public Guard(HSMAgent agent) : base(agent)
    {
        transitions = new List<Transition>();
        Transition transitionToPatrol = new Transition();

        transitionToPatrol.Condition = agent.getTransitions().TransitionToGuard;
        transitionToPatrol.targetState = "Patrol";
        transitions.Add(transitionToPatrol);
    }



    public override void EnterState()
    {
        Debug.Log("Entering inner state guard");
        agent.getData().GetAgent().isStopped = true;
    }

    public override void ExitState()
    {
        Debug.Log("Exiting inner state guard");
        agent.getData().GetAgent().isStopped = false;
    }

    public override void Update()
    {
        
    }
}
