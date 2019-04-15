
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
            if (transition.Condition.Invoke())
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

        transitionToAction.Condition = checkPlan;
        transitionToAction.targetState = "Action";
        transitions.Add(transitionToAction);
    }
    public override void EnterState()
    {
        if(agent.hasTool())
        {
            agent.setWorldState(agent.hasToolState);
        }
        else
        {
            agent.setWorldState(agent.noToolState);
        }

        plan = agent.planner.plan(agent, agent.avaliableActions, agent.getWorldState(), agent.getGoal());
    }

    public override void ExitState()
    {
        hasPlan = false;
    }

    bool checkPlan()
    {
        return hasPlan;
    }
    public override void Update()
    {
        if (plan != null)
        {
            Debug.Log("Plan created");
            agent.currentActions = plan;
            hasPlan = true;
        }
        else
        {
            Debug.Log("Plan could not be created. Try again");
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

        transitionToIdle.Condition = checkTargetNull;
        transitionToIdle.targetState = "Idle";
        transitions.Add(transitionToIdle);

        Transition transitionToAction = new Transition();

        transitionToAction.Condition = checkTargetArrive;
        transitionToAction.targetState = "Action";
        transitions.Add(transitionToAction);
    }

    bool checkTargetNull()
    {
        return noTarget;
    }

    bool checkTargetArrive()
    {
        return atTarget;
    }
    public override void EnterState()
    {
        currentAction = agent.currentActions.Peek();
        agent.getAnim().Play("Walk");
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
        Debug.Log(currentAction.target);
        Debug.Log("Agent " + agent);
        currentAction.inRange = atTarget = agent.getActions().moveTo(currentAction.target);
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

        transitionToMove.Condition = checkMoveNeed;
        transitionToMove.targetState = "Move";
        transitions.Add(transitionToMove);

        Transition transitionToIdle = new Transition();

        transitionToIdle.Condition = checkPlan;
        transitionToIdle.targetState = "Idle";
        transitions.Add(transitionToIdle);
    }

    bool checkPlan()
    {
        return noPlan;
    }

    bool checkMoveNeed()
    {
        return needMove;
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

