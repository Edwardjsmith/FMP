using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Transition //Holds the state transition condition and the target state
{
    public delegate bool condition();
    public condition Condition;
    public State<HSMAgent> targetState;
}
public class State<HSMAgent>
{
    public List<Transition> transitions;

    public virtual void Start() { }


    public virtual void EnterState(HSMAgent agent) { }
    public virtual void Update(HSMAgent agent) { }
  
    public virtual void ExitState(HSMAgent agent) { }
}
