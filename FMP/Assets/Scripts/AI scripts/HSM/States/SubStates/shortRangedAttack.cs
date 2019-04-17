﻿
using System.Collections.Generic;
using UnityEngine;

public class shortRangedAttack : State<HSMAgent>
{
    bool run = false;
    float timer = 1.0f;
    public shortRangedAttack(HSMAgent bot) : base(bot)
    {
        transitions = new List<Transition>();
        Transition transitionToCover = new Transition();

        transitionToCover.Condition = runAway;
        transitionToCover.targetState = "FindCover";

        transitions.Add(transitionToCover);
    }

    bool runAway()
    {
        return run;
    }
    public override void EnterState()
    {
        agent.getAnim().Play("Run");
        agent.getData().speed = agent.getData().runSpeed;
    }

    public override void ExitState()
    {
        run = false;
    }

    public override void Update()
    {
        Vector3 targetDir = agent.getData().enemyTarget.transform.position - agent.transform.position;
        Vector3 target = agent.getData().enemyTarget.transform.position + (targetDir.normalized  * -3);

        if (agent.getActions().moveTo(target))
        {
            agent.getActions().Shoot(agent.getData().enemyTarget);
            agent.getAnim().Play("singleShot");
            agent.getActions().Shoot(agent.getData().enemyTarget);

            if (timer <= 0)
            {
                run = true;
            }

            timer -= Time.deltaTime;
        }
    }
}
