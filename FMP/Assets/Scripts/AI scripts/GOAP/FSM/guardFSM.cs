using System.Collections;
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
    bool transitionToAttack()
    {
        return agent.target != null;
    }
    public otherIdle(stateMachineAI bot) : base(bot)
    {
        transitions = new List<Transition>();
        Transition transitionToAction = new Transition();

        transitionToAction.Condition = transitionToAttack;
        transitionToAction.targetState = "Action";
        transitions.Add(transitionToAction);
    }
    public override void EnterState()
    {
        
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
        return agent.target == null;
    }

    bool checkTargetArrive()
    {
        return Vector3.Distance(agent.transform.position, agent.target.transform.position) < agent.getData().attackDistance;
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
        agent.getActions().moveTo(agent.target);
    }
}

public class otherAttack : State<stateMachineAI>
{
    bool inRange()
    {
        return Vector3.Distance(agent.transform.position, agent.target.transform.position) > agent.getData().attackDistance;
    }

    bool checkTarget()
    {
        return agent.target != null;
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
        
    }

    public override void ExitState()
    {

    }

    public override void Update()
    {
        agent.getAnim().Play("Attack");
        agent.attack();
    }
}

 

