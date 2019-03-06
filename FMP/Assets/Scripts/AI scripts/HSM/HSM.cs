using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSM
{
    public SuperState currentState;
    public HSMAgent Agent;

    public SortedDictionary<string, SuperState> States;

    public HSM(HSMAgent agent)
    {
        States = new SortedDictionary<string, SuperState>();

        States.Add("Idle", new IdleState(agent));
        States.Add("Alert", new AlertState(agent));
        States.Add("Low health", new LowHealthState(agent));
        States.Add("Player spotted", new PlayerSpottedState(agent));

        Agent = agent;
        currentState = States["Idle"];
    }

	// Update is called once per frame
	public void Update ()
    {
		if(currentState != null)
        {
            currentState.Update();
        }
	}

    public void LateUpdate()
    {
        foreach (Transition transition in currentState.transitions) //Go through each transition to see if any have been triggered
        {
            if (transition.Condition != null && transition.Condition.Invoke())
            {
                currentState.ExitState();
                currentState = States[transition.targetState];
                currentState.EnterState();
            }
            else
            {
                currentState.FixedUpdate();
            }
        }
    }
}

