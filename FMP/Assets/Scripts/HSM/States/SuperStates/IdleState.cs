using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : SuperState
{
    private static IdleState instance = null;

    private IdleState() : base()
    {
        if(instance != null)
        {
            return;
        }

        instance = this;

        transitions = new List<Transition>();
        Transition transitionToAlert = new Transition();

        transitionToAlert.Condition += AgentTriggers.isHit;
        transitionToAlert.targetState = AlertState.Instance();
        transitions.Add(transitionToAlert);
        initialState = Patrol.Instance();
    
    }

    public static IdleState Instance()
    {
        if(instance == null)
        {
            instance = new IdleState();
        }
        return instance;

    }
    public override void EnterState(HSMAgent agent)
    {
        Debug.Log("Entering idle");
    }
    public override void Update(HSMAgent agent)
    {
        base.Update(agent);
    }
    public override void ExitState(HSMAgent agent)
    {
        Debug.Log("Exiting idle");
    }

}
