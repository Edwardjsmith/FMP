
using System.Collections.Generic;
using UnityEngine;

public class FSM
{
    public State<goapAgent> currentState;
    goapAgent agent;

    public SortedDictionary<string, State<goapAgent>> States;

    public FSM(goapAgent bot)
    {
        agent = bot;
        States = new SortedDictionary<string, State<goapAgent>>();
        States.Add("Idle", new FSMIdle(bot));
        States.Add("Move", new FSMMoveTo(bot));
        States.Add("Action", new FSMPerformAction(bot));

        currentState = States["Idle"];
        currentState.EnterState();
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
            if (transition.Condition == true)
            {
                currentState.ExitState();
                currentState = States[transition.targetState];
                currentState.EnterState();
            }
        }
    }
}

public class FSMIdle : State<goapAgent>
{
    bool hasPlan = false;
    Queue<goapAction> plan;

    public FSMIdle(goapAgent agent) : base(agent)
    {
        transitions = new List<Transition>();
        Transition transitionToAction = new Transition();

        transitionToAction.Condition = hasPlan == true;
        transitionToAction.targetState = "Action";
        transitions.Add(transitionToAction);
    }
    public override void EnterState()
    {
        plan = agent.planner.plan(agent, agent.avaliableActions, agent.worldState(), agent.createGoal());
    }

    public override void ExitState()
    {
        hasPlan = false;
    }

    public override void Update()
    {
        if (plan != null)
        {
            Debug.Log("Plan created");
            agent.currentActions = plan;
            //agent.planFound(agent.createGoal(), plan);
            hasPlan = true;
        }
        else
        {
            Debug.Log("Plan could not be created. Try again");
            //plan = agent.planner.plan(agent, agent.avaliableActions, agent.worldState(), agent.createGoal());
        }
    }
}

public class FSMMoveTo : State<goapAgent>
{
    goapAction currentAction;

    bool noTarget = false;
    bool atTarget = false;
    public FSMMoveTo(goapAgent agent) : base(agent)
    {
        transitions = new List<Transition>();
        Transition transitionToIdle = new Transition();

        transitionToIdle.Condition = noTarget == true;
        transitionToIdle.targetState = "Idle";
        transitions.Add(transitionToIdle);

        Transition transitionToAction = new Transition();

        transitionToAction.Condition = atTarget == true;
        transitionToAction.targetState = "Action";
        transitions.Add(transitionToAction);
    }
    public override void EnterState()
    {
        currentAction = agent.currentActions.Peek();
        if(currentAction.target == null)
        {
            noTarget = true;
        }
    }

    public override void ExitState()
    {
        noTarget = false;
        atTarget = false;
    }

    public override void Update()
    {
        atTarget = agent.getActions().moveTo(currentAction.target);
    }
}

public class FSMPerformAction : State<goapAgent>
{
    goapAction currentAction;
    bool needMove = false;
    bool noPlan = false;
    public FSMPerformAction(goapAgent agent) : base(agent)
    {
        transitions = new List<Transition>();
        Transition transitionToMove = new Transition();

        transitionToMove.Condition = needMove == true;
        transitionToMove.targetState = "Move";
        transitions.Add(transitionToMove);

        Transition transitionToIdle = new Transition();

        transitionToIdle.Condition = noPlan == true;
        transitionToIdle.targetState = "Idle";
        transitions.Add(transitionToIdle);
    }
    public override void EnterState()
    {
        if(agent.currentActions.Count == 0)
        {
            noPlan = true;
        }
        else
        {
            currentAction = agent.currentActions.Peek();
        }
    }

    public override void ExitState()
    {
        needMove = false;
        noPlan = false;
    }

    public override void Update()
    {
        if(currentAction.taskComplete(agent))
        {
            agent.currentActions.Dequeue();
            if(agent.currentActions.Count > 0)
            {
                currentAction = agent.currentActions.Peek();
            }
            else
            {
                noPlan = true;
            }
        }

        if(currentAction.inRange)
        {
            currentAction.executeAction(agent);
        }
        else
        {
            needMove = true;
        }
    }
}

