using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
public class Condition : Task
{

    public Condition(baseAI bot) : base(bot)
    {
        
    }
}

public class TargetSpotted : Task
{
    LayerMask layer;
    public TargetSpotted(baseAI bot, LayerMask layer) : base(bot)
    {
        this.layer = layer;
    }

    public override void runTask()
    {
        if (isRunning())
        {
            if(bot.getSenses().getTarget(layer).Count > 0)
            {
                bot.getData().enemyTarget = bot.getSenses().getTarget(layer)[0];
                Succeed();
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
    public TargetInRange(baseAI bot) : base(bot)
    {

    }

    public override void runTask()
    {
        if (isRunning())
        {
            if(Vector3.Distance(bot.getData().enemyTarget.transform.position, bot.transform.position) < bot.getData().attackDistance)
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
