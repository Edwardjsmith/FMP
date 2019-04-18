


public class AlertState : SuperState
{
    public AlertState(HSMAgent agent) : base(agent)
    {
        Transition transitionToIdle = new Transition();

        States.Add("Player detected", new PlayerDetected(agent));
        States.Add("FindCover", new findCover(agent));
        States.Add("fireFromCover", new fireFromCover(agent));
        States.Add("shortRangedAttack", new shortRangedAttack(agent));
        States.Add("Flank", new flankState(agent));

        transitionToIdle.Condition = agent.getTransitions().enemyLost;
        transitionToIdle.targetState = "Idle";
        transitions.Add(transitionToIdle);
    }


    public override void EnterState()
    {
        if(agent.getTransitions().isHit())
        {
            currentState = States["FindCover"];
        }
        else
        {
            currentState = States["Player detected"];
        }

        //agent.getData().speed = agent.getData().walkSpeed;
    }
    public override void Update()
    {
        base.Update();
    }
    public override void ExitState()
    {
        
    }
}
