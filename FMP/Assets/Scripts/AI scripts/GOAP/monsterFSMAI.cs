
using UnityEngine;

public class monsterFSMAI : stateMachineAI
{
    public stateMachineAI[] targets;
	// Use this for initialization
	public override void Start ()
    {
        anim = GetComponentInChildren<Animator>();
        target = null;
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
        target = null;
        gameObject.SetActive(false);
    }

    public override void idle()
    {
        
    }
    public override void attack()
    {
       
    }
}
