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
    GameObject target;
    public Attack(baseAI bot, GameObject target) : base(bot)
    {
        this.target = target;
    }

    public override void runTask()
    {
        Debug.Log("Checking attack");
        if (Vector3.Distance(bot.transform.position, target.transform.position) < bot.getData().attackDistance)
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

public class OpenDoor : Task
{
    public OpenDoor(baseAI bot) : base(bot)
    {
        
    }

    public override void runTask()
    {
        Debug.Log("Checking door");
        if (Vector3.Distance(bot.transform.position, bot.getData().doorTarget.transform.position) < bot.getData().attackDistance)
        {
            Debug.Log("Open!");
            bot.getData().doorTarget.SendMessage("OpenDoor");
            bot.getData().doorTarget = null;
            Succeed();
        }
        else
        {
            Debug.Log("Open failed!");
            Fail();
        }
    }
}

public class Move : Action
{
    protected float distanceLeeway;
    GameObject target;
    public Move(baseAI bot, GameObject target) : base(bot)
    {
        distanceLeeway = 5.0f;
        this.target = target;
    }

    public override void runTask()
    {
        if (isRunning())
        {
            {
                bot.getActions().moveTo(target);
                Debug.Log(target);
                if (Vector3.Distance(bot.transform.position, new Vector3(target.transform.position.x, bot.transform.position.y, target.transform.position.z)) < distanceLeeway)
                {
                    Debug.Log("Successfully moved to target");
                    Succeed();
                }
                
            }
        }
    }
}

    public class RandomMove : Action
    {
        Vector3 targetPos;
    protected bool targetSet = false;
    protected float distanceLeeway;

    public RandomMove(baseAI bot) : base(bot)
        {
            distanceLeeway = 10.0f;
        }

        public override void runTask()
        {
            if (isRunning())
            {
            if (!targetSet)
            {
                targetPos = bot.getActions().moveToRandom();
                targetSet = true;
            }
            //else
            {
                bot.getActions().moveTo(targetPos);

                if (Vector3.Distance(bot.transform.position, new Vector3(targetPos.x, bot.transform.position.y, targetPos.z)) < distanceLeeway)
                {
                    targetSet = false;
                    Succeed();
                }
            }
            }
        
        
        Debug.Log("Random move" + " " + targetPos);
        }
    }

