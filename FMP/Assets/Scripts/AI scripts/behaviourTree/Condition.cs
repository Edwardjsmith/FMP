using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
public class Condition : Task
{

    public Condition(agentActions bot) : base(bot)
    {
        
    }
}

public class PlayerSpotted : Task
{
    public PlayerSpotted(agentActions bot) : base(bot)
    {

    }

    public override void runTask()
    {
        if (isRunning())
        {
            //if(botActions.)
            {
              
            }
            //else
            {
                
            }
        }
    }
}
