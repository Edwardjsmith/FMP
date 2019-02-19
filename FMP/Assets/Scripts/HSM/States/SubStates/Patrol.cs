using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State<HSMAgent>
{

    private static Patrol instance = null;

    private Patrol() : base()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;

        transitions = new List<Transition>();
        Transition transitionToGuard = new Transition();

        transitionToGuard.Condition += AgentTriggers.trigger2;
        transitionToGuard.targetState = Guard.Instance();
        transitions.Add(transitionToGuard);
    }

    public static Patrol Instance()
    {
        if (instance == null)
        {
            instance = new Patrol();
        }
        return instance;

    }

    public override void EnterState(HSMAgent agent)
    {
        Debug.Log("Entering inner state patrol");
    }

    public override void ExitState(HSMAgent agent)
    {
        Debug.Log("Exiting inner state patrol");
    }

    public override void Update(HSMAgent agent)
    {
        
    }
}
