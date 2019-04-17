using UnityEngine;

public class baseAI : gameEntity {

    agentActions actions;
    Senses senses;
    Data data;
    protected Animator anim;
    // Use this for initialization
    public override void Start ()
    {
        base.Start();
        actions = GetComponent<agentActions>();
        senses = GetComponent<Senses>();
        data = GetComponent<Data>();
        health = data.health;
        speed = data.speed;
    }

    public agentActions getActions()
    {
        return actions;
    }
    public Senses getSenses()
    {
        return senses;
    }
    public Data getData()
    {
        return data;
    }
}
