
using UnityEngine;
using BehaviourTree;
using System.Collections.Generic;

public class Condition : Task
{

    public Condition(baseAI bot, List<Task> tasks, string name) : base(bot, tasks, name)
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
                state = taskState.Success;
                return state;

            case taskState.Failure:
                state = taskState.Failure;
                return state;

            case taskState.Running:
                state = taskState.Running;
                return state;

            default:
                state = taskState.Failure;
                return state;

        }
    }
}

public class checkDistance : Condition
{
    GameObject target;
    float distance;

    public checkDistance(baseAI bot, GameObject target, float distance, List<Task> tasks, string name) : base(bot, tasks, name)
    {
        this.target = target;
        this.distance = distance;
    }

    public override taskState checkCondition()
    {
        if(Vector3.Distance(bot.transform.position, target.transform.position) <= distance)
        {
            Debug.Log("Player in range");
            Succeed();
            return state;
        }
        else
        {
            Debug.Log("Player not in range");
            Fail();
            return state;
        }
    }
}

public class checkRay : Condition
{
    LayerMask layer;
    GameObject[] targets;
    float distance;
    public checkRay(baseAI bot, LayerMask layer, GameObject[] targets, float distance, List<Task> tasks, string name) : base(bot, tasks, name)
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
    public TargetSpotted(baseAI bot, LayerMask layer, GameObject target, List<Task> tasks, string name) : base(bot, tasks, name)
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
                RaycastHit hitInfo;
                if (Physics.Linecast(bot.transform.position, go.transform.position, out hitInfo))
                {
                    if (hitInfo.collider == go.GetComponent<Collider>())
                    {
                        Succeed();
                        Debug.Log(go);
                        return state;
                    }
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

public class isDoorOpen : Condition
{
    openDoor target;
    public isDoorOpen(baseAI bot, openDoor target, List<Task> tasks, string name) : base(bot, tasks, name)
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
    public TargetInRange(baseAI bot, GameObject target, List<Task> tasks, string name) : base(bot, tasks, name)
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
