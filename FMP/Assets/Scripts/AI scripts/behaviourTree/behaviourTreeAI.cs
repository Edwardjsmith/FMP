using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviourTree;


public class behaviourTreeAI : baseAI
{

    public GameObject Player;
    GameObject[] doors;
    public LayerMask player;
    public LayerMask door;
    
    Selector root;


    //Player side of tree
    Sequence playerSpotted;
    Selector dealWithPlayer;
    Sequence playerInRange;

    //Random move
    Selector randomMove;
    


    Sequence doorBlock;


    public override void Start()
    {
        base.Start();

        doors = GameObject.FindGameObjectsWithTag("door");

        root = new Selector(this);
        playerSpotted = new Sequence(this);
        dealWithPlayer = new Selector(this);
        playerInRange = new Sequence(this);
        doorBlock = new Sequence(this);

        randomMove = new Selector(this);
        randomMove.addChild(0, doorBlock);
        randomMove.addChild(1, new RandomMove(this));

        playerSpotted.addChild(0, new TargetSpotted(this, player, Player));
        playerSpotted.addChild(1, dealWithPlayer);

        dealWithPlayer.addChild(0, playerInRange);

        playerInRange.addChild(0, new TargetInRange(this, Player));
        playerInRange.addChild(1, new Attack(this, Player));

        dealWithPlayer.addChild(1, doorBlock);

        doorBlock.addChild(0, new checkRay(this, door, doors));
        doorBlock.addChild(1, new OpenDoor(this));

        dealWithPlayer.addChild(1, new Move(this, Player));

        root.addChild(0, playerSpotted);
        root.addChild(1, randomMove);
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
