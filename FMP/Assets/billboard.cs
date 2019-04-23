﻿using UnityEngine;

public class billboard : MonoBehaviour
{
    Camera cam;
    TextMesh text;
    goapAgent agent;
    string goapPlan;
    int count = 0;


    public enum type { goap, HSM};
    public type Type;
    // Use this for initialization
    void Start ()
    {
        cam = Camera.main;
        text = GetComponent<TextMesh>();
        agent = GetComponentInParent<goapAgent>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Type == type.goap)
        {
            if (agent.currentActions != null)
            {
                if (count > agent.currentActions.Count)
                {
                    goapPlan = "";
                    count = 0;
                }

                foreach (goapAction node in agent.currentActions)
                {
                    if (count < agent.currentActions.Count - 1)
                    {
                        goapPlan += node.actionName + " -> ";
                        count++;
                    }
                    else if(count == agent.currentActions.Count - 1)
                    {
                        goapPlan += " " + node.actionName;
                        count++;
                        break;
                    }
                }
            }
            else
            {
                count = 0;
            }

            text.text = goapPlan;
        }

        transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
	}
}
