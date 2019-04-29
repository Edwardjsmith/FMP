

public class flankState : State<HSMAgent>
{
    public flankState(HSMAgent bot) : base(bot)
    {
        Transition transitionToFireFromCover = new Transition();

        transitionToFireFromCover.Condition = agent.getTransitions().inCover;
        transitionToFireFromCover.targetState = "fireFromCover";
        transitions.Add(transitionToFireFromCover);

        Transition transitionToFindCover = new Transition();
        transitionToFindCover.Condition = agent.getTransitions().isHit;
        transitionToFindCover.targetState = "FindCover";

        transitions.Add(transitionToFindCover);
    }
    public override void EnterState()
    {
        agent.getAnim().SetBool("transitionToCrouch", false);
        agent.getSenses().getCoverFlank(agent.getData().enemyTarget.transform.position);
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
