using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowHealthState : SuperState
{ 
    public LowHealthState(HSMAgent agent) : base(agent)
    {
    

        transitions = new List<Transition>();

    }
    public override void EnterState()
    {
        Debug.Log("Entering low health");
    }
    public override void Update()
    {

    }
    public override void ExitState()
    {
        Debug.Log("Exiting low health");
    }
}
