﻿
using System.Collections.Generic;

public class HSM
{
    public SuperState currentState;
    public HSMAgent Agent;

    public SortedDictionary<string, SuperState> States;

    public HSM(HSMAgent agent)
    {
        //create states
        States = new SortedDictionary<string, SuperState>();

        States.Add("Idle", new IdleState(agent));
        States.Add("Alert", new AlertState(agent));
        States.Add("Player spotted", new PlayerSpottedState(agent));

        Agent = agent;
        currentState = States["Idle"];
    }

    public string currentSuperState()
    {
        return "Current super state: " + currentState.ToString();
    }

    public string currentSubState()
    {
        return "Current sub state: " + currentState.getCurrentSubState();
    }

	// Update is called once per frame
	public void Update ()
    {
        //run current state
		if(currentState != null)
        {
            currentState.Update();
        }
	}

    public void LateUpdate()
    {
        //check for transitions in super states
        foreach (Transition transition in currentState.transitions) //Go through each transition to see if any have been triggered
        {
            if (transition.Condition.Invoke())
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

