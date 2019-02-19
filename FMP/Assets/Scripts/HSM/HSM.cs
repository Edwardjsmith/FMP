﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSM
{
    public SuperState currentState;
    public HSMAgent Agent;

    public HSM(HSMAgent agent)
    {
        Agent = agent;
        currentState = IdleState.Instance();
    }

	// Update is called once per frame
	public void Update ()
    {
		if(currentState != null)
        {
            currentState.Update(Agent);
        }
	}

    public void LateUpdate()
    {
        foreach (Transition transition in currentState.transitions) //Go through each transition to see if any have been triggered
        {
            if (transition.Condition.Invoke())
            {
                currentState.ExitState(Agent);
                currentState = (SuperState)transition.targetState;
                currentState.EnterState(Agent);
            }
            else
            {
                currentState.FixedUpdate(Agent);
            }
        }
    }
}

