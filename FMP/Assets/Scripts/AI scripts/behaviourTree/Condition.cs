﻿
using UnityEngine;
using BehaviourTree;
public class Condition : Task
{

    public Condition(baseAI bot) : base(bot)
    {
        
    }

    public virtual taskState checkCondition()
    {
        return state;
    }

    public override taskState evaluateTask()
    {
        switch (checkCondition())
        {
            case taskState.Success:
                return state = taskState.Success;

            case taskState.Failure:
                return state = taskState.Failure;

            case taskState.Running:
                return state = taskState.Running;

            default:
                return state = taskState.Failure;

        }
    }
}

public class checkRay : Condition
{
    LayerMask layer;
    GameObject[] targets;
    float distance;
    public checkRay(baseAI bot, LayerMask layer, GameObject[] targets, float distance) : base(bot)
    {
        this.layer = layer;
        this.targets = targets;
        this.distance = distance;
    }

    public override taskState checkCondition()
    {
        RaycastHit hit;
        Debug.Log(distance);
        Debug.DrawRay(new Vector3(bot.transform.position.x, bot.transform.position.y + 2, bot.transform.position.z), bot.transform.forward * distance, Color.red);
        if (Physics.Raycast(bot.transform.position, bot.transform.forward * distance, out hit, layer))
        {
            foreach (GameObject obj in targets)
            {
                if (hit.collider.gameObject == obj)
                {
                    bot.getData().doorTarget = obj;
                    Succeed();
                    return state;
                }
            }

            Fail();
            return state;

        }
        else
        {
            Fail();
            return state;
        }
    }
}

public class TargetSpotted : Condition
{
    LayerMask layer;
    GameObject target;
    public TargetSpotted(baseAI bot, LayerMask layer, GameObject target) : base(bot)
    {
        this.layer = layer;
        this.target = target;
    }

    public override taskState checkCondition()
    {
        if (bot.getSenses().getTarget(layer).Count > 0)
        {
            foreach (GameObject go in bot.getSenses().getTarget(layer))
            {
                Succeed();
                return state;
            }
            Fail();
            return state;
        }
        else
        {
            Fail();
            return state;
        }
    }
}

public class isDoorOpen : Condition
{
    openDoor target;
    public isDoorOpen(baseAI bot, openDoor target) : base(bot)
    {
        this.target = target;
    }

    public override taskState checkCondition()
    {
        Debug.Log("Query door");
        if (target != null)
        {
            if (!target.isOpen)
            {
                bot.getData().enemyTarget = target.gameObject;
                Succeed();
                return state;
            }
            else
            {
                Fail();
                return state;
            }
        }
        else
        {
            Fail();
            return state;
        }
    }
}
public class TargetInRange : Condition
{
    GameObject target;
    public TargetInRange(baseAI bot, GameObject target) : base(bot)
    {
        this.target = target;
    }

    public override taskState checkCondition()
    {
        Debug.Log("Checking in range");
        Debug.Log(target);
        if (Vector3.Distance(target.transform.position, bot.transform.position) < bot.getData().attackDistance)
        {
            Succeed();
            return state;
        }
        else
        {
            Fail();
            return state;
        }
    }
}
