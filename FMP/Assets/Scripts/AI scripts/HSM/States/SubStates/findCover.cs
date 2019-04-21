

public class findCover : State<HSMAgent>
{
    public findCover(HSMAgent agent) : base(agent)
    {
        Transition transitionToFireFromCover = new Transition();

        transitionToFireFromCover.Condition = agent.getTransitions().inCover;
        transitionToFireFromCover.targetState = "fireFromCover";
        transitions.Add(transitionToFireFromCover);
    }


    public override void EnterState()
    {
        agent.getAnim().SetBool("transitionToCrouch", false);
        agent.getSenses().getCover();
    }

    public override void ExitState()
    {
        agent.getTransitions().covered = false;
    }

    public override void Update()
    {
        if (agent.getActions().takeCover())
        {
            agent.getTransitions().covered = true;
            agent.getAnim().SetBool("transitionToCrouch", true);
        }

    }
}

