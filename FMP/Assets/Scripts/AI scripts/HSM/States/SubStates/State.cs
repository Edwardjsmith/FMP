
using System.Collections.Generic;



public struct Transition //Holds the state transition condition and the target state
{
    public delegate bool condition();
    public condition Condition;
    public string targetState;
    
}
public abstract class State<baseAI>
{
    protected baseAI agent;
    public SortedDictionary<string, State<baseAI>> States;

    public State(baseAI Agent)
    {
        stateLevel = 1;
        agent = Agent;
        States = new SortedDictionary<string, State<baseAI>>();
        transitions = new List<Transition>();
    }
    public List<Transition> transitions;
    public int stateLevel;
    public abstract void EnterState();
    public abstract void Update();
    public abstract void ExitState();
}
