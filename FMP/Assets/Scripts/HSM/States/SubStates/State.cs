using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Transition //Holds the state transition condition and the target state
{
    public delegate bool condition();
    public condition Condition;
    public State<HSMAgent> targetState;
    
}
public abstract class State<HSMAgent>
{
    public List<Transition> transitions;
    public int stateLevel;
    public abstract void EnterState(HSMAgent agent);
    public abstract void Update(HSMAgent agent);
    public abstract void ExitState(HSMAgent agent);
}
