using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSMAgent : MonoBehaviour
{
    AgentTriggers triggers;
    HSM hsm;

	// Use this for initialization
	void Start ()
    {
        triggers = GetComponent<AgentTriggers>();
        hsm = new HSM(this);
	}
	
	// Update is called once per frame
	void Update ()
    {
        hsm.Update();
	}
    private void LateUpdate()
    {
        hsm.LateUpdate();
    }

    public AgentTriggers GetTriggers()
    {
        return triggers;
    }
}
