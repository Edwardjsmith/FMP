
using System.Collections.Generic;
using System.Linq;


public class goapAgent : baseAI
{
    FSM fsm;
    public goapAction[] attachedActions;
    public List<goapAction> avaliableActions;
    public Queue<goapAction> currentActions;
    public goapPlanner planner;

    public bool hasWood = false;
    public bool taskComplete = false;

    HashSet<KeyValuePair<string, bool>> worldstate;

    // Use this for initialization
    public override void Start () 
    {
        base.Start();
        attachedActions = GetComponents<goapAction>();
        avaliableActions = attachedActions.ToList();
        worldstate = new HashSet<KeyValuePair<string, bool>>();
        worldstate.Add(new KeyValuePair<string, bool>("hasAxe", false));
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

    public HashSet<KeyValuePair<string, bool>> getWorldState()
    {
        return worldstate;
    }

    public void setWorldState(HashSet<KeyValuePair<string, bool>> state)
    {
        worldstate = state;
    }

    public HashSet<KeyValuePair<string, bool>> createGoal()
    {
        HashSet<KeyValuePair<string, bool>> goal = new HashSet<KeyValuePair<string, bool>>();
        goal.Add(new KeyValuePair<string, bool>("taskComplete", true));
        return goal;
    }
}
