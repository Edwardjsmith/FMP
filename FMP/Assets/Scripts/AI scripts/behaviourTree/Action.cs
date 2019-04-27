
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
public class Action : Task
{
    public Action(baseAI bot, List<Task> tasks, string name) : base(bot, tasks, name)
    {
        
    }

    public virtual taskState performAction()
    {
        return state;
    }

    public override taskState evaluateTask()
    {
        switch (performAction())
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

public class Attack : Action
{
    float timer = 0.0f;
    GameObject target;
    public Attack(baseAI bot, GameObject target, List<Task> tasks, string name) : base(bot, tasks, name)
    {
        this.target = target;
    }

    public override taskState performAction()
    {
        Debug.Log("Checking attack");
        timer -= Time.deltaTime;
        if (Vector3.Distance(bot.transform.position, target.transform.position) <= bot.getData().attackDistance)
        {
            bot.getAnim().Play("Attack");

            Run(); 
            if(timer <= 0)
            {
                timer = 0.0f;
                Succeed();
            }
            timer -= Time.deltaTime;
            return state;
            
        }
        else
        {
            Debug.Log("Attack failed");
            Fail();
            return state;
        }
    }
}

public class OpenDoor : Action
{
    public OpenDoor(baseAI bot, List<Task> tasks, string name) : base(bot, tasks, name)
    {
        
    }

    public override taskState performAction()
    {
        Debug.Log("Checking door");
        if (Vector3.Distance(bot.transform.position, bot.getData().doorTarget.transform.position) < bot.getData().doorOpenRange)
        {
            Debug.Log("Open!");
            bot.getData().doorTarget.SendMessage("OpenDoor", bot.transform.position);
            bot.getData().doorTarget = null;
            Succeed();
            return state;
        }
        else
        {
            Debug.Log("Open failed!");
            Fail();
            return state;
        }
    }

}

public class Move : Action
{
    protected float distanceLeeway;
    GameObject target;
    public Move(baseAI bot, GameObject target, List<Task> tasks, string name) : base(bot, tasks, name)
    {
        distanceLeeway = 5.0f;
        this.target = target;
    }

    public override taskState performAction()
    {
        Run();
        //bot.getAnim().Play("Walk");
        bot.getActions().moveTo(target);

        if (Vector3.Distance(bot.transform.position, new Vector3(target.transform.position.x, bot.transform.position.y, target.transform.position.z)) < distanceLeeway)
        {
            Debug.Log("Successfully moved to target");
            Succeed();
        }

        return state;
    }
}
public class RandomMove : Action
{
    Vector3 targetPos;
    protected bool targetSet = false;
    protected float distanceLeeway;
    Point[] seed;

    public RandomMove(baseAI bot, Point[] seed, List<Task> tasks, string name) : base(bot, tasks, name)
    {
        distanceLeeway = 2.0f;
        this.seed = seed;
    }

    public override taskState performAction()
    {
        
        Debug.Log("Random move" + " " + targetPos);
        Run();
        //bot.getAnim().Play("Walk");
        if (!targetSet)
        {
            targetPos = seed[Random.Range(0, seed.Length - 1)].transform.position;
            targetSet = true;
        }

        bot.getActions().moveTo(targetPos);

        if (Vector3.Distance(bot.transform.position, new Vector3(targetPos.x, bot.transform.position.y, targetPos.z)) < distanceLeeway)
        {
            targetSet = false;
            Succeed();
        }

        return state;
    }
}







