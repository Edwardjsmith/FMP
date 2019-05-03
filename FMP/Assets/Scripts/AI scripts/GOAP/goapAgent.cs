using UnityEngine;
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
    HashSet<KeyValuePair<string, bool>> goalstate;
    public HashSet<KeyValuePair<string, bool>> hasToolState;
    public HashSet<KeyValuePair<string, bool>> noToolState;

    public GameObject tool;
    // Use this for initialization
    public override void Start () 
    {
        base.Start();
        tool.SetActive(false);
        //get attached actions
        attachedActions = GetComponents<goapAction>();
        avaliableActions = attachedActions.ToList();

        //Extra states to allow for breakage of tool
        hasToolState = new HashSet<KeyValuePair<string, bool>>();
        hasToolState.Add(new KeyValuePair<string, bool>("hasAxe", true));
        noToolState = new HashSet<KeyValuePair<string, bool>>();
        noToolState.Add(new KeyValuePair<string, bool>("hasAxe", false));

        //Set initial goal state
        goalstate = new HashSet<KeyValuePair<string, bool>>();
        goalstate.Add(new KeyValuePair<string, bool>("taskComplete", true));
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

    public bool hasTool()
    {
        if(tool.activeSelf)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public HashSet<KeyValuePair<string, bool>> getWorldState()
    {
        return worldstate;
    }

    
    public void setWorldState(HashSet<KeyValuePair<string, bool>> state)
    {
        worldstate = state;
    }

    public HashSet<KeyValuePair<string, bool>> getGoal()
    {
        return goalstate;
    }

}
