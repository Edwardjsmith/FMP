using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviourTree;


public class behaviourTreeAI : baseAI
{ 
    Repeat root;
    Selector selector;
    public override void Start()
    {
        base.Start();
        root = new Repeat(getActions());
        selector = new Selector(getActions());
        selector.addChild(0, new RandomMove(getActions()));
        root.addChild(0, selector);
    }
    void Update()
    {
        if (root.returnState() == Task.taskState.NotSet)
        {
            root.Start();
        }

        root.runTask();
    }
}
