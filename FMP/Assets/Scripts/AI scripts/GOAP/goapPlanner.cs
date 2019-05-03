using System.Collections.Generic;


public class goapPlanner
{
    List<goapNode> completePlan;

    public Queue<goapAction> plan(goapAgent agent, List<goapAction> actions, HashSet<KeyValuePair<string, bool>> worldState, HashSet<KeyValuePair<string, bool>> goalState)
    {
        //Reset actions or remove if they are not possible
        foreach (goapAction action in actions)
        {
            action.reset();

            if (!action.checkTarget(agent))
            {
                actions.Remove(action);
            }
        }

        //Create nodes for A*
        goapNode startNode = new goapNode(worldState, null);
        goapNode targetNode = new goapNode(goalState, null);

        //If a plan is found, succeed will be true
        bool succeed = buildPath(agent, startNode, targetNode, actions);

        if (!succeed)
        {
            return null;
        }

        Queue<goapAction> plan = new Queue<goapAction>();
        foreach (goapNode node in completePlan)
        {
            if (node.action != null)
            {
                //Build sequence of actions
                plan.Enqueue(node.action);
            }
            else
            {
                continue;
            }
        }

    
        return plan;
    }

    public List<goapNode> getPlan()
    {
        return completePlan;
    }

    bool retracePath(goapNode startNode, goapNode targetNode)
    {
        goapNode current = startNode;
        completePlan = new List<goapNode>();
        while (current != null)
        {
            completePlan.Add(current);
            current = current.parent;
        }

        return true;
    }
    protected bool buildPath(goapAgent agent, goapNode startNode, goapNode goalNode, List<goapAction> actions)
    {
 
        List<goapNode> openSet = new List<goapNode>();
        HashSet<goapNode> closedSet = new HashSet<goapNode>();

        openSet.Add(startNode);
        while (openSet.Count > 0)
        {
            //Set to first node in open set
            goapNode currentNode = openSet[0];

            for (int i = 1; i < openSet.Count; i++)
            {
                //If a similar action is found that has the same effect and is lower cost, use this instead
                if (openSet[i].runningCost() <= currentNode.runningCost() && openSet[i].action.getEffects() == currentNode.action.getEffects())
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            //check if the plan has been found by comparing current nodes effect to goal state 
            foreach (KeyValuePair<string, bool> effect in currentNode.getNodeState())
            {
                foreach (KeyValuePair<string, bool> condition in goalNode.getNodeState())
                {
                    if (effect.Equals(condition))
                    {
                        return retracePath(startNode, goalNode);
                    }
                }
            }
            //get next action to be considered
            goapNode next = getNext(agent, actions);
            {
                if (closedSet.Contains(next))
                {
                    continue;
                }

                int newCost = currentNode.runningCost() + 1;

                if (!openSet.Contains(next))
                {
                    //Possible action found, add to open and set parent
                    next.setCost(newCost);
                    currentNode.parent = next;
                    openSet.Add(next);
                }
            }
        }

        return false;
    }

    goapNode getNext(goapAgent agent, List<goapAction> actions)
    {
        //If the current preeffect of an action is the same as the world state, this action will do
        foreach (goapAction action in actions)
        {
            foreach (KeyValuePair<string, bool> preEffect in action.getPreEffects())
            {
                foreach (KeyValuePair<string, bool> worldState in agent.getWorldState())
                {
                    if(preEffect.Equals(worldState))
                    {
                        agent.setWorldState(action.getEffects());
                        return new goapNode(action.getEffects(), action);
                    }
                    else
                    {
                        //Otherwise, check the precondition against world state
                        foreach(KeyValuePair<string, bool> precondition in action.getPreconditions())
                        {
                            if(worldState.Equals(precondition))
                            {
                                agent.setWorldState(action.getEffects());
                                return new goapNode(action.getEffects(), action);
                            }
                        }
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

    public goapNode(HashSet<KeyValuePair<string, bool>> state, goapAction action)
    {
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
