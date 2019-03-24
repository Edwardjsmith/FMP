using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
public class Action : Task
{
    public Action(agentActions bot) : base(bot, Vector3.zero, false)
    {
        
    }
}

public class Move : Action
{
    protected Vector3 targetPos;
    protected bool targetSet = false;
    protected float distanceLeeway;
    public Move(agentActions bot, Vector3 target) : base(bot)
    {
        targetPos = target;
        distanceLeeway = 1.0f;
    }

    public override void runTask()
    {
        if (isRunning())
        {
            if (!targetSet)
            {
                botActions.moveTo(targetPos);
                targetSet = true;
            }
            else
            {
                if (Vector3.Distance(botActions.transform.position, targetPos) < distanceLeeway)
                {
                    targetSet = false;
                    Succeed();
                }
            }
        }
    }
}

    public class RandomMove : Move
    {
        public RandomMove(agentActions bot) : base(bot, Vector3.zero)
        {
            distanceLeeway = 5.0f;
        }

        public override void runTask()
        {
            if (isRunning())
            {
                if (!targetSet)
                {
                    targetPos = botActions.moveToRandom();
                    botActions.moveTo(targetPos);
                    targetSet = true;
                }
                else
                {
                    if (Vector3.Distance(botActions.transform.position, targetPos) < distanceLeeway)
                    {
                        targetSet = false;
                        Succeed();
                    }
                }
            }
        }
    }

