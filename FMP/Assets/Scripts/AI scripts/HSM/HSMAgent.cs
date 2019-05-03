
using UnityEngine;



public class HSMAgent : baseAI
{
    HSM hsm;
    Transitions stateTransitions;
    public GameObject patrolTarget;

    public TextMesh subState;
    public TextMesh superState;

    // Use this for initialization
    public override void Start ()
    {
        base.Start();
        stateTransitions = GetComponent<Transitions>();
        hsm = new HSM(this);
        FPSBody.playerSound += stateTransitions.heardPlayer;
	}
	
	// Update is called once per frame
	void Update ()
    {
        hsm.Update();
        subState.text = hsm.currentSubState().ToString();
        superState.text = hsm.currentSuperState().ToString();
	}
    private void LateUpdate()
    {
        getAnim().SetFloat("velY", getData().GetAgent().velocity.z);
        getAnim().SetFloat("velX", getData().GetAgent().velocity.x);
        hsm.LateUpdate();
    }
    private void updatePath(GameObject path)
    {
        if (hsm.currentState == hsm.States["Idle"])
        {
            patrolTarget = path;
            getActions().moveTo(patrolTarget);
        }
    }

    public Transitions getTransitions()
    {
        return stateTransitions;
    }

    void HitByShot()
    {
        getTransitions().amHit = true;
        getTransitions().enemyTargetFound = true;
        getData().health -= 1;
    }

  

}
