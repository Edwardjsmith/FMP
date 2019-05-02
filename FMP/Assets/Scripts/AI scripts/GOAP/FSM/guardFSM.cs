
using System.Collections.Generic;
using UnityEngine;
public class otherFSM
{
    stateMachineAI agent;
    SortedDictionary<string, State<stateMachineAI>> States;
    public State<stateMachineAI> currentState;
    public otherFSM(stateMachineAI bot)
    {
        agent = bot;
        States = null;
        States = new SortedDictionary<string, State<stateMachineAI>>();
        States.Add("Idle", new otherIdle(bot));
        States.Add("Move", new otherMove(bot));
        States.Add("Action", new otherAttack(bot));

        currentState = States["Idle"];
        currentState.EnterState();
    }

    public void Update()
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

public class otherIdle : State<stateMachineAI>
{
    bool transitionToMove()
    {
        return agent.getTarget() != null;
    }
    public otherIdle(stateMachineAI bot) : base(bot)
    {
        transitions = new List<Transition>();
        Transition transitionToAction = new Transition();

        transitionToAction.Condition = transitionToMove;
        transitionToAction.targetState = "Move";
        transitions.Add(transitionToAction);
    }
    public override void EnterState()
    {
        agent.getAnim().Play("Idle");
    }

    public override void ExitState()
    {
        
    }

    public override void Update()
    {
        agent.idle();
    }
}

public class otherMove : State<stateMachineAI>
{
    bool checkTargetNull()
    {
        return agent.getTarget() == null;
    }

    bool checkTargetArrive()
    {
        if (agent.getTarget() != null)
        {
            return Vector3.Distance(agent.transform.position, agent.getTarget().transform.position) < agent.getData().attackDistance;
        }
        else
        {
            return false;
        }
    }

    public otherMove(stateMachineAI bot) : base(bot)
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

    public override void EnterState()
    {
        agent.getAnim().Play("Walk");
    }

    public override void ExitState()
    {

    }

    public override void Update()
    {
        agent.getActions().moveTo(agent.getTarget());
    }
}

public class otherAttack : State<stateMachineAI>
{
    bool inRange()
    {
        if (agent.getTarget() != null)
        {
            return Vector3.Distance(agent.transform.position, agent.getTarget().transform.position) > agent.getData().attackDistance;
        }
        else
        {
            return true;
        }
    }

    bool checkTarget()
    {
        return agent.goToIdle;
    }
    public otherAttack(stateMachineAI bot) : base(bot)
    {
        transitions = new List<Transition>();
        Transition transitionToMove = new Transition();

        transitionToMove.Condition = inRange;
        transitionToMove.targetState = "Move";
        transitions.Add(transitionToMove);

        Transition transitionToIdle = new Transition();

        transitionToIdle.Condition = checkTarget;
        transitionToIdle.targetState = "Idle";
        transitions.Add(transitionToIdle);
    }
    public override void EnterState()
    {
        agent.getData().GetAgent().isStopped = true;
        agent.getAnim().Play("Attack");
    }

    public override void ExitState()
    {
        agent.getData().GetAgent().isStopped = false;
        agent.goToIdle = false;
    }

    public override void Update()
    {
        agent.attack();
    }
}

 

