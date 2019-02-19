using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : State<HSMAgent>
{
    private static Guard instance = null;

    private Guard() : base()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;

        transitions = new List<Transition>();
        Transition transitionToAlert = new Transition();

        transitionToAlert.Condition += AgentTriggers.trigger3;
        transitionToAlert.targetState = Patrol.Instance();
        transitions.Add(transitionToAlert);

    }

    public static Guard Instance()
    {
        if (instance == null)
        {
            instance = new Guard();
        }
        return instance;
    }

    public override void EnterState(HSMAgent agent)
    {
        Debug.Log("Entering inner state guard");
    }

    public override void ExitState(HSMAgent agent)
    {
        Debug.Log("Exiting inner state guard");
    }

    public override void Update(HSMAgent agent)
    {
        
    }
}
