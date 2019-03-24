using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviourTree;


public class behaviourTreeAI : baseAI
{

    public GameObject Player = null;
    public LayerMask player;
    public LayerMask door;
    
    Selector root;
    Sequence playerSpotted;
    public override void Start()
    {
        base.Start();
        getData().enemyTarget = Player;
        playerSpotted = new Sequence(this);
        playerSpotted.addChild(0, new TargetSpotted(this, player));
        playerSpotted.addChild(1, new Move(this));
        playerSpotted.addChild(2, new TargetInRange(this));
        playerSpotted.addChild(3, new Attack(this));


        root = new Selector(this);
        root.addChild(0, playerSpotted);
        root.addChild(1, new RandomMove(this));
    }
    void Update()
    { 
        if(root.returnState() == Task.taskState.NotSet)
        {
            root.Start();
        }

        root.runTask();
    }
}
