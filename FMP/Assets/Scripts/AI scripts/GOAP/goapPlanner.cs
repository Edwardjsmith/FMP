﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goapPlanner
{
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

        goapNode startNode = new goapNode(worldState, null);
        goapNode targetNode = new goapNode(goalState, null);
        bool succeed = buildPath(agent, startNode, targetNode, actions);

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

    bool retracePath(goapNode startNode, goapNode targetNode)
    {
        goapNode current = startNode;
        path = new List<goapNode>();
        while (current != null)
        {
            path.Add(current);
            current = current.parent;
        }

        return true;
    }
    protected bool buildPath(goapAgent agent, goapNode startNode, goapNode targetNode, List<goapAction> actions)
    {
        List<goapNode> openSet = new List<goapNode>();
        HashSet<goapNode> closedSet = new HashSet<goapNode>();

        openSet.Add(startNode);
        while (openSet.Count > 0)
        {
            goapNode currentNode = openSet[0];

            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].runningCost() <= currentNode.runningCost())
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            foreach (KeyValuePair<string, bool> effect in currentNode.getNodeState())
            {
                foreach (KeyValuePair<string, bool> condition in targetNode.getNodeState())
                {
                    if (effect.Equals(condition))
                    {
                        //startNode.parent = currentNode;
                        return retracePath(startNode, targetNode);
                    }
                }
            }

            goapNode neighbour = getNext(agent, actions);
            {
                if (closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newCost = currentNode.runningCost() + 1;

                if (!openSet.Contains(neighbour))
                {
                    neighbour.setCost(newCost);
                    currentNode.parent = neighbour;
                    openSet.Add(neighbour);
                }
            }
        }

        return false;
    }

    goapNode getNext(goapAgent agent, List<goapAction> actions)
    {

        List<goapNode> next = new List<goapNode>();

        foreach (goapAction action in actions)
        {
            foreach (KeyValuePair<string, bool> preEffect in action.getPreEffects())
            {
                foreach (KeyValuePair<string, bool> worldState in agent.getWorldState())
                {
                    if (preEffect.Equals(worldState))
                    {
                        
                        agent.setWorldState(action.getEffects());
                        return new goapNode(action.getEffects(), action);
                        //next.Add(node);
                    }
                }
            }
        }

        return null;
    }
}
public class goapNode
{
    public goapNode parent;
    int cost;
    HashSet<KeyValuePair<string, bool>> state;
    public goapAction action;
    public bool conditionSatisfied = false;
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
