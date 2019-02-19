using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperState : State<HSMAgent>
{
    List<State<HSMAgent>> innerStates;
    State<HSMAgent> initialState = null;
    State<HSMAgent> currentState = null;

    public SuperState()
    {
        stateLevel = 0;
    }

    public override void EnterState(HSMAgent agent)
    {
        if(currentState == null)
        {
            currentState = initialState;
            currentState.EnterState(agent);
        }
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
    }

    public void FixedUpdate(HSMAgent agent)
    {
        foreach(Transition transition in currentState.transitions)
        {
            if (transition.Condition.Invoke())
            {
                if(transitio)
            }
        }
    }
}
