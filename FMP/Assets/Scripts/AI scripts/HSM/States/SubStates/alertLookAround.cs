

using UnityEngine;

public class alertLookAround : State<HSMAgent>
{
    float timer = 2.0f;
    public alertLookAround(HSMAgent agent) : base(agent)
    {
        transitions = null;
    }
    public override void EnterState()
    {
        agent.getData().GetAgent().isStopped = true;
    }

    public override void ExitState()
    {
        timer = 2.0f;
        agent.getTransitions().noTarget = false;
        agent.getData().GetAgent().isStopped = false;
    }

    public override void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            timer = 2.0f;
            agent.getTransitions().noTarget = true;
        }
    }
}
