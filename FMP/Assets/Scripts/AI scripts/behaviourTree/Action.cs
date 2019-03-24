using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
public class Action : Task
{
    public Action(baseAI bot) : base(bot)
    {
        
    }
}

public class Attack : Task
{
    public Attack(baseAI bot) : base(bot)
    {
    }

    public override void runTask()
    {
        if(Vector3.Distance(bot.transform.position, bot.getData().enemyTarget.transform.position) < bot.getData().attackDistance)
        {
            Debug.Log("Attack!");
            Succeed();
        }
        else
        {
            Debug.Log("Attack failed");
            Fail();
        }
    }
}

public class Move : Action
{
    protected bool targetSet = false;
    protected float distanceLeeway;
    public Move(baseAI bot) : base(bot)
    {
        distanceLeeway = 1.0f;
    }

    public override void runTask()
    {
        if (isRunning())
        {
            if (!targetSet)
            {
                if (bot.getData().enemyTarget != null)
                {
                    bot.getActions().moveTo(bot.getData().enemyTarget);
                    targetSet = true;
                }
                else
                {
                    Fail();
                }
            }
            else
            {
                if (Vector3.Distance(bot.transform.position, bot.getData().enemyTarget.transform.position) < distanceLeeway)
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
        Vector3 targetPos;
        public RandomMove(baseAI bot) : base(bot)
        {
            distanceLeeway = 5.0f;
        }

        public override void runTask()
        {
            if (isRunning())
            {
                if (!targetSet)
                {
                    targetPos = bot.getActions().moveToRandom();
                    bot.getActions().moveTo(targetPos);
                    targetSet = true;
                }
                else
                {
                    if (Vector3.Distance(bot.transform.position, targetPos) < distanceLeeway)
                    {
                        targetSet = false;
                        Succeed();
                    }
                }
            }
        }
    }

