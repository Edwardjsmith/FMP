using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpottedState : SuperState
{
    private static PlayerSpottedState instance = null;

    private PlayerSpottedState() : base()
    {
        
        if (instance != null)
        {
            return;
        }

        instance = this;

        transitions = new List<Transition>();

    }

    public static PlayerSpottedState Instance()
    {
        if (instance == null)
        {
            instance = new PlayerSpottedState();
        }
        return instance;
    }

    public override void EnterState(HSMAgent agent)
    {
        Debug.Log("Entering player spotted");
    }
    public override void Update(HSMAgent agent)
    {

    }
    public override void ExitState(HSMAgent agent)
    {
        Debug.Log("Exiting player spotted");
    }
}
