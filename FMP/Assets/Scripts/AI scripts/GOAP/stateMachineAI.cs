using UnityEngine;

public class stateMachineAI : baseAI {

    otherFSM fsm;
    public GameObject target;
    Vector3 guardSpot;
    Quaternion guardRotation;

	// Use this for initialization
	public override void Start ()
    {
        base.Start();
        anim = GetComponentInChildren<Animator>();
        guardSpot = transform.position;
        guardRotation = transform.rotation;
        target = null;
        fsm = new otherFSM(this);
	}
	
	// Update is called once per frame
	public virtual void Update ()
    {
        fsm.Update();
	}
    public virtual void LateUpdate()
    {
        fsm.LateUpdate();
    }

    public virtual void attack()
    {
        if (Random.Range(0, 10) < 1)
        {
            if (target != null)
            {
                //target.gameObject.SendMessage("die");
            }
        }
    }

    public virtual void idle()
    {
        if(getActions().moveTo(guardSpot))
        {
            if(transform.rotation != guardRotation)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, guardRotation, 3);
            }
        }
    }
}
