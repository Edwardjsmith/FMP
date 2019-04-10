using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goapPlanner 
{
    List<goapNode> nodes;
    List<goapNode> path;

    public Queue<goapAction> plan(goapAgent agent, List<goapAction> actions, HashSet<KeyValuePair<string, bool>> worldState, HashSet<KeyValuePair<string, bool>> goalState)
    {
        foreach (goapAction action in actions)
        {
            action.reset();

            if (!action.checkTarget(agent))
            {
                actions.Remove(action);
            }
        }
        createNodes(actions);


        goapNode startNode = new goapNode(worldState, null);
        goapNode targetNode = new goapNode(goalState, null);
        bool succeed = buildPath(startNode, targetNode);

        if (!succeed)
        {
            return null;
        }

        Queue<goapAction> plan = new Queue<goapAction>();
        foreach (goapNode node in path)
        {
            if (node.action != null)
            {
                plan.Enqueue(node.action);
            }
            else
            {
                continue;
            }
        }

        return plan;
    }

    void createNodes(List<goapAction> possibleActions)
    {
        nodes = new List<goapNode>();

        foreach(goapAction action in possibleActions)
        {
            nodes.Add(new goapNode(action.getEffects(), action));
        }
    }

    bool retracePath(goapNode startNode, goapNode targetNode)
    {
        goapNode current = targetNode;
        path = new List<goapNode>();
        while (current != null)
        {
            path.Add(current);
            current = current.parent;
        }

        path.Reverse();
        return true;
    }
    protected bool buildPath(goapNode startNode, goapNode targetNode)
    {
        List<goapNode> openSet = new List<goapNode>();
        HashSet<goapNode> closedSet = new HashSet<goapNode>();

        openSet.Add(startNode);
        while(openSet.Count > 0)
        {
            goapNode currentNode = openSet[0];

            for(int i = 1; i < openSet.Count; i++)
            {
                if(openSet[i].runningCost() <= currentNode.runningCost())
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);
            
            foreach(KeyValuePair<string, bool> effect in currentNode.getNodeState())
            {
                foreach(KeyValuePair<string, bool> condition in targetNode.getNodeState())
                {
                    if(effect.Equals(condition))
                    {
                        return retracePath(startNode, targetNode);
                    }
                }
            }

            foreach(goapNode neighbour in getNext(currentNode))
            {
                if (closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newCost = currentNode.runningCost() + 1;

                if(!openSet.Contains(neighbour))
                {
                    neighbour.setCost(newCost);
                    neighbour.parent = currentNode;

                    //if(!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }
        /*bool foundPath = false;
        foreach(goapAction action in possibleActions)
        {
            if(satisfiesPrecondition(action.getPreconditions(), startNode.getNodeState()))
            {
                HashSet<KeyValuePair<string, bool>> currentState = updateState(startNode.getNodeState(), action.getEffects());
                goapNode node = new goapNode(startNode, startNode.runningCost() + action.cost, currentState, action);
                if(satisfiesGoal(currentState, goalState))
                {
                    path.Add(node);
                    foundPath = true;
                }
                else
                {
                    possibleActions.Remove(action);
                    foundPath = buildPath(node, path, possibleActions, goalState);
                }
            }
        }
        return foundPath;*/

        return false;
    }

    List<goapNode> getNext(goapNode current)
    {
        Debug.Log(current.action);
        List<goapNode> next = new List<goapNode>();

        foreach(goapNode node in nodes)
        {
            foreach (KeyValuePair<string, bool> effect in node.action.getEffects())
            {
                foreach (KeyValuePair<string, bool> currentPrecondition in current.getNodeState())
                {
                    KeyValuePair<string, bool> currentPreconditionInverse = new KeyValuePair<string, bool>(currentPrecondition.Key, !currentPrecondition.Value);
                    
                    if (effect.Equals(currentPreconditionInverse))
                    {
                        //if (node != current)
                        {
                            current.parent = node;
                            next.Add(node);
                        }
                    }
                }
            }
        }

        return next;
    }
    protected bool satisfiesPrecondition(HashSet<KeyValuePair<string, bool>> actionPrecondition, HashSet<KeyValuePair<string, bool>> parentEffects)
    {
        bool allMatch = true;
        foreach(KeyValuePair<string, bool> precondition in actionPrecondition)
        {
            bool match = false;
            foreach(KeyValuePair<string, bool> effect in parentEffects)
            {
                if(effect.Equals(precondition))
                {
                    match = true;
                    break;
                }
            }
            allMatch = match;
        }
        return allMatch;
    }

    protected bool satisfiesGoal(HashSet<KeyValuePair<string, bool>> actionPrecondition, HashSet<KeyValuePair<string, bool>> parentEffects)
    {
        bool match = false;
        foreach (KeyValuePair<string, bool> precondition in actionPrecondition)
        {
            foreach (KeyValuePair<string, bool> effect in parentEffects)
            {
                if (effect.Equals(precondition))
                {
                    match = true;
                    break;
                }
            }

        }
        return match;
    }

    protected HashSet<KeyValuePair<string, bool>> updateState(HashSet<KeyValuePair<string, bool>> currentState, HashSet<KeyValuePair<string, bool>> updatedState)
    {
        HashSet<KeyValuePair<string, bool>> stateChange = currentState;

        foreach(KeyValuePair<string, bool> update in updatedState)
        {
            bool exists = false;
            foreach(KeyValuePair<string, bool> change in stateChange)
            {
                if(update.Key.Equals(change.Key))
                {
                    exists = true;
                    break;
                }
            }
            if(exists)
            {
                stateChange.RemoveWhere((KeyValuePair<string, bool> r) => { return r.Key.Equals(update.Key); });
                KeyValuePair<string, bool> updated = new KeyValuePair<string, bool>(update.Key, update.Value);
                stateChange.Add(updated);
            }
            else
            {
                stateChange.Add(new KeyValuePair<string, bool>(update.Key, update.Value));
            }
        }


        return stateChange;
    }
}


public class goapNode
{
    public goapNode parent;
    int cost;
    HashSet<KeyValuePair<string, bool>> state;
    public goapAction action;

    public goapNode(HashSet<KeyValuePair<string, bool>> state, goapAction action)
    {
        //cost = action.getCost();
        this.state = state;
        this.action = action;
    }
    
    public goapNode getParent()
    {
        return parent;
    }

    public int runningCost()
    {
        return cost;
    }

    public void setCost(int value)
    {
        cost = value;
    }
    public HashSet<KeyValuePair<string, bool>> getNodeState()
    {
        return state;
    }

    public goapAction getNodeAction()
    {
        return action;
    }
}
