
using System.Collections.Generic;
using System.Linq;


public class goapAgent : baseAI, IGOAP
{
    FSM fsm;
    public goapAction[] actions;
    public List<goapAction> avaliableActions;
    public Queue<goapAction> currentActions;
    public goapPlanner planner;

    public bool hasWood = false;
    public bool taskComplete = false;

    // Use this for initialization
    public override void Start ()
    {
        actions = GetComponents<goapAction>();
        avaliableActions = actions.ToList();
        planner = new goapPlanner();
        fsm = new FSM(this);
	}
	
	// Update is called once per frame
	void Update ()
    {
        fsm.Update();
	}

    private void LateUpdate()
    {
        fsm.LateUpdate();
    }

    public HashSet<KeyValuePair<string, bool>> worldState()
    {
        HashSet<KeyValuePair<string, bool>> data = new HashSet<KeyValuePair<string, bool>>();
        data.Add(new KeyValuePair<string, bool>("taskComplete", false));
        return data;
    }

    public HashSet<KeyValuePair<string, bool>> createGoal()
    {
        HashSet<KeyValuePair<string, bool>> goal = new HashSet<KeyValuePair<string, bool>>();
        goal.Add(new KeyValuePair<string, bool>("taskComplete", true));
        return goal;
    }

    //public void planFailed(HashSet<KeyValuePair<string, bool>> failedGoal)
    //{
    //    throw new System.NotImplementedException();
    //}

    //public void planFound(HashSet<KeyValuePair<string, bool>> goal, Queue<goapAction> actions)
    //{
    //    throw new System.NotImplementedException();
    //}

    //public void actionsFinished()
    //{
    //    throw new System.NotImplementedException();
    //}

    //public void planAborted(goapAction abortedAction)
    //{
    //    throw new System.NotImplementedException();
    //}

    public bool moveTo(goapAction nextAction)
    {
        return nextAction.inRange = getActions().moveTo(nextAction.target);
    }
}
