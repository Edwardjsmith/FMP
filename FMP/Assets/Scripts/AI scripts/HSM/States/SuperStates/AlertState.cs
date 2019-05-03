


using UnityEngine;

public class AlertState : SuperState
{
    bool enemyFound = false;

    bool returnEnemyFound()
    {
        if(enemyFound || agent.getTransitions().isHit())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public AlertState(HSMAgent agent) : base(agent)
    {
        Transition transitionToIlde = new Transition();

        transitionToIlde.Condition = agent.getTransitions().noTargetFound;
        transitionToIlde.targetState = "Idle";
        transitions.Add(transitionToIlde);

        Transition transitionToPlayerSpotted = new Transition();
        transitionToPlayerSpotted.Condition = returnEnemyFound;
        transitionToPlayerSpotted.targetState = "Player spotted";
        transitions.Add(transitionToPlayerSpotted);


        States.Add("checkArea", new moveToAlertSpot(agent));
        States.Add("scanPoint", new alertLookAround(agent));

        initialState = States["checkArea"];
    }


    public override void EnterState()
    {
        agent.getData().sightRange = 35;
        currentState = initialState;
    }
    public override void Update()
    {
        base.Update();

        if (agent.getSenses().getTarget(agent.getSenses().otherAgents).Count > 0)
        {
            GameObject potentialTarget = agent.getSenses().GetClosestObj(agent.getSenses().getTarget(agent.getSenses().otherAgents));
            RaycastHit hitInfo;
            if (Physics.Linecast(agent.transform.position, potentialTarget.transform.position, out hitInfo))
            {
                if (hitInfo.collider.gameObject == potentialTarget)
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
        currentState.ExitState();
    }
}
