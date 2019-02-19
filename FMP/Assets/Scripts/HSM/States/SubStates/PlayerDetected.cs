using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetected : State<HSMAgent>
{
    private static PlayerDetected instance = null;

    private PlayerDetected() : base()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;

        transitions = new List<Transition>();
        Transition transitionToAlert = new Transition();

        transitionToAlert.Condition += AgentTriggers.trigger1;
        transitionToAlert.targetState = IsHit.Instance();
        transitions.Add(transitionToAlert);

    }

    public static PlayerDetected Instance()
    {
        if (instance == null)
        {
            instance = new PlayerDetected();
        }
        return instance;

    }


    public override void EnterState(HSMAgent agent)
    {
        Debug.Log("Entering inner state PlayerDetected");
    }

    public override void ExitState(HSMAgent agent)
    {
        Debug.Log("Exiting inner state PlayerDetected");
    }

    public override void Update(HSMAgent agent)
    {
        
    }
}
