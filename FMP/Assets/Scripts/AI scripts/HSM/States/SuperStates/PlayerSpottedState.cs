using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpottedState : SuperState
{
    public PlayerSpottedState(HSMAgent agent) : base(agent)
    {
        transitions = new List<Transition>();
    }


    public override void EnterState()
    {
        Debug.Log("Entering player spotted");
    }
    public override void Update()
    {

    }
    public override void ExitState()
    {
        Debug.Log("Exiting player spotted");
    }
}
