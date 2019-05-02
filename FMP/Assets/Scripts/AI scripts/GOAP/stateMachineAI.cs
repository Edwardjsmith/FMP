using UnityEngine;

public class stateMachineAI : baseAI {

    otherFSM fsm;
    GameObject target;
    Vector3 guardSpot;
    Quaternion guardRotation;
    public LayerMask targetLayer;

    public bool goToIdle = false;

	// Use this for initialization
	public override void Start ()
    {
        base.Start();
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
                target.gameObject.SendMessage("die");
                target = null;
                goToIdle = true;
            }
        }
    }

    public virtual void idle()
    {
        if (getActions().moveTo(guardSpot))
        {
            getAnim().Play("Idle");
            if (transform.rotation != guardRotation)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, guardRotation, 3);
            }
        }
        else
        {
            getAnim().Play("Walk");
        }

        if (getSenses().getTarget(targetLayer).Count > 0)
        {
            target = getSenses().getTarget(targetLayer)[0];
        }
    }


    public GameObject getTarget()
    {
        return target;
    }
    public void setTarget(GameObject t)
    {
        target = t;
    }
}
