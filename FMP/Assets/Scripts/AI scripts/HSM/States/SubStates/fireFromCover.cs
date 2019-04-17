using UnityEngine;

public class fireFromCover : State<HSMAgent>
{
    bool run = false;
    bool attack = false;
    bool transitioning = false;
    bool flank = false;

    float timer = 3.0f;

    bool playerClose()
    {
        return Vector3.Distance(agent.transform.position, agent.getData().enemyTarget.transform.position) < agent.getData().safeDistance;
    }

    bool runAway()
    {
        return run;
    }

    bool shortRangedAttack()
    {
        return attack;
    }

    bool flankPlayer()
    {
        return flank;
    }
    public fireFromCover(HSMAgent bot) : base(bot)
    {
        Transition transitionToCover = new Transition();

        transitionToCover.Condition = runAway;
        transitionToCover.targetState = "FindCover";

        transitions.Add(transitionToCover);

        Transition transitionToShortRangedAttack = new Transition();

        transitionToShortRangedAttack.Condition = shortRangedAttack;
        transitionToShortRangedAttack.targetState = "shortRangedAttack";

        transitions.Add(transitionToShortRangedAttack);

        Transition transitionToFlank = new Transition();

        transitionToFlank.Condition = flankPlayer;
        transitionToFlank.targetState = "Flank";

        transitions.Add(transitionToFlank);
    }
    public override void EnterState()
    {
        
    }

    public override void ExitState()
    {
        run = false;
        attack = false;
        transitioning = false;
        flank = false;
        timer = 3.0f;
    }

    public override void Update()
    {
        agent.getActions().Aim();
        if (agent.GetWeapon().enemyInSights())
        {
            agent.getActions().Shoot(agent.getData().enemyTarget);
            timer = 3.0f;
        }
        else
        {
            if(timer <= 0)
            {
                flank = true;
            }
            timer -= Time.deltaTime;
        }
      

        if(playerClose() && !transitioning)
        {
            transitioning = true;
            if(Random.Range(0, 2) < 1)
            {
                run = true;
            }
            else
            {
                attack = true;
            }
        }
    }

}
