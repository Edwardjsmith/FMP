using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM
{
    public State<baseAI> currentState;
    baseAI agent;

    public SortedDictionary<string, State<baseAI>> States;

    public FSM(baseAI bot)
    {
        agent = bot;
        States = new SortedDictionary<string, State<baseAI>>();
        States.Add("Idle", new FSMIdle(bot));
        States.Add("Move", new FSMMoveTo(bot));
        States.Add("Action", new FSMPerformAction(bot));

        currentState = States["Idle"];
    }
	public void Update ()
    {
        if (currentState != null)
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
        }
    }
}

public class FSMIdle : State<baseAI>
{
    public FSMIdle(baseAI agent) : base(agent)
    {

    }
    public override void EnterState()
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }
}

public class FSMMoveTo : State<baseAI>
{

    public FSMMoveTo(baseAI agent) : base(agent)
    {

    }
    public override void EnterState()
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }
}

public class FSMPerformAction : State<baseAI>
{
    public FSMPerformAction(baseAI agent) : base(agent)
    {
    }
    public override void EnterState()
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }
}

