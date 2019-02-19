using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : SuperState
{

    private static AlertState instance = null;

    private AlertState() : base()
    {
        if(instance != null)
        {
            return;
        }

        instance = this;

        transitions = new List<Transition>();
        Transition transitionToIdle = new Transition();

        transitionToIdle.Condition += AgentTriggers.trigger1;
        transitionToIdle.targetState = AlertState.Instance();
        transitions.Add(transitionToIdle);
        initialState = IsHit.Instance();

    }

    public static AlertState Instance()
    {
        if (instance == null)
        {
            instance = new AlertState();
        }
        return instance;
    }

    public override void EnterState(HSMAgent agent)
    {
        Debug.Log("Entering alert");
    }
    public override void Update(HSMAgent agent)
    {
        base.Update(agent);
    }
    public override void ExitState(HSMAgent agent)
    {
        Debug.Log("Exiting alert");
    }
}
