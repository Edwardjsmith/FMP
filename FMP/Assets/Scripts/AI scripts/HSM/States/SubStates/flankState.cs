
using UnityEngine;

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
        agent.getAnim().Play("Run");
        agent.getData().speed = agent.getData().runSpeed;
        
        agent.getSenses().getCoverFlank(agent.transform.position);
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
            agent.getAnim().Play("crouchAim");
        }
    }
}
