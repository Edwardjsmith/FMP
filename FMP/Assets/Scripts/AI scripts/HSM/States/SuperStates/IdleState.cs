
using UnityEngine;

public class IdleState : SuperState
{
    bool enemyFound = false;

    bool returnEnemyFound()
    {
        return enemyFound;
    }
    public IdleState(HSMAgent agent) : base(agent)
    {
        States.Add("Patrol", new Patrol(agent));
        States.Add("Guard", new Guard(agent));
        Transition transitionToAlert = new Transition();

        transitionToAlert.Condition = agent.getTransitions().getPlayerHeard;
        transitionToAlert.targetState = "Alert";
        transitions.Add(transitionToAlert);

        Transition transitionToPlayerSpotted = new Transition();
        transitionToPlayerSpotted.Condition = returnEnemyFound;
        transitionToPlayerSpotted.targetState = "Player spotted";
        transitions.Add(transitionToPlayerSpotted);

        initialState = States["Patrol"];
    
    }

    public override void EnterState()
    {
        agent.getData().sightRange = 25;
        agent.getAnim().SetBool("transitionToCrouch", false);
        agent.getData().speed = agent.getData().walkSpeed;
    }
    public override void Update()
    {
        base.Update();

        if (agent.getSenses().getTarget(agent.getSenses().otherAgents).Count > 0)
        {
            var potentialTarget = agent.getSenses().GetClosestObj(agent.getSenses().getTarget(agent.getSenses().otherAgents));
            RaycastHit hitInfo;
            if (Physics.Linecast(agent.transform.position, potentialTarget.transform.position, out hitInfo))
            {
                if (hitInfo.collider == potentialTarget.GetComponent<Collider>())
                {
                    enemyFound = true;
                    agent.getData().enemyTarget = potentialTarget;
                }
            }
        }
    }
    public override void ExitState()
    {
        enemyFound = false;
        agent.getTransitions().playerHeard = false;
        currentState.ExitState();
    }

}
