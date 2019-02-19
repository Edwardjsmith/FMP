using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperState : State<HSMAgent>
{
    protected State<HSMAgent> initialState = null;
    protected State<HSMAgent> currentState = null;

    public SuperState() : base()
    {
        stateLevel = 0;
        initialState = Patrol.Instance();
    }

    public override void EnterState(HSMAgent agent)
    {
        
    }

    public override void ExitState(HSMAgent agent)
    {
        
    }

    public override void Update(HSMAgent agent)
    {
        if (currentState == null)
        {
            currentState = initialState;
            currentState.EnterState(agent);
        }

        currentState.Update(agent);
    }

    public void FixedUpdate(HSMAgent agent)
    {
        foreach(Transition transition in currentState.transitions)
        {
            if (transition.Condition.Invoke())
            {
                if(transition.targetState.stateLevel == currentState.stateLevel)
                {
                    currentState.ExitState(agent);
                    currentState = transition.targetState;
                    currentState.EnterState(agent);
                }
                else
                {
                    currentState.ExitState(agent);
                    currentState = null;
                }
            }
        }
    }
}
