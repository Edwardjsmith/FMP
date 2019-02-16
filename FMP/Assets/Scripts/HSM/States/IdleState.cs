using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State<HSMAgent>
{
    private static IdleState instance = null;

    public static IdleState Instance()
    {
        if(instance == null)
        {
            instance = new IdleState();
        }
        return instance;
    }

    public override void Start()
    {
        instance = this;
        transitions = new List<Transition>();
        Transition transitionToAlert = new Transition();

        transitionToAlert.Condition += AgentTriggers.trigger1;
        transitionToAlert.targetState = AlertState.Instance;
    }
    public override void EnterState(HSMAgent agent)
    {
        Debug.Log("Entering idle");
    }
    public override void Update(HSMAgent agent)
    {
        
    }
    public override void ExitState(HSMAgent agent)
    {
        Debug.Log("Exiting idle");
    }

}
