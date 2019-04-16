
using UnityEngine;


public class HSMAgent : baseAI
{
    HSM hsm;
    Transitions stateTransitions;
    public GameObject patrolTarget;

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
