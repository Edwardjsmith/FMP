﻿

public class flankState : State<HSMAgent>
{
    bool returnToCover()
    {
        return cover;
    }
    bool cover = false;
    public flankState(HSMAgent bot) : base(bot)
    {
        Transition transitionToFireFromCover = new Transition();

        transitionToFireFromCover.Condition = agent.getTransitions().inCover;
        transitionToFireFromCover.targetState = "fireFromCover";
        transitions.Add(transitionToFireFromCover);

        Transition transitionToFindCover = new Transition();
        transitionToFindCover.Condition = returnToCover;
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
        cover = false;
    }

    public override void Update()
    {
        if (agent.getActions().takeCover())
        {
            agent.getTransitions().covered = true;
            agent.getAnim().SetBool("transitionToCrouch", true);
        }
        else
        {
            if (agent.getData().health <= 0)
            {
                cover = true;
                agent.getData().health = 3;
            }
        }


    }
}
