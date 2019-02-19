using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : SuperState
{

    private static AlertState instance = null;

    private AlertState()
    {
        if(instance != null)
        {
            return;
        }

        instance = this;

        transitions = new List<Transition>();

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

    }
    public override void ExitState(HSMAgent agent)
    {
        Debug.Log("Exiting alert");
    }
}
