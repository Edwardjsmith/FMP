using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Transition //Holds the state transition condition and the target state
{
    public delegate bool condition();
    public condition Condition;
    public string targetState;
    
}
public abstract class State<HSMAgent>
{
    protected HSMAgent agent;
    public SortedDictionary<string, State<HSMAgent>> States;

    public State(HSMAgent Agent)
    {
        stateLevel = 1;
        agent = Agent;
        States = new SortedDictionary<string, State<HSMAgent>>();
    }
    public List<Transition> transitions;
    public int stateLevel;
    public abstract void EnterState();
    public abstract void Update();
    public abstract void ExitState();
}
