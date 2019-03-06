using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetected : State<HSMAgent>
{

    public PlayerDetected(HSMAgent agent) : base(agent)
    {


        transitions = new List<Transition>();
        Transition transitionToAlert = new Transition();

       // transitionToAlert.Condition += AgentTriggers.trigger1;
        //transitionToAlert.targetState = new IsHit(agent);
        transitions.Add(transitionToAlert);

    }



    public override void EnterState()
    {
        Debug.Log("Entering inner state PlayerDetected");
    }

    public override void ExitState()
    {
        Debug.Log("Exiting inner state PlayerDetected");
    }

    public override void Update()
    {
        
    }
}
