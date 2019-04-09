using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperState : State<HSMAgent>
{
    protected State<HSMAgent> initialState = null;
    protected State<HSMAgent> currentState = null;

    public SuperState(HSMAgent agent) : base(agent)
    {
        stateLevel = 0;
        //initialState = Patrol.Instance(agent);
    }

    public string getCurrentSubState()
    {
        return currentState.ToString();
    }

    public override void EnterState()
    {
        
    }

    public override void ExitState()
    {
        
    }

    public override void Update()
    {
        if (currentState == null)
        {
            currentState = initialState;
            currentState.EnterState();
        }

        currentState.Update();
    }

    public void FixedUpdate()
    {
        foreach(Transition transition in currentState.transitions)
        {
            if (transition.Condition == true)
            {
                if(States[transition.targetState].stateLevel == currentState.stateLevel)
                {
                    currentState.ExitState();
                    currentState = States[transition.targetState];
                    currentState.EnterState();
                }
                else
                {
                    currentState.ExitState();
                    currentState = null;
                }
            }
        }
    }
}
