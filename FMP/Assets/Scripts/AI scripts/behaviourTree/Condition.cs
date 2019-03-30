
using UnityEngine;
using BehaviourTree;
public class Condition : Task
{

    public Condition(baseAI bot) : base(bot)
    {
        
    }
}

public class checkRay : Task
{
    LayerMask layer;
    GameObject[] targets;
    public checkRay(baseAI bot, LayerMask layer, GameObject[] targets) : base(bot)
    {
        this.layer = layer;
        this.targets = targets;
    }

    public override void runTask()
    {
        if (isRunning())
        {
            RaycastHit hit;
            Debug.DrawLine(bot.transform.position, bot.transform.forward, Color.red);
            if(Physics.Linecast(bot.transform.position, bot.transform.forward, out hit, layer))
            {
                foreach(GameObject obj in targets)
                {
                    if(hit.collider.gameObject == obj)
                    {
                        bot.getData().doorTarget = obj;
                        Succeed();
                    }
                    else
                    {
                        Fail();
                    }
                }
                
            }
          
        }
    }
}

public class TargetSpotted : Task
{
    LayerMask layer;
    GameObject target;
    public TargetSpotted(baseAI bot, LayerMask layer, GameObject target) : base(bot)
    {
        this.layer = layer;
        this.target = target;
    }

    public override void runTask()
    {
        if (isRunning())
        {
            foreach (GameObject go in bot.getSenses().getTarget(layer))
            {
                if (go == target)
                {
                    Debug.Log("Target spotted");
                    Debug.Log(target);
                    Succeed();
                }
                else
                {
                    Fail();
                }
            }
            
        }
    }
}

public class isDoorOpen : Task
{
    openDoor target;
    public isDoorOpen(baseAI bot, openDoor target) : base(bot)
    {
        this.target = target;
    }

    public override void runTask()
    {
       if (isRunning())
        {
            Debug.Log("Query door");
            if (target != null)
            {
                if (!target.isOpen)
                {
                    bot.getData().enemyTarget = target.gameObject;
                    Succeed();
                }
                else
                {
                    Fail();
                }
            }
            else
            {
                Fail();
            }
        }
    }
}
public class TargetInRange : Task
{
    GameObject target;
    public TargetInRange(baseAI bot, GameObject target) : base(bot)
    {
        this.target = target;
    }

    public override void runTask()
    {
        if (isRunning())
        {
            Debug.Log("Checking in range");
            if(Vector3.Distance(target.transform.position, bot.transform.position) < bot.getData().attackDistance)
            {
                Succeed();
            }
            else
            {
                Fail();
            }
        }
    }
}
