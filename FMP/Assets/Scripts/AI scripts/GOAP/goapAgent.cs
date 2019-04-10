
using System.Collections.Generic;
using System.Linq;


public class goapAgent : baseAI, IGOAP
{
    FSM fsm;
    public goapAction[] attachedActions;
    public List<goapAction> avaliableActions;
    public Queue<goapAction> currentActions;
    public goapPlanner planner;

    public bool hasWood = false;
    public bool taskComplete = false;

    // Use this for initialization
    public override void Start () 
    {
        base.Start();
        attachedActions = GetComponents<goapAction>();
        avaliableActions = attachedActions.ToList();
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
        data.Add(new KeyValuePair<string, bool>("hasWood", false));
        return data;
    }

    public HashSet<KeyValuePair<string, bool>> createGoal()
    {
        HashSet<KeyValuePair<string, bool>> goal = new HashSet<KeyValuePair<string, bool>>();
        goal.Add(new KeyValuePair<string, bool>("hasWood", true));
        return goal;
    }
}
