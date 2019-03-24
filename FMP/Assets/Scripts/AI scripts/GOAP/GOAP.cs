using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAP
{ 
    public goapAction planActions(worldState state, int depth, goapAction[] actions, goapGoal[] goals)
    {
        int currentDepth = 0;
        float currentValue = 0;
        float bestValue = 0;
        goapAction bestAction = null;
        worldState[] models = new worldState[depth + 1];

        models[0] = state;
        while (currentDepth >= 0)
        {
            currentValue = models[currentDepth].calculateDiscontentment(actions[0], goals);

            if (currentDepth >= depth)
            {
                if (currentValue < bestValue)
                {
                    bestValue = currentValue;
                    bestAction = actions[0];

                    currentDepth -= 1;

                    continue;
                }
            }

            goapAction nextAction = models[currentDepth].nextAction();

            if (nextAction != null)
            {
                models[currentDepth + 1] = models[currentDepth];
                actions[currentDepth] = nextAction;
                models[currentDepth + 1].applyAction(nextAction);

                currentDepth += 1;
            }
            else
            {
                currentDepth -= 1;
            }
        }

        return bestAction;
    }
}


public class goapGoal
{
    public float value = 0.0f;
    public string name;
    public float change;

    public float getChange()
    {
        return 0;
    }

    public float getDiscontentment(float value)
    {
        return value * value;
    }
}

public class worldState
{
    public float calculateDiscontentment(goapAction action, goapGoal[] goals)
    {
        float discontentment = 0;

        foreach (goapGoal goal in goals)
        {
            float newValue = goal.value + action.getGoalChange(goal);
            newValue += action.getDuration() * goal.getChange();
            discontentment += goal.getDiscontentment(newValue);
        }

        return discontentment;
    }

    public goapAction nextAction()
    {
        return new goapAction();
    }

    public void applyAction(goapAction action)
    {

    }
}

