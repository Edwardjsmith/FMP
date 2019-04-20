
using UnityEngine;
using BehaviourTree;
using System.Threading;


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

    Point[] patrolPoints;

    Thread btTree;

    public TextMesh treePosition;

    public override void Start()
    {
        base.Start();

        patrolPoints = FindObjectsOfType<Point>();
        anim = GetComponentInChildren<Animator>();
        doors = GameObject.FindGameObjectsWithTag("door");

        root = new Selector(this, "Root");
        playerSpotted = new Sequence(this, "Player spotted");
        dealWithPlayer = new Selector(this, "");
        playerInRange = new Sequence(this, "Checking player range");
        doorBlock = new Sequence(this, "Door spotted");

        randomMove = new Selector(this, "Patrolling");
        randomMove.addChild(0, doorBlock);
        randomMove.addChild(1, new RandomMove(this, patrolPoints));

        playerSpotted.addChild(0, new TargetSpotted(this, player, Player));
        playerSpotted.addChild(1, dealWithPlayer);

        dealWithPlayer.addChild(0, playerInRange);

        GameObject[] play = new GameObject[1] { Player };
        

        playerInRange.addChild(0, new checkRay(this, player,  play, getData().attackDistance));
        playerInRange.addChild(1, new Attack(this, Player));

        dealWithPlayer.addChild(1, doorBlock);

        doorBlock.addChild(0, new checkRay(this, door, doors, getData().doorOpenRange));
        doorBlock.addChild(1, new OpenDoor(this));

        dealWithPlayer.addChild(2, new Move(this, Player));

        root.addChild(0, playerSpotted);
        root.addChild(1, randomMove);
    }

    void executeTree()
    {
        if (root.currentState() == Task.taskState.NotSet)
        {
            root.reset();
        }

        root.evaluateTask(treePosition);
    }

    private void FixedUpdate()
    {
        executeTree();
    }
}
