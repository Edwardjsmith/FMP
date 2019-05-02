
using UnityEngine;

public class monsterFSMAI : stateMachineAI
{
    public stateMachineAI[] targets;
	// Use this for initialization
	public override void Start ()
    {
        base.Start();
    }

	
	// Update is called once per frame
	public override void Update ()
    {
        base.Update();
	}

    public override void LateUpdate()
    {
        base.LateUpdate();
    }

    public void die()
    {
        gameObject.SetActive(false);
    }

    public override void idle()
    {
        setTarget(getSenses().getTarget(targetLayer)[0]);

        if (getTarget() != null)
        {
            getActions().moveTo(getTarget());
        }
    }
    public override void attack()
    {
        getAnim().Play("Attack");
    }
}
