﻿
using UnityEngine;



public class HSMAgent : baseAI
{
    HSM hsm;
    Transitions stateTransitions;
    public GameObject patrolTarget;

    public TextMesh subState;
    public TextMesh superState;

    // Use this for initialization
    public override void Start ()
    {
        base.Start();
        anim = GetComponentInChildren<Animator>();
        stateTransitions = GetComponent<Transitions>();
        hsm = new HSM(this);
	}
	
	// Update is called once per frame
	void Update ()
    {
        hsm.Update();
        subState.text = hsm.currentSubState().ToString();
        superState.text = hsm.currentSuperState().ToString();
	}
    private void LateUpdate()
    {
        hsm.LateUpdate();
    }
    private void updatePath(GameObject path)
    {
        if (hsm.currentState == hsm.States["Idle"])
        {
            patrolTarget = path;
            getActions().moveTo(patrolTarget);
        }
    }

    public Transitions getTransitions()
    {
        return stateTransitions;
    }

    void HitByShot()
    {
        getTransitions().amHit = true;
    }

}
