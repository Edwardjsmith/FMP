using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HSMAgent : gameEntity
{
    HSM hsm;

    Actions agentActions;
    Senses agentSenses;
    Data agentData;
    Transitions stateTransitions;

    public GameObject patrolTarget;

    public Text[] metrics;

	// Use this for initialization
	public override void Start ()
    {
        base.Start();
        agentActions = GetComponent<Actions>();
        agentSenses = GetComponent<Senses>();
        agentData = GetComponent<Data>();
        stateTransitions = GetComponent<Transitions>();
        hsm = new HSM(this);
        health = agentData.health;
        speed = agentData.speed;
	}
	
	// Update is called once per frame
	void Update ()
    {
        hsm.Update();

        metrics[0].text = gameObject.name;
        metrics[1].text = hsm.currentSuperState();
        metrics[2].text = hsm.currentSubState();
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

    public Transitions getTransitions()
    {
        return stateTransitions;
    }

    public override void HitByShot(float damage)
    {
        base.HitByShot(damage);
        getTransitions().amHit = true;
    }

}
