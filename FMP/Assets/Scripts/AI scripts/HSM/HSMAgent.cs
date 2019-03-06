using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSMAgent : gameEntity
{
    HSM hsm;

    Actions agentActions;
    Senses agentSenses;
    Data agentData;
    Conditions stateTransitions;

    public GameObject patrolTarget;

	// Use this for initialization
	void Start ()
    {
        agentActions = GetComponent<Actions>();
        agentSenses = GetComponent<Senses>();
        agentData = GetComponent<Data>();
        stateTransitions = GetComponent<Conditions>();
        hsm = new HSM(this);
	}
	
	// Update is called once per frame
	void Update ()
    {
        hsm.Update();
	}
    private void LateUpdate()
    {
        hsm.LateUpdate();
    }
    private void updatePath(GameObject path)
    {
        patrolTarget = path;
        getActions().moveTo(patrolTarget);
    }

    public Actions getActions()
    {
        return agentActions;
    }
    public Senses getSenses()
    {
        return agentSenses;
    }
    public Data getData()
    {
        return agentData;
    }

    public Conditions getTransitions()
    {
        return stateTransitions;
    }

}
