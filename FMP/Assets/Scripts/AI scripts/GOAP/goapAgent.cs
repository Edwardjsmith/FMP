using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goapAgent : baseAI
{
    FSM fsm;
    goapAction[] actions;
    // Use this for initialization
    public override void Start ()
    {
        fsm = new FSM(this);
        actions = GetComponents<goapAction>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        fsm.Update();
	}

    private void LateUpdate()
    {
        fsm.LateUpdate();
    }
}
