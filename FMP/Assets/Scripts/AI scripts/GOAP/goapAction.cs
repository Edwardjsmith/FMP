using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class goapAction
{
    public float cost = 0;

    private HashSet<KeyValuePair<string, object>> preconditions;
    private HashSet<KeyValuePair<string, object>> effects;

    public GameObject target;

    private bool inRange = false;

    public goapAction()
    {
        preconditions = new HashSet<KeyValuePair<string, object>>();
        effects = new HashSet<KeyValuePair<string, object>>();
    }

    public virtual void reset()
    {
        inRange = false;
        target = null;
    }

    public abstract bool taskComplete();

    public abstract bool testAction(baseAI agent);
    public abstract bool performAction(baseAI agent);

    public void addPrecondition(string name, object value)
    {
        preconditions.Add(new KeyValuePair<string, object>(name, value));
    }

    public void addEffect(string name, object value)
    {
        effects.Add(new KeyValuePair<string, object>(name, value));
    }
}
