using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowHealthState : SuperState
{ 
    private static LowHealthState instance = null;

    private LowHealthState() : base()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;

        transitions = new List<Transition>();

    }

    public static LowHealthState Instance()
    {
        if (instance == null)
        {
            instance = new LowHealthState();
        }
        return instance;
    }

    public override void EnterState(HSMAgent agent)
    {
        Debug.Log("Entering low health");
    }
    public override void Update(HSMAgent agent)
    {

    }
    public override void ExitState(HSMAgent agent)
    {
        Debug.Log("Exiting low health");
    }
}
