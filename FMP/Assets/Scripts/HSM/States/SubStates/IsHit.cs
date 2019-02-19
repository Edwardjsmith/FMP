using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsHit : State<HSMAgent>
{
    private static IsHit instance = null;

    private IsHit() : base()
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

    public static IsHit Instance()
    {
        if (instance == null)
        {
            instance = new IsHit();
        }
        return instance;

    }


    public override void EnterState(HSMAgent agent)
    {
        Debug.Log("Entering inner state IsHit");
    }

    public override void ExitState(HSMAgent agent)
    {
        Debug.Log("Exiting inner state IsHit");
    }

    public override void Update(HSMAgent agent)
    {

    }
}
