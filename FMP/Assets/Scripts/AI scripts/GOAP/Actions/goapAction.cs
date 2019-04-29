using System.Collections.Generic;
using UnityEngine;

public abstract class goapAction : MonoBehaviour
{
    public int cost = 0;

    private HashSet<KeyValuePair<string, bool>> preconditions;
    private HashSet<KeyValuePair<string, bool>> effects;
    private HashSet<KeyValuePair<string, bool>> preEffects;

    public GameObject target;

    public bool inRange = false;
    public string actionName;

    public goapAction()
    {
        preconditions = new HashSet<KeyValuePair<string, bool>>();
        effects = new HashSet<KeyValuePair<string, bool>>();
        preEffects = new HashSet<KeyValuePair<string, bool>>();
        //addPrecondition("taskComplete", false);
    }

    public virtual void reset()
    {
        inRange = false;
    }

    public int getCost()
    {
        return cost;
    }

    public abstract bool taskComplete(goapAgent agent);

    public abstract bool executeAction(goapAgent agent);

    public abstract bool checkTarget(goapAgent agent);

    public void addPrecondition(string name, bool value)
    {
        preconditions.Add(new KeyValuePair<string, bool>(name, value));
    }

    public void addEffect(string name, bool value)
    {
        effects.Add(new KeyValuePair<string, bool>(name, value));
        preEffects.Add(new KeyValuePair<string, bool>(name, !value));
    }

    public void addPreEffect(string name, bool value)
    {
        preEffects.Add(new KeyValuePair<string, bool>(name, value));
    }

    public HashSet<KeyValuePair<string, bool>> getPreconditions()
    {
        return preconditions;
    }
    public HashSet<KeyValuePair<string, bool>> getEffects()
    {
        return effects;
    }

    public HashSet<KeyValuePair<string, bool>> getPreEffects()
    {
        return preEffects;
    }
        
}
