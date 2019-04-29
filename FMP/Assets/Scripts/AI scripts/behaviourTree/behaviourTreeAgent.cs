
using UnityEngine;
using BehaviourTree;
using System.Collections.Generic;
using UnityEngine.UI;

public class behaviourTreeAgent : baseAI
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

    public Canvas uiMetrics;
    public Button uiNode;
    public Image line;

    List<Task> uiTree;
    float childOffsetX, childOffsetY;
    public override void Start()
    {
        base.Start();
        uiTree = new List<Task>();
        patrolPoints = FindObjectsOfType<Point>();
        doors = GameObject.FindGameObjectsWithTag("door");

        root = new Selector(this, "Root", uiTree, new Vector2(150, 600));
        playerSpotted = new Sequence(this, "Player spotted", uiTree);
        dealWithPlayer = new Selector(this, "Deal with player", uiTree);
        playerInRange = new Sequence(this, "Checking player range", uiTree);
        doorBlock = new Sequence(this, "Door spotted", uiTree);

        randomMove = new Selector(this, "Patrolling", uiTree);
        randomMove.addChild(0, doorBlock);
        randomMove.addChild(1, new RandomMove(this, patrolPoints, uiTree, "Move randomly"));

        playerSpotted.addChild(0, new TargetSpotted(this, player, Player, uiTree, "Is target Spotted?"));
        playerSpotted.addChild(1, dealWithPlayer);

        dealWithPlayer.addChild(0, playerInRange);

        playerInRange.addChild(0, new checkDistance(this, Player, getData().attackDistance, uiTree, "Check target distance"));
        playerInRange.addChild(1, new Attack(this, Player, uiTree, "Attack!"));

        dealWithPlayer.addChild(1, doorBlock);

        doorBlock.addChild(0, new checkRay(this, door, doors, getData().doorOpenRange, uiTree, "Check ray"));
        doorBlock.addChild(1, new OpenDoor(this, uiTree, "Open door"));

        dealWithPlayer.addChild(2, new Move(this, Player, uiTree, "Move"));

        root.addChild(0, playerSpotted);
        root.addChild(1, randomMove);

        foreach(Task node in uiTree)
        {
            Button nodeButton = Instantiate(uiNode);
            RectTransform buttonTransform = nodeButton.GetComponent<RectTransform>();
            buttonTransform.anchorMin = new Vector2(0, 1);
            buttonTransform.anchorMax = new Vector2(0, 1);
            buttonTransform.pivot = new Vector2(0.5F, 0.5F);
            nodeButton.GetComponentInChildren<Text>().text = node.name;
            buttonTransform.SetParent(uiMetrics.transform);
            buttonTransform.position = node.uiPos;
            node.asignedNode = nodeButton;

            if (node.childTasks.Count > 0)
            {
                node.calculateChildUIPos();

                foreach (Task child in node.childTasks)
                {
                    Image arrow = Instantiate(line, child.uiPos, Quaternion.identity);
                    Vector2 difference = node.uiPos - child.uiPos;
                    arrow.rectTransform.sizeDelta = new Vector2(difference.magnitude * 6, 100f);
                    arrow.rectTransform.pivot = new Vector2(0, 0.5f);
                    float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                    arrow.rectTransform.rotation = Quaternion.Euler(0, 0, angle);
                    arrow.rectTransform.SetParent(uiMetrics.transform);
                }
            }
        }
    }


    void executeTree()
    {
        root.reset();
        root.evaluateTask();
    }

    private void Update()
    {
        executeTree();

        foreach (Task node in uiTree)
        {
            if (node.isSuccess())
            {
                var color = node.asignedNode.GetComponent<Button>().colors;
                color.normalColor = Color.green;
                node.asignedNode.GetComponent<Button>().colors = color;
            }
            else if(node.isRunning())
            {
                var color = node.asignedNode.GetComponent<Button>().colors;
                color.normalColor = Color.blue;
                node.asignedNode.GetComponent<Button>().colors = color;
            }
            else if(node.isFailure())
            {
                var color = node.asignedNode.GetComponent<Button>().colors;
                color.normalColor = Color.red;
                node.asignedNode.GetComponent<Button>().colors = color;
            }
            else
            {
                var color = node.asignedNode.GetComponent<Button>().colors;
                color.normalColor = Color.grey;
                node.asignedNode.GetComponent<Button>().colors = color;
            }

            node.reset();
        }
    }
}
